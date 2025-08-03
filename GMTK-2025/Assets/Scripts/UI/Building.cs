using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public abstract class Building : MonoBehaviour
{
    // public string buildingName;
    // public string buildingDescription;
    public int level = 0;
    public int maxLevel = 0;
    public GameObject buildingObject;
    public GameObject recruitmentObject;
    public List<int> upgradePrice = new List<int>();
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI costText;

    public abstract void applyUpgrade();

    private void Awake()
    {
        costText.text = upgradePrice[level] + "";
    }
    public bool canUpgrade()
    {
        int cost = 0;
        if (level < upgradePrice.Count)
        {
            cost = upgradePrice[level];
            if (GoldManager.instance.canAfford(cost))
            {
                GoldManager.instance.changeGold(-cost);
            }
            return GoldManager.instance.canAfford(cost); 
        }
        return false;
    }

    public void assignTexts()
    {
        levelText.text = "Lvl: " + level;
        if(level < upgradePrice.Count)
            costText.text = upgradePrice[level] + "";
        else
            costText.text = "MAX";
    }
}
