using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewUnitClass", menuName = "Units/UnitClass")]
public class UnitSO : ScriptableObject
{
    public bool isEnemy = false;
    public TARGET_TYPE targetType = TARGET_TYPE.Enemy;
    public ATTACK_TYPE attackType = ATTACK_TYPE.Melee;
    public float AOERadius = 2f;
    public List<Sprite> spritesPerLevel = new List<Sprite>();
    public Sprite icon;
    public int purchasePrice = 5;
    public int goldOnKill = 1;
    public int maxLevel = 3;
    public List<int> health;
    public float attackRange = 1f;
    public float breakBetweenAttacks = 0.5f;
    public List<int> attack;
    public float levelUpTime = 3f;
    public float moveSpeed;
    public ActionSheet actionSheet;
    public Vector2 spawnOffset;
}

public enum ATTACK_TYPE
{
    Melee,
    Melee_AOE,
    Ranged,
    Ranged_AOE
}