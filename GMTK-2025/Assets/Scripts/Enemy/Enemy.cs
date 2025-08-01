using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int maxHealth;
    public int currentHealth;
    public float maxMoveSpeed;
    public float moveSpeed;
    public int attackDamage;

    public Transform target;

    bool inRange;
    void Start()
    {
        moveSpeed = maxMoveSpeed;
        maxHealth = Random.Range(10, 18);
        currentHealth = maxHealth;
    }

    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, new Vector2(target.position.x, transform.position.y), moveSpeed * Time.deltaTime);

        /*
        if (Vector2.Distance(transform.position, target.position) <= 2f)
        {
            moveSpeed = 0;
        }*/
    }

    void DealDamage(HealthComponent target)
    {
        // Hardcoded damage is wrong
        target.onChangeHP(-5);
    }

    // Grisha: damage code can be reused
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Gate"))
        {
            HealthComponent castleHealth = collision.GetComponent<HealthComponent>();
            CastleManager castleManager = target.GetComponent<CastleManager>();
            DealDamage(castleHealth);
            transform.position = new Vector2(transform.position.x + castleManager.castlePushForce, transform.position.y);
            moveSpeed = 0;
        }

        if (collision.CompareTag("Defender"))
        {
            HealthComponent defenderHealth = collision.GetComponent<HealthComponent>();
            //DealDamage(defenderHealth);
            moveSpeed = 0;
        }
    }
    // Grisha: ???
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Gate"))
        {
            moveSpeed = maxMoveSpeed;
        }
    }
}
