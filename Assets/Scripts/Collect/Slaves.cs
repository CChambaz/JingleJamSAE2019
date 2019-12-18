using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slaves : MonoBehaviour
{
    [SerializeField] int slavePrice = 30;
    public int SlavePrice
    {
        get => slavePrice;
        set => slavePrice = value;
    }
    int slavePriceAtStart;
    public int SlavePriceAtStart
    {
        get => slavePriceAtStart;
    }


    [SerializeField] int amountSlaves = 0;
    public int AmountSlaves
    {
        get => amountSlaves;
        set => amountSlaves = value;
    }

    [SerializeField] float farmTime = 5;
    [SerializeField] int amountSnowBySlaves = 1;

    [SerializeField] float currentTime = 0;
    [SerializeField] bool currentTimeWasSet = false;

    [SerializeField] CollectManager collectManager;
    private void Start()
    {
        collectManager = FindObjectOfType<CollectManager>();
        slavePriceAtStart = slavePrice;
    }
    private void Update()
    {
        if (GameManager.Instance.InPause)
        {
            currentTime += Time.deltaTime;
            return;
        }
        if (amountSlaves > 0)
        {
            if (!currentTimeWasSet)
            {
                currentTime = Time.time;
                currentTimeWasSet = true;
            }
            if (Time.time >= currentTime + (farmTime / amountSlaves))
            {
                GameManager.Instance.SnowAmount += amountSnowBySlaves;
                currentTimeWasSet = false;
                if (GameManager.Instance.SnowAmount >= collectManager.MaximumSnowConainer)
                {
                    GameManager.Instance.SnowAmount = collectManager.MaximumSnowConainer;
                }
            }
        }
    }

}
