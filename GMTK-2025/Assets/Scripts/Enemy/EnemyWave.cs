using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Wave", menuName = "Wave")]
public class EnemyWave : ScriptableObject
{
    public List<EnemyGroup> enemyGroup;
    public float intervalBetweenSpawns = 0.15f;
}

[Serializable]
public class EnemyGroup
{
    public int level = 1;
    public int count = 1;
    public int moneyPerUnit = 1;
    public UnitSO enemySO;
}