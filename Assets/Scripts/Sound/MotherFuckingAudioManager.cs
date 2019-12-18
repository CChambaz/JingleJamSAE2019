using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class MotherFuckingAudioManager : MonoBehaviour
{
    public static MotherFuckingAudioManager Instance;
    
    List<AudioSource> emitters = new List<AudioSource>();

    public enum SoundList
    {
        REACTION_HAPPY,
        REACTION_SAD
    }

    public enum MusicList
    {
        NONE,
        MAIN,
        NUCK,
        TORNADO
    }

    MusicList currentMusicPlaying = MusicList.NONE;
    
    [Header("Sounds")]
    [SerializeField] private AudioClip[] reactionHappy;
    [SerializeField] private AudioClip[] reactionSad;

    [Header("Musics")]
    [SerializeField] private AudioClip mainMusic;
    [SerializeField] private AudioClip nuckMusic;
    [SerializeField] private AudioClip tornadoMusic;

    [Header("Emmiters")]
    [SerializeField] private int emitterNumber;
    [SerializeField] private GameObject emitterPrefab;
    [SerializeField] private AudioSource musicEmitter;

    private void Awake()
    {
        if(Instance != this && Instance != null)
            Destroy(gameObject);
        else
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }

        for (int i = 0; i <= emitterNumber; i++)
        {
            GameObject audioObject = Instantiate(emitterPrefab, emitterPrefab.transform.position, emitterPrefab.transform.rotation);
            emitters.Add(audioObject.GetComponent<AudioSource>());
            DontDestroyOnLoad(audioObject);
        }

        musicEmitter = GetComponent<AudioSource>();
        PlayMusic(MusicList.MAIN);
    }

    public void PlaySound(SoundList sound)
    {
        AudioSource emitterAvailable = null;
        
        foreach (AudioSource emitter in emitters)
        {
            if (!emitter.isPlaying)
            {
                emitterAvailable = emitter;
                break;
            }
        }

        if (emitterAvailable != null)
        {
            emitterAvailable.loop = false;
            
            switch (sound)
            {
                case SoundList.REACTION_SAD:
                    emitterAvailable.clip = reactionSad[Random.Range(0, reactionSad.Length)];
                    emitterAvailable.outputAudioMixerGroup = AudioConfig.Instance.sound;
                    break;
                case SoundList.REACTION_HAPPY:
                    emitterAvailable.clip = reactionHappy[Random.Range(0, reactionHappy.Length)];
                    emitterAvailable.outputAudioMixerGroup = AudioConfig.Instance.sound;
                    break;
            }

            emitterAvailable.Play();
        }
        else
        {
            Debug.Log("no emitter available");
        }
    }

    public void PlayMusic(MusicList music)
    {
        if (musicEmitter == null || !musicEmitter.enabled)
            musicEmitter = GetComponent<AudioSource>();

        if (currentMusicPlaying != music)
        {
            musicEmitter.loop = true;

            switch (music)
            {
                case MusicList.MAIN:
                    musicEmitter.clip = mainMusic;
                    musicEmitter.Play();
                    break;
                case MusicList.NUCK:
                    musicEmitter.clip = nuckMusic;
                    musicEmitter.Play();
                    break;
                case MusicList.TORNADO:
                    musicEmitter.clip = tornadoMusic;
                    musicEmitter.Play();
                    break;
                case MusicList.NONE:
                    musicEmitter.Stop();
                    break;
            }

            currentMusicPlaying = music;
        }
    }
}
