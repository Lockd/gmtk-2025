using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelingSpeedBuilding : Building
{
    public List<float> reduceLevelingSpeed = new List<float>();
    public override void applyUpgrade()
    {
        if (!canUpgrade()) return;

        level++;
        buildingObject.SetActive(true);
        if (level <= reduceLevelingSpeed.Count)
        {
            float speedBoost = reduceLevelingSpeed[level - 1];
            UpgradesManager.instance.onChangeReduceLevelingSpeed(speedBoost);
        }
        afterUpgrade();
    }
}
