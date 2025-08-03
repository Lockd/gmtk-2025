using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradesManager : MonoBehaviour
{
    public float reduceLevelingSpeed = 0f;
    public int maxLevel = 3;
    public int additionalTrainingUnits = 0;
    public Dictionary<UnitSO, List<float>> damageUpgrades = new Dictionary<UnitSO, List<float>>();
    public Dictionary<UnitSO, List<float>> healthUpgrades = new Dictionary<UnitSO, List<float>>();
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
        if (!damageUpgrades.ContainsKey(archetype))
        {
            damageUpgrades[archetype] = new List<float>();
        }
        damageUpgrades[archetype].Add(damage);
    }

    public void changeHealthMultiplier(float health, UnitSO archetype)
    {
        if (!healthUpgrades.ContainsKey(archetype))
        {
            healthUpgrades[archetype] = new List<float>();
        }
        healthUpgrades[archetype].Add(health);
    }

    public void onChangeMaxLevel(int level)
    {
        maxLevel += level;
    }

    public void OnChangeMaxUnitsPresent(int units)
    {
        additionalTrainingUnits += units;
    }

    public float getDamageMultiplier(UnitSO archetype)
    {
        if (damageUpgrades.ContainsKey(archetype) && damageUpgrades[archetype].Count > 0)
        {
            float baseMultiplier = 1;
            for (int i = 0; i < damageUpgrades[archetype].Count; i++)
            {
                baseMultiplier *= damageUpgrades[archetype][i];
            }
            return baseMultiplier;
        }
        return 1f;
    }

    public float getHealthMultiplier(UnitSO archetype)
    {
        if (healthUpgrades.ContainsKey(archetype) && healthUpgrades[archetype].Count > 0)
        {
            float baseMultiplier = 1;
            for (int i = 0; i < healthUpgrades[archetype].Count; i++)
            {
                baseMultiplier *= healthUpgrades[archetype][i];
            }
            return baseMultiplier;
        }
        return 1f;
    }
}

