using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradesManager : MonoBehaviour
{
    public float reduceLevelingSpeed = 0f;
    public static UpgradesManager instance { get; private set; }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }

    public void onChangeReduceLevelingSpeed(float speedBoost)
    {
        reduceLevelingSpeed += speedBoost;
    }
}
