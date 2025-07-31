using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Wave", menuName = "Wave")]
public class EnemyWave : ScriptableObject
{
    public GameObject[] enemy;

    public int[] enemyPerInterval;

    public float interval;
}