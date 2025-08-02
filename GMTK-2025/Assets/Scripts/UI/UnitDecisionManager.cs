using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// UI component for choosing the next action for a TRAINING unit
public class UnitDecisionManager : MonoBehaviour
{
    public TrainingManager trainingManager;
    public Transform decisionGrid;
    public GameObject decisionPrefab;

    public void SetDecisions()
    {
        foreach (Transform t in decisionGrid)
        {
            Destroy(t.gameObject);
        }
        foreach (UnitInstance unit in trainingManager.unitInstancesAwaitingDecision)
        {
            GameObject decision = Instantiate(decisionPrefab, decisionGrid);
            decision.GetComponent<DecisionBehaviour>().init(unit, trainingManager);
        }
    }
}
