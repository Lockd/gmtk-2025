
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public EnemyWave[] waves;
    public float startSpawningAfter;
    public float wavesCooldown = 15f;
    public List<UnitFighter> spawnedEnemies = new List<UnitFighter>();

    [SerializeField] private GameObject enemyPrefab;

    float spawnTime;
    private int waveIdx = 0;
    public BoxCollider2D spawnLocation;

    public Transform tower;

    public static EnemySpawner instance { get; private set; }

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        spawnTime = Time.time + startSpawningAfter;
    }

    void Update()
    {
        if (Time.time >= spawnTime)
        {
            SpawnWave();
            spawnTime = Time.time + wavesCooldown;
        }
    }

    void SpawnWave()
    {
        Debug.Log("Should spawn new wave: " + waveIdx);
        EnemyWave w = waves[waveIdx];
        StartCoroutine(DeployEnemies(w));
        waveIdx = Mathf.Clamp(waveIdx + 1, 0, waves.Length - 1);
    }

    IEnumerator DeployEnemies(EnemyWave wave)
    {
        List<UnitFighter> enemies = new List<UnitFighter>();
        foreach (EnemyGroup group in wave.enemyGroup)
        {
            for (int i = 0; i < group.count; i++)
            {
                GameObject enemyObject = Instantiate(enemyPrefab, spawnLocation.transform);
                enemyObject.transform.position = RandomPointInBounds(spawnLocation.bounds);
                UnitFighter enemy = enemyObject.GetComponent<UnitFighter>();
                UnitInstance enemyInstance = enemyObject.GetComponent<UnitInstance>();
                enemyInstance.init(group.enemySO);
                enemy.lookForTarget();
                enemies.Add(enemy);
                spawnedEnemies.Add(enemy.GetComponent<UnitFighter>());
                for (int j = 0; j < group.level; j++) enemyInstance.onLevelUp();
                yield return new WaitForSeconds(wave.intervalBetweenSpawns * i);
            }
        }
    }

    static Vector2 RandomPointInBounds(Bounds bounds)
    {
        return new Vector2(
            Random.Range(bounds.min.x, bounds.max.x),
            Random.Range(bounds.min.y, bounds.max.y)
        );
    }

    public void onUnitDeath(UnitFighter unit)
    {
        spawnedEnemies.Remove(unit);
    }
}
