using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastleManager : MonoBehaviour
{
    public HealthComponent castleHealth;
    public int castleMaxHealth;

    public float castlePushForce;

    public static CastleManager instance { get; private set; }

    private void Awake()
    {
        if (instance != null) Destroy(gameObject);
        else instance = this;
    }

    void Start()
    {
        castleHealth.init(castleMaxHealth);
    }

}
