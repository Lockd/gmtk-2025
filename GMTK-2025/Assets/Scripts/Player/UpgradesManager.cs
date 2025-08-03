using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradesManager : MonoBehaviour
{
    public float reduceLevelingSpeed = 0f;
    public int maxLevel = 3;
    public int maxTrainingUnits = 5;
    public int additionalPeasantDamage = 0;
    public int additionalWarriorDamage = 0;
    public int additionalArcherDamage = 0;
    public int additionalPriestDamage = 0;
    public int additionalMageDamage = 0;
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

    public void onIncreaseDamage(int damage, string className)
    {
        if(className == "Peasant")
        {
            additionalPeasantDamage += damage;
        }
        else if (className == "Warrior")
        {
            additionalWarriorDamage += damage;
        }
        else if (className == "Archer")
        {
            additionalArcherDamage += damage;
        }
        else if (className == "Priest")
        {
            additionalPriestDamage += damage;
        }
        else if (className == "Mage")
        {
            additionalMageDamage += damage;
        }
    }

    public void onChangeMaxLevel(int level)
    {
        maxLevel+= level;
    }

    public void OnChangeMaxUnitsPresent(int units)
    {
        maxTrainingUnits += units;
    }

}

