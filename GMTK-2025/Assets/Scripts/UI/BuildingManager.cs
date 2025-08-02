using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class BuildingManager : MonoBehaviour
{
    public Building thisBuilding;
    public Image buildingImage;
    public int currentLevel = 0;

    public GameObject thisClassButton;
    public int baseUpgradeCost;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpgradeBuilding()
    {
        if (GoldManager.instance.canAfford(baseUpgradeCost + baseUpgradeCost * currentLevel))
        {
            GoldManager.instance.changeGold(-(baseUpgradeCost + baseUpgradeCost* currentLevel));
            currentLevel++;
            if (currentLevel == 1)
            {
                buildingImage.sprite = thisBuilding.buildingSprite;
                thisClassButton.gameObject.SetActive(true);
            }
        }
    }
}
