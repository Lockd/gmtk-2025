using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RecruitButton : MonoBehaviour
{
    public UnitSO thisUnit;
    Button thisButton;
    public TMP_Text priceText;

    void Start()
    {
        thisButton = GetComponent<Button>();
        thisButton.onClick.AddListener(Recruit);
        priceText.text = thisUnit.purchasePrice.ToString();
    }

    void Recruit()
    {
        int cost = thisUnit.purchasePrice;
        if (GoldManager.instance.canAfford(cost) && TrainingManager.instance.canSpawnMoreUnits())
        {
            GoldManager.instance.changeGold(-cost);
            TrainingManager.instance.onSpawnTrainingUnit(thisUnit);
            // Play the sound and adjust parameters as needed
            FMOD.Studio.EventInstance sound = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/UI/upgrades");
            sound.setVolume(MusicManager.instance.volume);
            sound.start();
            sound.release();
        }
    }
}
