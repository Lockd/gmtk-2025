using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingManager : MonoBehaviour
{
    public Building thisBuilding;
    public Image buildingImage;
    public int currentLevel = -1;
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
        currentLevel++;
        if (currentLevel == 0)
        {
            buildingImage.sprite = thisBuilding.buildingSprite;
        }
    }
}
