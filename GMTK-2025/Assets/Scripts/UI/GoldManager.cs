using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GoldManager : MonoBehaviour
{
    [SerializeField] private TMP_Text goldText;
    public int gold = 0;
    public static GoldManager instance { get; private set; }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        goldText.text = "Gold: " + gold;
    }

    public void changeGold(int amount)
    {
        gold = Mathf.Min(gold + amount, int.MaxValue);
        goldText.text = "Gold: " + gold;
    }

    public bool canAfford(int cost)
    {
        return gold >= cost;
    }
}
