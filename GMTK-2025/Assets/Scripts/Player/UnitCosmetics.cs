using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitCosmetics : MonoBehaviour
{
    private UnitInstance unit;
    [SerializeField] private SpriteRenderer spriteRenderer;

    void Awake()
    {
        unit = GetComponent<UnitInstance>();
    }

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
}
