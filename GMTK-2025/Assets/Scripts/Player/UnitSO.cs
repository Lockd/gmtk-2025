using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewUnitClass", menuName = "Units/UnitClass")]
public class UnitSO : ScriptableObject
{
    public bool isEnemy = false;
    public TARGET_TYPE targetType = TARGET_TYPE.Enemy;
    public List<Sprite> spritesPerLevel = new List<Sprite>();
    public Sprite icon;
    public int purchasePrice = 5;
    public int maxLevel = 3;
    public int health;
    public int healthPerLevel;
    public float attackRange = 1f;
    public float breakBetweenAttacks = 0.5f;
    public int attack;
    public int attackPerLevel;
    public float levelUpTime = 3f;
    public float moveSpeed;

    public ActionSheet actionSheet;
    public Vector2 spawnOffset;
}
