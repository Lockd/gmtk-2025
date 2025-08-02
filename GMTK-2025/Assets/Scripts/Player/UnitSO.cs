using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewUnitClass", menuName = "Units/UnitClass")]
public class UnitSO : ScriptableObject
{
    public List<Sprite> spritesPerLevel = new List<Sprite>();
    public Sprite icon;
    public int maxLevel = 3;
    public int health;
    public int healthPerLevel;
    public int attack;
    public int attackPerLevel;
    public float levelUpTime = 3f;
    public float moveSpeed;
    public float detectionRadius;

    public ActionSheet actionSheet;

    public Vector2 spawnOffset;
}
