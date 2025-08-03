using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainingUnitsUpgrade : Building
{
    public List<int> additionalUnits = new List<int>();
    public override void applyUpgrade()
    {
        if (!canUpgrade()) return;

        level++;
        buildingObject.SetActive(true);
        if (level <= additionalUnits.Count)
        {
            int units = additionalUnits[level - 1];
            UpgradesManager.instance.OnChangeMaxUnitsPresent(units);
        }
        afterUpgrade();
    }
}
