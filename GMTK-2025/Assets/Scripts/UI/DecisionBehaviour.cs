using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DecisionBehaviour : MonoBehaviour
{
    public UnitInstance thisUnit;
    public Image classIcon;

    public Button runButton;
    public Button deployButton;

    public TrainingManager trainingManager;

    public void init(UnitInstance unit, TrainingManager manager)
    {
        thisUnit = unit;
        classIcon.sprite = thisUnit.archetype.icon;
        trainingManager = manager;
        runButton.onClick.AddListener(RunNextLap);
        deployButton.onClick.AddListener(Deploy);
    }


    void RunNextLap()
    {
        trainingManager.startRunning(thisUnit);
        trainingManager.unitInstancesAwaitingDecision.Remove(thisUnit);
        Destroy(gameObject);
    }

    void Deploy()
    {
        trainingManager.deployUnit(thisUnit);
        trainingManager.unitInstancesAwaitingDecision.Remove(thisUnit);
        Destroy(thisUnit.gameObject);
        Destroy(gameObject);
    }
}
