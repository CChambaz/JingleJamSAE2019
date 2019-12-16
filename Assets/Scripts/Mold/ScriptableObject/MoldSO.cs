using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu]
public class MoldSO : ScriptableObject
{
    [SerializeField] private float maxTimer;
    public float MaxTimer
    {
        get => maxTimer;
        set => maxTimer = value;
    }
    [SerializeField] private float timerSpeed;
    public float TimerSpeed
    {
        get => timerSpeed;
        set => timerSpeed = value;
    }
    [SerializeField] private int ballRate;
    public int BallRate
    {
        get => ballRate;
        set => ballRate = value;
    }
    [SerializeField] private int snowCost;
    public int SnowCost
    {
        get => snowCost;
        set => snowCost = value;
    }
}
