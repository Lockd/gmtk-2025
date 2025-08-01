using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainingManager : MonoBehaviour
{
    public List<UnitSO> unitTypes = new List<UnitSO>();
    public List<UnitInstance> unitInstances = new List<UnitInstance>();

    public static TrainingManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void onStartTraining(UnitSO unitType)
    {

    }
}
