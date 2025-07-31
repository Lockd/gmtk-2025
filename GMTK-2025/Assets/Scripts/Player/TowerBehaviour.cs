using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TowerBehaviour : MonoBehaviour
{
    private float shootTimer = 0f;
    public float shootInterval = 1f;
    public GameObject projectile;

    public float projectileSpeed = 0f;

    public int projCount;

    public Transform shootPoint;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        shootTimer += Time.deltaTime;

        if (shootTimer >= shootInterval)
        {
            Shoot();
            shootTimer = 0f;
        }
    }

    void Shoot()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        if (enemies.Length == 0) return;


        GameObject closest = enemies[0];
        float closestDist = Mathf.Infinity;

        List<GameObject> sortedEnemies = enemies
            .OrderBy(e => Vector2.Distance(transform.position, e.transform.position))
            .ToList();

        int shots = Mathf.Min(projCount, sortedEnemies.Count);

        for (int i = 0; i < shots; i++)
        {
            GameObject enemy = sortedEnemies[i];

            GameObject proj = Instantiate(projectile, shootPoint.position, Quaternion.identity);

            Vector2 dir = (enemy.transform.position - transform.position).normalized;
            proj.GetComponent<Rigidbody2D>().velocity = dir * projectileSpeed;
        }


    }
}
