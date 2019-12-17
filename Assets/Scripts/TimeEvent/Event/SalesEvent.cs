using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class SalesEvent : TimeEvent
{
    [SerializeField] private float promotions;
    public override void StartEvent()
    {
        index = "Sales";
        GameManager.Instance.StatsManagerInstance.SalesMultiplier = promotions;
    }

    public override void EndEvent()
    {
        GameManager.Instance.StatsManagerInstance.SalesMultiplier = 1;
    }
}
