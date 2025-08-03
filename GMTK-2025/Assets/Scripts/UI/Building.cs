using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

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
    public UnitSO targetUnit;

    private void Awake()
    {
        costText.text = upgradePrice[level] + "";
    }

    public abstract void applyUpgrade();

    public bool canUpgrade()
    {
        if (level < upgradePrice.Count)
        {
            int cost = upgradePrice[level];
            if (GoldManager.instance.canAfford(cost))
            {
                GoldManager.instance.changeGold(-cost);
                return true;
            }
        }
        return false;
    }

    public void afterUpgrade()
    {
        Button button = GetComponent<Button>();
        if (level >= maxLevel && button != null)
        {
            button.interactable = false;
        }
        assignTexts();

        FMOD.Studio.EventInstance sound = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/UI/unit_purchase");
        sound.setVolume(MusicManager.instance.volume);
        sound.start();
        sound.release();
    }

    public void assignTexts()
    {
        levelText.text = level.ToString();
        if (level < upgradePrice.Count)
            costText.text = upgradePrice[level] + "";
        else
            costText.text = "MAX";
    }
}
