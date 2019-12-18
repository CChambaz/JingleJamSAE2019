using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
[CreateAssetMenu]
public class ClimatEvent : TimeEvent
{
    public enum ClimatType
    {
        TORNADO
    }
    [SerializeField] private ClimatType weathertype;
    public override void StartEvent()
    {
        index = "Climat";
        switch (weathertype)
        {
            case ClimatType.TORNADO:
                GameManager.Instance.StatsManagerInstance.SpawnTimeMultiplier = 0.1f;
                GameManager.Instance.StatsManagerInstance.Speed = 2;
                GameManager.Instance.StatsManagerInstance.FallingSpeed = 0.1f;
                GameManager.Instance.StatsManagerInstance.SpawningSize = 1;
                GameManager.Instance.StatsManagerInstance.TempestForce = 2;
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    public override void EndEvent()
    {
        GameManager.Instance.StatsManagerInstance.SpawnTimeMultiplier = 1;
        GameManager.Instance.StatsManagerInstance.Speed = 1;
        GameManager.Instance.StatsManagerInstance.SpawningSize = 1;
        GameManager.Instance.StatsManagerInstance.TempestForce = 0;
    }
}
