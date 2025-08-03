using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeasantUpgrade : Building
{
    public List<int> increasedDamage = new List<int>();
    public override void applyUpgrade()
    {
        if (!canUpgrade()) return;

        level++;
        buildingObject.SetActive(true);
        recruitmentObject.SetActive(true);
        if (level <=increasedDamage.Count)
        {
            UpgradesManager.instance.onIncreaseDamage(increasedDamage[level-1], "Peasant");
            assignTexts();
        }
    }
}
