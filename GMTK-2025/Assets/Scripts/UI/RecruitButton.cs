using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class RecruitButton : MonoBehaviour
{
    public TrainingManager trainingManager;
    public UnitSO thisUnit;
    Button thisButton;

    public int cost;
    void Start()
    {
        thisButton = GetComponent<Button>();
        thisButton.onClick.AddListener(Recruit);
    }

    void Recruit()
    {
        if (GoldManager.instance.canAfford(cost))
        {
            GoldManager.instance.changeGold(-cost);
            trainingManager.onSpawnTrainingUnit(thisUnit);
        }
    }
}
