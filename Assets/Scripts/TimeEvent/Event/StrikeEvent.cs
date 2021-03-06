﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu]
public class StrikeEvent : TimeEvent
{
    [SerializeField] private float moldAccelerations;
    [SerializeField] private float promotions;
    public override void StartEvent()
    {
        index = "Strike";
        GameManager.Instance.StatsManagerInstance.MoldTimeMultiplier = moldAccelerations;
        GameManager.Instance.StatsManagerInstance.SalesMultiplier = promotions;
    }

    public override void EndEvent()
    {
        GameManager.Instance.StatsManagerInstance.MoldTimeMultiplier = 1;
        GameManager.Instance.StatsManagerInstance.SalesMultiplier = 1;
    }
}
