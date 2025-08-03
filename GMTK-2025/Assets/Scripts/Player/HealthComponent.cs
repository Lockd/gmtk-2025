using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using JetBrains.Annotations;

public class HealthComponent : MonoBehaviour
{
    public bool isDead = false;
    public UnityEvent onDeath;
    public int currentHealth;
    public int maxHealth;
    public Slider hpSlider;
    public TextMeshProUGUI hpText;

    [Header("Damage Text")]
    [SerializeField] private TMP_Text damageTextPrefab;
    [SerializeField] private Transform textSpawnPoint;

    public void init(int maxHealth)
    {
        this.maxHealth = maxHealth;
        currentHealth = maxHealth;

        updateUI();
    }

    public void onChangeHP(int amount)
    {
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);

        if (damageTextPrefab && textSpawnPoint)
        {
            TMP_Text damageText = Instantiate(damageTextPrefab, textSpawnPoint.position, Quaternion.identity, textSpawnPoint);
            damageText.text = amount.ToString();
            damageText.color = amount < 0 ? Color.red : Color.green;
            damageText.DOFade(0, 1f).OnComplete(() => Destroy(damageText.gameObject));
        }

        if (currentHealth <= 0)
        {
            isDead = true;
            onDeath.Invoke();
        }
        updateUI();
    }

    public void onChangeMaxHP(int newMaxHP)
    {
        UnitInstance unitInstance = GetComponent<UnitInstance>();
        float healthMultiplier = 0;
        bool hasHealthUpgrade = UpgradesManager.instance.healthUpgrades.ContainsKey(unitInstance.archetype);
        if (unitInstance && hasHealthUpgrade)
        {
            healthMultiplier = UpgradesManager.instance.healthUpgrades[unitInstance.archetype];
        }
        maxHealth = (int)(newMaxHP * (1 + healthMultiplier));
        currentHealth = maxHealth;
        updateUI();
    }

    private void updateUI()
    {
        if (hpSlider)
        {
            hpSlider.maxValue = maxHealth;
            hpSlider.value = currentHealth;
        }
        if (hpText) hpText.text = currentHealth + "/" + maxHealth;
    }
}


