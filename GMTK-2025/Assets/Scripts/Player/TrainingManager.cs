using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class TrainingManager : MonoBehaviour
{
    public int maxHiredUnits = 5;
    public List<UnitSO> unitTypes = new List<UnitSO>();
    public GameObject trainingUnitPrefab;
    public GameObject fightingUnitPrefab;
    public List<UnitInstance> trainingUnits = new List<UnitInstance>();
    public List<UnitFighter> combatUnits = new List<UnitFighter>();
    [Header("UI")]
    [SerializeField] private TMP_Text trainingUnitsText;

    [Header("Running settings")]
    [SerializeField] private Transform startPoint;
    [SerializeField] private Transform midPoint;
    [SerializeField] private Transform combatPoint;

    public List<UnitInstance> unitInstancesAwaitingDecision = new List<UnitInstance>();

    public UnitDecisionManager decisionManager;

    public static TrainingManager instance { get; private set; }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void onSpawnTrainingUnit(UnitSO unitType)
    {
        GameObject unitObject = Instantiate(trainingUnitPrefab);
        unitObject.transform.position = startPoint.position;
        UnitInstance unit = unitObject.GetComponent<UnitInstance>();
        unit.init(unitType);
        trainingUnits.Add(unit);
        startRunning(unit);
        updateUI();
    }

    public void startRunning(UnitInstance unit)
    {
        float timePerSegment = (unit.archetype.levelUpTime - UpgradesManager.instance.reduceLevelingSpeed) / 4f;
        Sequence runningSequence = DOTween.Sequence();
        Vector2 topRight = startPoint.position;
        Vector2 bottomRight = new Vector2(startPoint.position.x, midPoint.position.y);
        Vector2 bottomLeft = midPoint.position;
        unit.animationHandler.playWalkingAnimation(true);
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
            unit.animationHandler.playWalkingAnimation(false);
        });
    }

    // TODO this is only need for testing
    private void spawnCombatUnit(UnitSO unitType)
    {
        GameObject unitObject = Instantiate(fightingUnitPrefab);
        UnitInstance unitInstance = unitObject.GetComponent<UnitInstance>();
        unitInstance.init(unitType);
        combatUnits.Add(unitInstance.GetComponent<UnitFighter>());
        unitInstance.onLevelUp();
        unitObject.transform.position = new Vector2(combatPoint.position.x + unitInstance.archetype.spawnOffset.x, combatPoint.position.y + unitInstance.archetype.spawnOffset.y);
    }

    public void deployUnit(UnitInstance unit)
    {
        GameObject unitObject = Instantiate(fightingUnitPrefab);
        UnitInstance unitInstance = unitObject.GetComponent<UnitInstance>();
        unitInstance.init(unit.archetype);
        for (int i = 0; i < unit.currentLevel; i++) unitInstance.onLevelUp();
        unitObject.transform.position = new Vector2(combatPoint.position.x + unit.archetype.spawnOffset.x, combatPoint.position.y + unit.archetype.spawnOffset.y);
        trainingUnits.Remove(unit);
        combatUnits.Add(unitObject.GetComponent<UnitFighter>());
    }

    public void onUnitDeath(UnitFighter unit)
    {
        combatUnits.Remove(unit);
        updateUI();
    }

    private void updateUI()
    {
        int maxUnits = maxHiredUnits + UpgradesManager.instance.additionalTrainingUnits;
        int currentUnits = trainingUnits.Count + combatUnits.Count;
        trainingUnitsText.text = currentUnits + " / " + maxUnits;
    }

    public bool canSpawnMoreUnits()
    {
        int maxUnits = maxHiredUnits + UpgradesManager.instance.additionalTrainingUnits;
        int currentUnits = trainingUnits.Count + combatUnits.Count;
        return currentUnits < maxUnits;
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
