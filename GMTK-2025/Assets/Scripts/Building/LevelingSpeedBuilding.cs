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
        if (level < reduceLevelingSpeed.Count)
        {
            float speedBoost = reduceLevelingSpeed[level];
            // Apply the speed boost to the building's functionality
            UpgradesManager.instance.onChangeReduceLevelingSpeed(speedBoost);
        }
    }
}
