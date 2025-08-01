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
    public float speed;
}
