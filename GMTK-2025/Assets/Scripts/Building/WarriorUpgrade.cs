using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WarriorUpgrade : Building
{
    public List<int> increasedDamage = new List<int>();
    public override void applyUpgrade()
    {
        if (!canUpgrade()) return;

        level++;
        buildingObject.SetActive(true);
        recruitmentObject.SetActive(true);
        if (level <= increasedDamage.Count)
        {
            UpgradesManager.instance.onIncreaseDamage(increasedDamage[level - 1], "Warrior");
            assignTexts();
        }
    }
}
