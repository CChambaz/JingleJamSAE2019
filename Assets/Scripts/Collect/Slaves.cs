using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slaves : MonoBehaviour
{
    [SerializeField] int slavePrice = 500;
    [SerializeField] int amountSlaves = 0;
    [SerializeField] float farmTime = 5;
    [SerializeField] int amountSnowBySlaves = 1;

    [SerializeField] float currentTime = 0;
    [SerializeField] bool currentTimeWasSet = false;

    private void Update()
    {
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
            }
        }
    }



}
