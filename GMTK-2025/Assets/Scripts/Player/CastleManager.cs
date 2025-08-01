using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastleManager : MonoBehaviour
{
    public HealthComponent castleHealth;
    public int castleMaxHealth;

    public float castlePushForce;
    // Start is called before the first frame update
    void Start()
    {
        castleHealth.init(castleMaxHealth);
    }

}
