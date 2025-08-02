using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitInstance : MonoBehaviour
{
    public UnitSO archetype;
    public int currentLevel = 0;

    public UnitCosmetics cosmetics;
    public HealthComponent hp;

    private void Awake()
    {
        cosmetics = GetComponent<UnitCosmetics>();
        hp = GetComponent<HealthComponent>();
    }

    public void init(UnitSO archetype)
    {
        this.archetype = archetype;
        currentLevel = 0;
        cosmetics.setSprite(archetype.spritesPerLevel[currentLevel]);
        hp.init(archetype.health[currentLevel]);
    }

    public void onLevelUp()
    {
        currentLevel = Mathf.Clamp(currentLevel + 1, 0, archetype.maxLevel);
        hp.onChangeMaxHP(archetype.health[currentLevel]);
        if (currentLevel != 1) cosmetics.setSprite(archetype.spritesPerLevel[currentLevel - 1]);
    }
}
