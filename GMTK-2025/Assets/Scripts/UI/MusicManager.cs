using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public float volume = 1;
    public static MusicManager instance { get; private set; }
    private static FMOD.Studio.EventInstance Music;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        Music = FMODUnity.RuntimeManager.CreateInstance("event:/Music/Music");
        Music.start();
        Music.release();
        Music.setParameterByName("Music", 0);
    }

    public void SetMusicIntensity(int intensity)
    {
        Music.setParameterByName("Music", intensity);
    }

    public void SetVolume(float volume)
    {
        this.volume = volume;
        Music.setVolume(volume);
    }
}
