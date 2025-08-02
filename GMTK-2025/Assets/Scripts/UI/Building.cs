using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Building : MonoBehaviour
{
    // public string buildingName;
    // public string buildingDescription;
    public int level = 0;
    public int maxLevel = 0;
    public Sprite buildingSprite;
    public List<int> upgradePrice = new List<int>();

    public abstract void applyUpgrade();

    public bool canUpgrade()
    {
        int cost = upgradePrice[level];
        return GoldManager.instance.canAfford(cost);
    }
}
