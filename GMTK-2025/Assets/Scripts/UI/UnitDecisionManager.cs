using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitDecisionManager : MonoBehaviour
{
    public TrainingManager trainingManager;
    public Transform decisionGrid;
    public GameObject decisionPrefab;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void SetDecisions()
    {
        foreach(Transform t in decisionGrid)
        {
            Destroy(t.gameObject);
        }
        foreach(UnitInstance unit in trainingManager.unitInstancesAwaitingDecision)
        {
            GameObject decision = Instantiate(decisionPrefab, decisionGrid);
            decision.GetComponent<DecisionBehaviour>().init(unit, trainingManager);
        }
    }
}
