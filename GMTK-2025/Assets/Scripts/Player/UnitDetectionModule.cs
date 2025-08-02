using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitDetectionModule : MonoBehaviour
{
    private UnitInstance parentUnit;

    public GameObject getTarget()
    {
        if (parentUnit == null) parentUnit = GetComponent<UnitInstance>();

        List<UnitFighter> potentialTargets = new List<UnitFighter>();

        List<UnitFighter> enemyUnit = EnemySpawner.instance.spawnedEnemies;
        List<UnitFighter> playerUnits = TrainingManager.instance.combatUnits;
        bool isTargetingEnemies = parentUnit.archetype.targetType == TARGET_TYPE.Enemy;
        bool isEnemy = parentUnit.archetype.isEnemy;

        if (isEnemy && isTargetingEnemies) potentialTargets.AddRange(playerUnits);
        if (isEnemy && !isTargetingEnemies) potentialTargets.AddRange(enemyUnit);

        if (!isEnemy && isTargetingEnemies) potentialTargets.AddRange(enemyUnit);
        if (!isEnemy && !isTargetingEnemies) potentialTargets.AddRange(playerUnits);

        // Sort by distance to parentUnit, closest first
        potentialTargets.Sort((a, b) =>
            Vector3.Distance(parentUnit.transform.position, a.transform.position)
            .CompareTo(
                Vector3.Distance(parentUnit.transform.position, b.transform.position)
            )
        );

        Vector2 castleAttackPoint = new Vector2(CastleManager.instance.transform.position.x, parentUnit.transform.position.y);
        float distanceToCastle = Vector2.Distance(parentUnit.transform.position, castleAttackPoint);
        if (isEnemy && potentialTargets.Count == 0 && distanceToCastle < parentUnit.archetype.attackRange)
        {
            return CastleManager.instance.gameObject;
        }

        return potentialTargets.Count > 0 ? potentialTargets[0].gameObject : null;
    }
}

public enum TARGET_TYPE
{
    Enemy,
    Ally,
}