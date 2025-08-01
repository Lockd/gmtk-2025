using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitInstance : MonoBehaviour
{
    public UnitSO archetype;
    public int currentLevel = 0;

    private UnitCosmetics cosmetics;
    private HealthComponent hp;

    private void Awake()
    {
        cosmetics = GetComponent<UnitCosmetics>();
        hp = GetComponent<HealthComponent>();
        hp.onDeath.AddListener(onDeath);
    }

    public void init(UnitSO archetype)
    {
        this.archetype = archetype;
        currentLevel = 0;
        cosmetics.setSprite(archetype.spritesPerLevel[currentLevel]);
    }

    public void onLevelUp()
    {
        currentLevel = Mathf.Clamp(currentLevel + 1, 0, archetype.maxLevel);
        hp.onChangeMaxHP(archetype.healthPerLevel);
        if (currentLevel != 1) cosmetics.setSprite(archetype.spritesPerLevel[currentLevel]);
    }

    private void onDeath()
    {
        // TODO delete sprites and signal to the global manager
    }
}
