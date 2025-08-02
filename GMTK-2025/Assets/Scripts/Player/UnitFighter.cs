using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;

public class UnitFighter : MonoBehaviour
{
    public GameObject currentTarget;

    public UnitSO archetype;

    public CircleCollider2D enemyDetection;

    float enemiesCheckTimer = 0f;

    Transform dir;

    float moveSpeed;

    Vector2 startingPosition;

    bool isEnemy;

    public ActionsManager actionsManager;
    // Start is called before the first frame update
    void Start()
    {
        archetype = GetComponent<UnitInstance>().archetype;
        enemyDetection.radius = archetype.detectionRadius;
        dir = GameObject.Find("TEMP_MovementDir").transform;
        startingPosition = transform.position;
        moveSpeed = archetype.moveSpeed;
        actionsManager.actionSheet = archetype.actionSheet;
    }


    bool enemyPresent;
    // Update is called once per frame
    void Update()
    {
        if(currentTarget != null)
        {
            actionsManager.PerformAction(currentTarget);
        }
        if (enemyPresent)
        {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(dir.position.x, transform.position.y), moveSpeed * Time.deltaTime);
        }
        else if (!enemyPresent && Vector2.Distance(transform.position, startingPosition) > 0.1f)
        {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(-dir.position.x, transform.position.y), moveSpeed * Time.deltaTime);
        }
        if (Time.time >= enemiesCheckTimer)
        {
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            if(enemies.Length > 0)
            {
                enemyPresent = true;
            }
            else
            {
                enemyPresent = false;
            }
            enemiesCheckTimer = Time.time + 0.5f;
        }
    }

    public void OnEnemyDetection(GameObject target)
    {
        ChooseTarget(target);
        moveSpeed = 0;
    }

    public void OnEnemyDetectionEnd()
    {
        moveSpeed = archetype.moveSpeed;
    }


    void ChooseTarget(GameObject t)
    {
        currentTarget = t;
    }

    void PerformAction()
    {
        if (currentTarget != null)
        {
            actionsManager.PerformAction(currentTarget);
        }
        else
        {
            OnEnemyDetectionEnd();
        }
    }
}
