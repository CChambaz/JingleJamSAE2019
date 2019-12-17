using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    [SerializeField] float timeForHours = 5;
    float currentTime = 0;
    bool currentTimeWasSet = false;

    [SerializeField] int dayHours = 0;
    [SerializeField] int weekDay = 0;

    enum Season
    {
        WINTER,
        SPRING,
        SUMMER,
        FALL
    }
    [SerializeField] Season season = Season.WINTER;

    private void Update()
    {
        if (!currentTimeWasSet)
        {
            currentTime = Time.time;
            currentTimeWasSet = true;
        }

        if (Time.time > currentTime + timeForHours)
        {
            dayHours++;
            if (dayHours >= 24)
            {
                weekDay++;
                dayHours = 0;
            }
            if (weekDay >= 7)
            {
                weekDay = 0;
                switch (season)
                {
                    case Season.WINTER:
                        season = Season.SPRING;
                        break;
                    case Season.SPRING:
                        season = Season.SUMMER;
                        break;
                    case Season.SUMMER:
                        season = Season.FALL;
                        break;
                    case Season.FALL:
                        season = Season.WINTER;
                        break;
                }
            }
            currentTimeWasSet = false;
        }
    }
}
