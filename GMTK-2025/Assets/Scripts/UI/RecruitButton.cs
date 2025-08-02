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

    void Start()
    {
        thisButton = GetComponent<Button>();
        thisButton.onClick.AddListener(Recruit);
    }

    void Recruit()
    {
        trainingManager.onSpawnTrainingUnit(thisUnit);
    }
}
