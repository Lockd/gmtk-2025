using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class HealthComponent : MonoBehaviour
{
    public bool isDead = false;
    public UnityEvent onDeath;
    public UnityEvent onGetDamage;
    public int currentHealth;
    public int maxHealth;

    [Header("UI")]
    [SerializeField] private TMP_Text damageTextPrefab;
    [SerializeField] private Transform textSpawnPoint;
    public Slider hpSlider;
    public TextMeshProUGUI hpText;
    [SerializeField] private TMP_Text damageText;


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
        float healthMultiplier = UpgradesManager.instance.getHealthMultiplier(unitInstance.archetype);
        maxHealth = (int)(newMaxHP * healthMultiplier);
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
        if (damageText)
        {
            UnitInstance unitInstance = GetComponent<UnitInstance>();
            UnitFighter unitFighter = GetComponent<UnitFighter>();

            if (!unitInstance || !unitFighter) return;

            float damageMultiplier = unitFighter.damageMultiplier;
            int level = Mathf.Max(0, unitInstance.currentLevel - 1);
            damageText.text = ((int)(unitInstance.archetype.attack[level] * damageMultiplier)).ToString();
        }
    }
}


