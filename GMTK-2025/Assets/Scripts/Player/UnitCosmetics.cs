using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitCosmetics : MonoBehaviour
{
    public UnitInstance unitInstance;
    [SerializeField] private SpriteRenderer spriteRenderer;

    public void setSprite(Sprite sprite)
    {
        if (spriteRenderer != null)
        {
            spriteRenderer.sprite = sprite;
        }
        else
        {
            Debug.LogWarning("SpriteRenderer is not assigned in UnitCosmetics.");
        }
    }

    public void flipSprite(bool goingLeft)
    {
        if (unitInstance.archetype.isEnemy) return;

        spriteRenderer.flipX = goingLeft;
    }
}
