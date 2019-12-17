using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Config : MonoBehaviour
{
    private float mainVolume;
    private float musicVolume;
    private float soundVolume;
    private float alertVolume;

    [SerializeField] private Slider MainSlider;
    [SerializeField] private Slider MusicSlider;
    [SerializeField] private Slider SoundSlider;
    [SerializeField] private Slider AlertSlider;

    [SerializeField] private Text TxtMain;
    [SerializeField] private Text TxtMusic;
    [SerializeField] private Text TxtSound;
    [SerializeField] private Text TxtAlert;

    [SerializeField] private AudioMixer MixerAudio;

    private const int MAX_DECIBEL = 80;

    // Start is called before the first frame update
    void Start()
    {
        if (GameObject.FindObjectsOfType<ObjectDontDestroy>().Length == 1)
        {
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this);
        }
    }

    public void SetVolume()
    {
        if (MainSlider.value != mainVolume)
        {
            mainVolume = (int)MainSlider.value;
            TxtMain.text = mainVolume.ToString();
            MixerAudio.SetFloat("Master", (mainVolume / 100 * MAX_DECIBEL) - MAX_DECIBEL);
        }

        if (MusicSlider.value != musicVolume)
        {
            musicVolume = (int)MusicSlider.value;
            TxtMusic.text = musicVolume.ToString();
            MixerAudio.SetFloat("Music", (musicVolume / 100 * MAX_DECIBEL) - MAX_DECIBEL);
        }

        if (SoundSlider.value != soundVolume)
        {
            soundVolume = (int)SoundSlider.value;
            TxtSound.text = soundVolume.ToString();
            MixerAudio.SetFloat("Sound", (soundVolume / 100 * MAX_DECIBEL) - MAX_DECIBEL);
        }

        if (AlertSlider.value != alertVolume)
        {
            alertVolume = (int)AlertSlider.value;
            TxtAlert.text = alertVolume.ToString();
            MixerAudio.SetFloat("Alert", (alertVolume / 100 * MAX_DECIBEL) - MAX_DECIBEL);
        }
    }


}
