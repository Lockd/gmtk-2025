using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitFighter : MonoBehaviour
{
    public GameObject currentTarget;
    private float enemiesCheckTimer = 0f;
    private float attackAt = 0f;
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

        unitInstance.hp.onDeath.AddListener(onDeath);
    }

    void Update()
    {
        if (unitInstance.hp.isDead) return;

        // if (currentTarget != null)
        // {
        //     actionsManager.PerformAction(currentTarget.gameObject);
        // }

        float moveSpeed = unitInstance.archetype.moveSpeed;

        // Move to target
        if (currentTarget != null)
        {
            transform.position = Vector2.MoveTowards(transform.position, currentTarget.transform.position, moveSpeed * Time.deltaTime);
        }
        // If is enemy and no target - some to attack castle, but not lock the target on castle
        if (currentTarget == null && unitInstance.archetype.isEnemy)
        {
            Vector2 castlePosition = new Vector2(CastleManager.instance.transform.position.x, transform.position.y);
            transform.position = Vector2.MoveTowards(transform.position, castlePosition, moveSpeed * Time.deltaTime);
        }

        // If no target and not at starting position, return to it
        if (currentTarget == null && Vector2.Distance(transform.position, startingPosition) > 0.1f && !unitInstance.archetype.isEnemy)
        {
            transform.position = Vector2.MoveTowards(transform.position, startingPosition, moveSpeed * Time.deltaTime);
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
        if (attackAt > Time.time || currentTarget == null) return;
        if (Vector2.Distance(transform.position, currentTarget.transform.position) <= unitInstance.archetype.attackRange)
        {
            attackTarget();
        }
    }

    private void attackTarget()
    {
        HealthComponent targetHp = currentTarget.GetComponent<HealthComponent>();
        if (targetHp == null)
        {
            Debug.LogWarning("Target does not have HealthComponent");
            return;
        }

        int damage = unitInstance.archetype.attack[unitInstance.currentLevel];
        targetHp.onChangeHP(-damage);
        attackAt = unitInstance.archetype.breakBetweenAttacks + Time.time;
    }

    public void lookForTarget()
    {
        currentTarget = detection.getTarget();
    }

    private void onDeath()
    {
        unitInstance.cosmetics.setSprite(null);
        if (unitInstance.archetype.isEnemy) EnemySpawner.instance.onUnitDeath(this);
        else TrainingManager.instance.onUnitDeath(this);
        Destroy(gameObject, 0.5f);
    }
}
