using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitFighter : MonoBehaviour
{
    public GameObject currentTarget;
    private float enemiesCheckTimer = 0f;
    float moveSpeed;
    Vector2 startingPosition;

    [Header("Modules")]
    [SerializeField] private UnitDetectionModule detection;

    [Header("References")]
    [SerializeField] private UnitInstance unitInstance;
    [SerializeField] private ActionsManager actionsManager;

    void Start()
    {
        startingPosition = transform.position;
        moveSpeed = unitInstance.archetype.moveSpeed;
        actionsManager.actionSheet = unitInstance.archetype.actionSheet;
    }

    void Update()
    {
        if (currentTarget != null)
        {
            actionsManager.PerformAction(currentTarget.gameObject);
        }
        if (currentTarget != null)
        {
            transform.position = Vector2.MoveTowards(transform.position, currentTarget.transform.position, moveSpeed * Time.deltaTime);
        }
        else if (currentTarget == null && Vector2.Distance(transform.position, startingPosition) > 0.1f)
        {
            // TODO we should not move directly to the point, just towards the collider, to a shortest distance
            transform.position = Vector2.MoveTowards(transform.position, CastleManager.instance.transform.position, moveSpeed * Time.deltaTime);
        }

        if (currentTarget == null && Time.time >= enemiesCheckTimer)
        {
            lookForTarget();
            enemiesCheckTimer = Time.time + 0.5f;
        }
    }

    public void lookForTarget()
    {
        moveSpeed = 0;
        Debug.Log("detection" + detection);
        currentTarget = detection.getTarget();
    }

    public void OnEnemyDetectionEnd()
    {
        moveSpeed = unitInstance.archetype.moveSpeed;
    }

    void PerformAction()
    {
        if (currentTarget != null)
        {
            actionsManager.PerformAction(currentTarget.gameObject);
        }
        else
        {
            OnEnemyDetectionEnd();
        }
    }
}
