using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaxLevelUpgrade : Building
{
    public List<int> additionalLevels = new List<int>();
    public override void applyUpgrade()
    {
        if (!canUpgrade()) return;

        level++;
        buildingObject.SetActive(true);
        if (level <= additionalLevels.Count)
        {
            int levels = additionalLevels[level - 1];
            UpgradesManager.instance.onChangeMaxLevel(levels);
            assignTexts();
        }
        afterUpgrade();
    }
}
