using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.IO.Compression;

public class TrainingManager : MonoBehaviour
{
    public List<UnitSO> unitTypes = new List<UnitSO>();
    public GameObject trainingUnitPrefab;
    public GameObject fightinggUnitPrefab;
    public List<UnitInstance> unitInstances = new List<UnitInstance>();
    [Header("Running settings")]
    [SerializeField] private Transform startPoint;
    [SerializeField] private Transform midPoint;
    [SerializeField] private Transform combatPoint;

    public List<UnitInstance> unitInstancesAwaitingDecision = new List<UnitInstance>();

    public UnitDecisionManager decisionManager;

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

    public void onSpawnTrainingUnit(UnitSO unitType)
    {
        GameObject unitObject = Instantiate(trainingUnitPrefab);
        unitObject.transform.position = startPoint.position;
        UnitInstance unit = unitObject.GetComponent<UnitInstance>();
        unit.init(unitType);
        unitInstances.Add(unit);
        startRunning(unit);
    }

    public void startRunning(UnitInstance unit)
    {
        float timePerSegment = unit.archetype.levelUpTime / 4f;
        Sequence runningSequence = DOTween.Sequence();
        Vector2 topRight = startPoint.position;
        Vector2 bottomRight = new Vector2(startPoint.position.x, midPoint.position.y);
        Vector2 bottomLeft = midPoint.position;
        Vector2 topLeft = new Vector2(midPoint.position.x, startPoint.position.y);
        runningSequence.Append(unit.transform.DOMove(bottomRight, timePerSegment).SetEase(Ease.Linear));
        runningSequence.Append(unit.transform.DOMove(bottomLeft, timePerSegment).SetEase(Ease.Linear));
        runningSequence.Append(unit.transform.DOMove(topLeft, timePerSegment).SetEase(Ease.Linear));
        runningSequence.Append(unit.transform.DOMove(topRight, timePerSegment).SetEase(Ease.Linear));
        runningSequence.OnComplete(() =>
        {
            unitInstancesAwaitingDecision.Add(unit);
            decisionManager.SetDecisions();
            unit.onLevelUp();
        });
    }

    // TODO this is only need for testing
    private void spawnCombatUnit(UnitSO unitType)
    {
        GameObject unitObject = Instantiate(fightinggUnitPrefab);
        UnitInstance unitInstance = unitObject.GetComponent<UnitInstance>();
        unitInstance.init(unitType);
        unitInstances.Add(unitInstance);
        unitInstance.onLevelUp();
        unitObject.transform.position = new Vector2(combatPoint.position.x + unitInstance.archetype.spawnOffset.x, combatPoint.position.y + unitInstance.archetype.spawnOffset.y);
    }

    public void deployUnit(UnitInstance unit)
    {
        GameObject unitObject = Instantiate(fightinggUnitPrefab);
        UnitInstance unitInstance = unitObject.GetComponent<UnitInstance>();
        unitInstance.initOnDeploy(unit);
        unitObject.transform.position = new Vector2(combatPoint.position.x + unit.archetype.spawnOffset.x, combatPoint.position.y + unit.archetype.spawnOffset.y);
    } 

#if UNITY_EDITOR
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) onSpawnTrainingUnit(unitTypes[0]);
        if (Input.GetKeyDown(KeyCode.Alpha2)) onSpawnTrainingUnit(unitTypes[1]);
        if (Input.GetKeyDown(KeyCode.Alpha3)) onSpawnTrainingUnit(unitTypes[2]);
        if (Input.GetKeyDown(KeyCode.Alpha4)) onSpawnTrainingUnit(unitTypes[3]);
        if (Input.GetKeyDown(KeyCode.Alpha5)) onSpawnTrainingUnit(unitTypes[4]);
        if (Input.GetKeyDown(KeyCode.F1)) spawnCombatUnit(unitTypes[0]);
        if (Input.GetKeyDown(KeyCode.F2)) spawnCombatUnit(unitTypes[1]);
        if (Input.GetKeyDown(KeyCode.F3)) spawnCombatUnit(unitTypes[2]);
        if (Input.GetKeyDown(KeyCode.F4)) spawnCombatUnit(unitTypes[3]);
        if (Input.GetKeyDown(KeyCode.F5)) spawnCombatUnit(unitTypes[4]);
    }
#endif
}
