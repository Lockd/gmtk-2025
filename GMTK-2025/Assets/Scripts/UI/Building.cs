using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Diagnostics.Contracts;
using Unity.VisualScripting;
using UnityEngine.EventSystems;

public abstract class Building : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
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

    public GameObject descriptionBox;
    public TextMeshProUGUI descriptionText;
    public Image descriptionIcon;

    public string buildingDescription;

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
    }

    public void assignTexts()
    {
        levelText.text = level.ToString();
        if (level < upgradePrice.Count)
            costText.text = upgradePrice[level] + "";
        else
            costText.text = "MAX";
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        descriptionBox.SetActive(true);
        descriptionText.text = buildingDescription;
        descriptionIcon.sprite = transform.GetChild(0).GetComponent<Image>().sprite;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        descriptionBox.SetActive(false);
    }
}
