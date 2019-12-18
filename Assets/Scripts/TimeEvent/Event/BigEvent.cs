using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu]
public class BigEvent : TimeEvent
{
    [SerializeField] private float clientLost;
    public override void StartEvent()
    {
        if (Name == "Tornado")
        {
            MotherFuckingAudioManager.Instance.PlaySound(MotherFuckingAudioManager.SoundList.TORNADO);
            MotherFuckingAudioManager.Instance.PlayMusic(MotherFuckingAudioManager.MusicList.TORNADO);
        }
        if (Name == "NuclearWar")
        {
            MotherFuckingAudioManager.Instance.PlayMusic(MotherFuckingAudioManager.MusicList.TORNADO);
        }
        GameManager.Instance.StatsManagerInstance.ClientModifier = clientLost;
    }

    public override void EndEvent()
    {
        GameManager.Instance.StatsManagerInstance.ClientModifier = 1;
    }
}
