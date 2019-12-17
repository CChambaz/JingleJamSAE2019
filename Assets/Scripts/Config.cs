using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Config : MonoBehaviour
{
    [Header("Menu Audio")]
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

    [Header("Audio")]
    [SerializeField] private AudioMixer MixerAudio;

    [SerializeField] AudioMixerGroup sound;
    [SerializeField] AudioMixerGroup alert;

    [Header("Leprechaun Happy")]
    [SerializeField] private AudioClip[] happy;

    [Header("Leprechaun unhappy")]
    [SerializeField] private AudioClip[] unhappy;

    [SerializeField] private GameObject prefabClip;

    private AudioSource[] audioList;

    private const int MAX_DECIBEL = 80;

    // Start is called before the first frame update
    void Start()
    {
        if (GameObject.FindObjectsOfType<Config>().Length == 1)
        {
            audioList = GetComponents<AudioSource>();
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this);
        }
    }

    private float LinearToDecibel(float linear)
    {
        float dB;

        if (linear != 0)
            dB = 20.0f * Mathf.Log10(linear);
        else
            dB = -144.0f;

        return dB;
    }

    public void SetVolume()
    {
        if (MainSlider.value != mainVolume)
        {
            mainVolume = (int)MainSlider.value;
            TxtMain.text = mainVolume.ToString();
            MixerAudio.SetFloat("Master", LinearToDecibel(mainVolume/100));
        }

        if (MusicSlider.value != musicVolume)
        {
            musicVolume = (int)MusicSlider.value;
            TxtMusic.text = musicVolume.ToString();
            MixerAudio.SetFloat("Music", LinearToDecibel(musicVolume / 100));
           
        }

        if (SoundSlider.value != soundVolume)
        {
            soundVolume = (int)SoundSlider.value;
            TxtSound.text = soundVolume.ToString();
            MixerAudio.SetFloat("Sound", LinearToDecibel(soundVolume / 100));
        }

        if (AlertSlider.value != alertVolume)
        {
            alertVolume = (int)AlertSlider.value;
            TxtAlert.text = alertVolume.ToString();
            MixerAudio.SetFloat("Alert", LinearToDecibel(alertVolume / 100));
        }
    }

    public enum SoundType
    {
        HAPPY = 0,
        UNHAPPY = 1
    }

    public void PlayAudio(SoundType type)
    {
        int audio = CheckFreeAudioSource();
        int result;
        switch (type)
        {

            case SoundType.HAPPY:
                result = Random.Range(0, happy.Length);
                audioList[audio].outputAudioMixerGroup = sound;
                audioList[audio].PlayOneShot(happy[result]);
                break;

            case SoundType.UNHAPPY:
                result = Random.Range(0, unhappy.Length);
                audioList[audio].outputAudioMixerGroup = sound;
                audioList[audio].PlayOneShot(unhappy[result]);
                break;
        }
    }

    public void Play(int type)
    {
        int audio = CheckFreeAudioSource();
        int result;
        switch (type)
        {

            case (int)SoundType.HAPPY:
                result = Random.Range(0, happy.Length);
                audioList[audio].outputAudioMixerGroup = sound;
                audioList[audio].PlayOneShot(happy[result]);
                break;

            case (int)SoundType.UNHAPPY:
                result = Random.Range(0, unhappy.Length);
                audioList[audio].outputAudioMixerGroup = sound;
                audioList[audio].PlayOneShot(unhappy[result]);
                break;
        }
    }

    private int CheckFreeAudioSource()
    {
        
        for (int i = 0; i < audioList.Length; i++)
        {
            if (!audioList[i].isPlaying)
            {
                Debug.Log(i);
                return i;
            }
        }
        return 0;
    }


}
