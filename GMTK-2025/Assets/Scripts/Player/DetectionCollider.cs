using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class DetectionCollider : MonoBehaviour
{
    public UnitFighter thisUnit;

    public Enemy thisEnemy;

    public string targetType;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(targetType))
        {
            if (thisUnit != null)
            {
                thisUnit.OnEnemyDetection(collision.gameObject);
            }
            if (thisEnemy != null)
            {
                thisEnemy.moveSpeed = 0 ;
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag(targetType))
        {
            if (thisUnit != null)
            {
                thisUnit.Invoke("OnEnemyDetectionEnd", 0.25f);
            }
            if (thisEnemy != null)
            {
                thisEnemy.moveSpeed = thisEnemy.maxMoveSpeed;
            }
        }
    }
}
