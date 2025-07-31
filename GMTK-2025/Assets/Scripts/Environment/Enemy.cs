using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int maxHealth;
    public int currentHealth;
    public int moveSpeed;
    public int attackDamage;

    public Transform target;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);

        if (Vector2.Distance(transform.position, target.position) <= 3.5f)
        {
            moveSpeed = 0;
        }
    }
}
