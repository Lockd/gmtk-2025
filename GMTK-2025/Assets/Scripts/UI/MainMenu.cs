using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Slider volumeSlider;
    public Button startButton;
    void Start()
    {
        startButton.onClick.AddListener(OnStartButtonClicked);
        volumeSlider.onValueChanged.AddListener(OnVolumeChanged);
    }

    private void OnStartButtonClicked()
    {
        TransitionManager.instance.transitionCenterToRight();
    }

    private void OnVolumeChanged(float value)
    {
        MusicManager.instance.SetVolume(value);
    }
}