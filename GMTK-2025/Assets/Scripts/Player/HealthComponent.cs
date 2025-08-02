using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;

public class HealthComponent : MonoBehaviour
{
    public bool isDead = false;
    public UnityEvent onDeath;
    public int currentHealth;
    public int maxHealth;

    public Slider hpSlider;

    public TextMeshProUGUI hpText;

    public void init(int maxHealth)
    {
        this.maxHealth = maxHealth;
        currentHealth = maxHealth;
        if (hpSlider)
        {
            hpSlider.maxValue = maxHealth;
            hpSlider.value = currentHealth;
        }
        if (hpText) hpText.text = currentHealth + "/" + maxHealth;
    }

    public void onChangeHP(int amount)
    {
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);

        if (currentHealth <= 0)
        {
            isDead = true;
            onDeath.Invoke();
        }
        if (hpSlider) hpSlider.value = currentHealth;
        if (hpText) hpText.text = currentHealth + "/" + maxHealth;
    }

    public void onChangeMaxHP(int amount)
    {
        maxHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
    }
}


