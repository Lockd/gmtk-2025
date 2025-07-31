
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public EnemyWave[] waves;
    public float waveCadence;
    float internalCd;

    int wave;

    public BoxCollider2D[] spawnLocations;

    public Transform tower;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= internalCd)
        {
            SpawnWave();
            internalCd = Time.time + waveCadence;
        }
    }

    void SpawnWave()
    {
        EnemyWave w = waves[wave];
        for (int i = 0; i < w.enemy.Length; i++)
        {
            StartCoroutine(DeployEnemies(i, w));
        }
        wave++;
        if (wave >= waves.Length)
        {
            wave = 0;
        }
    }

    IEnumerator DeployEnemies(int subWave, EnemyWave wave)
    {
        yield return new WaitForSeconds(wave.interval * subWave);
        for (int i = 0; i < wave.enemyPerInterval[subWave]; i++)
        {
            int index = Random.Range(0, 4);
            BoxCollider2D col = spawnLocations[index];
            Vector2 spawn = RandomPointInBounds(col.bounds);
            GameObject obj = Instantiate(wave.enemy[subWave], spawn, Quaternion.identity);
            obj.GetComponent<Enemy>().target = tower;
        }
    }

    static Vector2 RandomPointInBounds(Bounds bounds)
    {
        return new Vector2(
            Random.Range(bounds.min.x, bounds.max.x),
            Random.Range(bounds.min.y, bounds.max.y)
        );
    }


}
