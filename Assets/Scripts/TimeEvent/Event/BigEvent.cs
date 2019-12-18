using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu]
public class BigEvent : TimeEvent
{
    [SerializeField] private float clientLost;
    public override void StartEvent()
    {
        GameManager.Instance.StatsManagerInstance.ClientModifier = clientLost;
    }

    public override void EndEvent()
    {
        GameManager.Instance.StatsManagerInstance.ClientModifier = 1;
    }
}
