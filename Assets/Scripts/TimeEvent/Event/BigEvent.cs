using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu]
public class BigEvent : TimeEvent
{
    [SerializeField] private float clientLost;
    [SerializeField] private float moldmodifier;
    public override void StartEvent()
    {
        if (Name == "Tornado")
        {
            MotherFuckingAudioManager.Instance.PlayMusic(MotherFuckingAudioManager.MusicList.TORNADO, true);
        }
        if (Name == "NuclearWar")
        {
            MotherFuckingAudioManager.Instance.PlayMusic(MotherFuckingAudioManager.MusicList.NUCK, true);
        }
        if (Name == "MassExtinction")
        {
            MotherFuckingAudioManager.Instance.PlayMusic(MotherFuckingAudioManager.MusicList.NUCK, true);
        }
        GameManager.Instance.StatsManagerInstance.ClientModifier = clientLost;
        GameManager.Instance.StatsManagerInstance.MoldTimeMultiplier = moldmodifier;
    }

    public override void EndEvent()
    {
        GameManager.Instance.StatsManagerInstance.ClientModifier = 1;
        GameManager.Instance.StatsManagerInstance.MoldTimeMultiplier = 1;
    }
}
