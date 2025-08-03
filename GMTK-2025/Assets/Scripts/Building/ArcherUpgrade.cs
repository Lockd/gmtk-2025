using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherUpgrade : Building
{
    public List<float> damageMultiplier = new List<float>();
    public List<float> healthMultiplier = new List<float>();
    public override void applyUpgrade()
    {
        if (!canUpgrade()) return;

        level++;
        buildingObject.SetActive(true);
        recruitmentObject.SetActive(true);
        if (level <= damageMultiplier.Count)
        {
            UpgradesManager.instance.changeDamageMultiplier(damageMultiplier[level - 1], targetUnit);
            assignTexts();
        }
        if (level <= healthMultiplier.Count)
        {
            UpgradesManager.instance.changeHealthMultiplier(healthMultiplier[level - 1], targetUnit);
            assignTexts();
        }

        afterUpgrade();
    }
}
