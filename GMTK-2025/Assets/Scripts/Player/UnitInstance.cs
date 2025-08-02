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
        hp.init(archetype.health);
    }

    public void initOnDeploy(UnitInstance unit)
    {
        archetype = unit.archetype;
        currentLevel = unit.currentLevel; 
        cosmetics.setSprite(archetype.spritesPerLevel[currentLevel - 1]);
        hp.init(archetype.health);
    }

    public void onLevelUp()
    {
        currentLevel = Mathf.Clamp(currentLevel + 1, 0, archetype.maxLevel);
        hp.onChangeMaxHP(archetype.healthPerLevel);
        if (currentLevel != 1) cosmetics.setSprite(archetype.spritesPerLevel[currentLevel - 1]);
    }

    private void onDeath()
    {
        // TODO delete sprites and signal to the global manager
    }
}
