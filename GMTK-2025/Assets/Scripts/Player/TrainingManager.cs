using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TrainingManager : MonoBehaviour
{
    public List<UnitSO> unitTypes = new List<UnitSO>();
    public GameObject unitPrefab;
    public List<UnitInstance> unitInstances = new List<UnitInstance>();
    [Header("Running settings")]
    [SerializeField] private Transform startPoint;
    [SerializeField] private Transform midPoint;

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
        GameObject unitObject = Instantiate(unitPrefab);
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
            unit.onLevelUp();
        });
    }

#if UNITY_EDITOR
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) onSpawnTrainingUnit(unitTypes[0]);
        if (Input.GetKeyDown(KeyCode.Alpha2)) onSpawnTrainingUnit(unitTypes[1]);
        if (Input.GetKeyDown(KeyCode.Alpha3)) onSpawnTrainingUnit(unitTypes[2]);
        if (Input.GetKeyDown(KeyCode.Alpha4)) onSpawnTrainingUnit(unitTypes[3]);
        if (Input.GetKeyDown(KeyCode.Alpha5)) onSpawnTrainingUnit(unitTypes[4]);
    }
#endif
}
