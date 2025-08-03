using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Slider volumeSlider;
    public Button startButton;
    public RectTransform victoryText;
    void Start()
    {
        startButton.onClick.AddListener(OnStartButtonClicked);
        volumeSlider.onValueChanged.AddListener(OnVolumeChanged);

        int lastWaveCompleted = PlayerPrefs.GetInt("lastWaveCompleted", 0);
        if (lastWaveCompleted == 1)
        {
            victoryText.gameObject.SetActive(true);
        }
        else
        {
            victoryText.gameObject.SetActive(false);
        }
    }

    private void OnStartButtonClicked()
    {
        TransitionManager.instance.transitionGame();
    }

    private void OnVolumeChanged(float value)
    {
        MusicManager.instance.SetVolume(value);
    }
}