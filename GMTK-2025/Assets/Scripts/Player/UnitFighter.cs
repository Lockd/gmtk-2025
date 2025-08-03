using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitFighter : MonoBehaviour
{
    public GameObject currentTarget;
    private float enemiesCheckTimer = 0f;
    private float attackAt = 0f;
    private float damageMultiplier = 0;
    Vector2 startingPosition;

    [Header("Modules")]
    [SerializeField] private UnitDetectionModule detection;

    [Header("References")]
    [SerializeField] private UnitInstance unitInstance;
    [SerializeField] private ActionsManager actionsManager;

    void Start()
    {
        startingPosition = transform.position;
        actionsManager.actionSheet = unitInstance.archetype.actionSheet;

        damageMultiplier = UpgradesManager.instance.damageUpgrades.ContainsKey(unitInstance.archetype)
            ? UpgradesManager.instance.damageUpgrades[unitInstance.archetype]
            : 0;

        unitInstance.hp.onDeath.AddListener(onDeath);
    }

    void Update()
    {
        if (unitInstance.hp.isDead) return;

        float moveSpeed = unitInstance.archetype.moveSpeed;
        bool isInAttackRange = currentTarget != null && Vector2.Distance(transform.position, currentTarget.transform.position) <= unitInstance.archetype.attackRange;

        // Move to target
        if (currentTarget != null && !isInAttackRange)
        {
            transform.position = Vector2.MoveTowards(transform.position, currentTarget.transform.position, moveSpeed * Time.deltaTime);
            unitInstance.animationHandler.playWalkingAnimation(true);
        }
        // If is enemy and no target - some to attack castle, but not lock the target on castle
        if (currentTarget == null && unitInstance.archetype.isEnemy)
        {
            Vector2 castlePosition = new Vector2(CastleManager.instance.transform.position.x, transform.position.y);
            transform.position = Vector2.MoveTowards(transform.position, castlePosition, moveSpeed * Time.deltaTime);
            unitInstance.animationHandler.playWalkingAnimation(true);

        }
        // If no target and not at starting position, return to it
        if (currentTarget == null && Vector2.Distance(transform.position, startingPosition) > 0.1f && !unitInstance.archetype.isEnemy)
        {
            transform.position = Vector2.MoveTowards(transform.position, startingPosition, moveSpeed * Time.deltaTime);
            unitInstance.animationHandler.playWalkingAnimation(true);
        }

        // Try to search for target every 0.3 seconds
        if (currentTarget == null && Time.time >= enemiesCheckTimer)
        {
            lookForTarget();
            enemiesCheckTimer = Time.time + 0.3f;
        }

        checkCanAttack();
        checkIfTargetDead();
    }

    private void checkIfTargetDead()
    {
        if (currentTarget == null) return;

        HealthComponent targetHealth = currentTarget.GetComponent<HealthComponent>();
        if (targetHealth != null && targetHealth.isDead)
        {
            currentTarget = null;
            lookForTarget();
            return;
        }
    }

    private void checkCanAttack()
    {
        if (attackAt > Time.time || currentTarget == null)
        {
            unitInstance.animationHandler.playAttackAnimation(false);
            return;
        }
        if (Vector2.Distance(transform.position, currentTarget.transform.position) <= unitInstance.archetype.attackRange)
        {
            attackTarget();
        }
    }

    private void attackTarget()
    {
        HealthComponent targetHp = currentTarget.GetComponent<HealthComponent>();
        if (targetHp == null) return;

        unitInstance.animationHandler.playAttackAnimation(true);
        unitInstance.animationHandler.playWalkingAnimation(false);

        int damage = (int)(unitInstance.archetype.attack[unitInstance.currentLevel - 1] * (1 + damageMultiplier));

        if (unitInstance.archetype.attackType == ATTACK_TYPE.Melee || unitInstance.archetype.attackType == ATTACK_TYPE.Ranged)
        {
            targetHp.onChangeHP(-damage);
            attackAt = unitInstance.archetype.breakBetweenAttacks + Time.time;
            return;
        }

        Vector3 AOEPosition = Vector3.zero;
        if (unitInstance.archetype.attackType == ATTACK_TYPE.Ranged_AOE)
        {
            AOEPosition = currentTarget.transform.position;
        }
        else if (unitInstance.archetype.attackType == ATTACK_TYPE.Melee_AOE)
        {
            AOEPosition = transform.position;
        }

        Collider2D[] hits = Physics2D.OverlapCircleAll(AOEPosition, unitInstance.archetype.AOERadius);
        foreach (var hit in hits)
        {
            if (hit.gameObject == currentTarget) continue; // Skip the main target
            UnitFighter otherFighter = hit.GetComponent<UnitFighter>();
            if (otherFighter != null && otherFighter.unitInstance.hp != null && !otherFighter.unitInstance.hp.isDead)
            {
                otherFighter.unitInstance.hp.onChangeHP(-damage);
            }
        }
        // Always damage the main target
        int mainDamage = (int)(unitInstance.archetype.attack[unitInstance.currentLevel - 1] * (1 + damageMultiplier));
        targetHp.onChangeHP(-mainDamage);
        attackAt = unitInstance.archetype.breakBetweenAttacks + Time.time;
    }

    public void lookForTarget()
    {
        currentTarget = detection.getTarget();
    }

    private void onDeath()
    {
        if (unitInstance.archetype.isEnemy)
        {
            EnemySpawner.instance.onUnitDeath(this);
            GoldManager.instance.changeGold(unitInstance.archetype.goldOnKill);
        }
        else TrainingManager.instance.onUnitDeath(this);

        unitInstance.animationHandler.playDeathAnimation();

        Destroy(gameObject, 0.8f);
    }
}
