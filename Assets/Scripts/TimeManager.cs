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
    [SerializeField] List<TimeEvent> events;
    private float eventtimer = 0;
    private TimeEvent currenTimeEvent = null;

    enum Season
    {
        WINTER,
        SPRING,
        SUMMER,
        FALL
    }
    [SerializeField] Season season = Season.WINTER;

    [SerializeField] private GameObject prefabDay;

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
            if (eventtimer <= 0)
            {
                if (currenTimeEvent != null)
                {
                    currenTimeEvent.EndEvent();
                    if (currenTimeEvent.Index == "Big")
                    {
                        events.Remove(currenTimeEvent);
                    }
                    currenTimeEvent = null;
                }
                List<TimeEvent> possibleEvents = events.FindAll(s => (s.Seasons[(int)season] && s.WeekDays[weekDay]));
                float random = Random.value * possibleEvents.Count;
                float currentCount = 0;
                int possibleEventsIndex = -1;
                for (int i = 0; i < possibleEvents.Count; i++)
                {
                    currentCount += possibleEvents[i].Percentage;
                    if (random < currentCount)
                    {
                        possibleEventsIndex = i;
                        break;
                    }
                }
                if (possibleEventsIndex != -1)
                {
                    possibleEvents[possibleEventsIndex].StartEvent();
                    currenTimeEvent = possibleEvents[possibleEventsIndex];
                    eventtimer = possibleEvents[possibleEventsIndex].Duration;
                }
            } else
            {
                eventtimer--;
            }
        }
    }
}
