using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradesManager : MonoBehaviour
{
    public float reduceLevelingSpeed = 0f;
    public int maxLevel = 3;
    public int additionalTrainingUnits = 0;
    public Dictionary<UnitSO, float> damageUpgrades = new Dictionary<UnitSO, float>();
    public Dictionary<UnitSO, float> healthUpgrades = new Dictionary<UnitSO, float>();
    public static UpgradesManager instance { get; private set; }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }

    public void onChangeReduceLevelingSpeed(float speedBoost)
    {
        reduceLevelingSpeed += speedBoost;
    }

    public void changeDamageMultiplier(float damage, UnitSO archetype)
    {

        damageUpgrades[archetype] = damage;
    }

    public void changeHealthMultiplier(float health, UnitSO archetype)
    {
        healthUpgrades[archetype] = health;
    }

    public void onChangeMaxLevel(int level)
    {
        maxLevel += level;
    }

    public void OnChangeMaxUnitsPresent(int units)
    {
        additionalTrainingUnits += units;
    }

}

