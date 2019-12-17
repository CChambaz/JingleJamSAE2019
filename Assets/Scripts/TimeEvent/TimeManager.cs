using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public struct Weather
{
    public int percent;
    public WeatherTypeSO weatherType;
}

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

    [SerializeField] private GameObject eventPanel;
    [SerializeField] private TextMeshProUGUI eventName;
    [SerializeField] private Image eventImage;
    private TimeEvent[] timeEvents = new TimeEvent[7];


    [SerializeField] private Transform allDayPanel;
    [SerializeField] private GameObject dayPrefab;
    private GameObject[] dayObjects = new GameObject[7];

    private Weather[] weathers = new Weather[7]; 
    [SerializeField] private List<WeatherTypeSO> weatherTypes;

    void Start()
    {
        eventPanel.SetActive(false);
        for (int j = 0; j < 7; j++)
        {
            dayObjects[j] = Instantiate(dayPrefab, allDayPanel);
            {
                Weather weather = new Weather();
                weather.percent = Random.Range(0, 15);
                weather.weatherType = weatherTypes[Random.Range(0, weatherTypes.Count)];
                weathers[j] = weather;
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
                timeEvents[j] = possibleEvents[possibleEventsIndex];
            }
            else
            {
                timeEvents[j] = null;
            }
            string weekDayText = "NoName";
            switch (weekDay)
            {
                case 0:
                {
                    weekDayText = "Mon";
                    break;
                }
                case 1:
                {
                    weekDayText = "Tue";
                    break;
                }
                case 2:
                {
                    weekDayText = "Wed";
                    break;
                }
                case 3:
                {
                    weekDayText = "Thi";
                    break;
                }
                case 4:
                {
                    weekDayText = "Fri";
                    break;
                }
                case 5:
                {
                    weekDayText = "Sat";
                    break;
                }
                case 6:
                {
                    weekDayText = "Sun";
                    break;
                }
            }
            if (timeEvents[j] != null)
            {
                dayObjects[j].GetComponent<DayPanel>().DisplayWeather(weekDayText, weathers[j].weatherType.WeatherImage, weathers[j].weatherType.Name, weathers[j].percent.ToString(), timeEvents[j].Name);
            }
            else
            {
                dayObjects[j].GetComponent<DayPanel>().DisplayWeather(weekDayText, weathers[j].weatherType.WeatherImage, weathers[j].weatherType.Name, weathers[j].percent.ToString(), "");
            }
        }
    }

    private void Update()
    {
        if (GameManager.Instance.InPause)
        {
            return;
        }
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
            //========Event===========
            
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
                currenTimeEvent = timeEvents[0];
                if (currenTimeEvent != null)
                {
                    currenTimeEvent.StartEvent();
                    eventtimer = currenTimeEvent.Duration;
                    eventPanel.SetActive(true);
                    eventImage.sprite = currenTimeEvent.Sprite;
                    eventName.text = currenTimeEvent.Name;
                }
                for (int i = 0; i < timeEvents.Length - 1; i++)
                {
                    timeEvents[i] = timeEvents[i + 1];
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
                    timeEvents[6] = possibleEvents[possibleEventsIndex];
                } else
                {
                    timeEvents[6] = null;
                }
            } else
            {
                eventtimer--;
                eventPanel.SetActive(false);
            }
            //========Weather==========
            if (weathers[0].percent < Random.value*100)
            {
                weathers[0].weatherType = weatherTypes[Random.Range(0, weatherTypes.Count)];
            }
            switch (weathers[0].weatherType.Type)
            {
                case WeatherTypeSO.WeatherType.SUN:
                    GameManager.Instance.StatsManagerInstance.SpawnTimeMultiplier = 2;
                    GameManager.Instance.StatsManagerInstance.FallingSpeed = 1;
                    GameManager.Instance.StatsManagerInstance.SpawningSize = 1;
                    GameManager.Instance.StatsManagerInstance.TempestForce = 0;
                    break;
                case WeatherTypeSO.WeatherType.CLOUDY:
                    GameManager.Instance.StatsManagerInstance.SpawnTimeMultiplier = 1.5f;
                    GameManager.Instance.StatsManagerInstance.FallingSpeed = 1;
                    GameManager.Instance.StatsManagerInstance.SpawningSize = 1;
                    GameManager.Instance.StatsManagerInstance.TempestForce = 0;
                    break;
                case WeatherTypeSO.WeatherType.SNOW:
                    GameManager.Instance.StatsManagerInstance.SpawnTimeMultiplier = 1;
                    GameManager.Instance.StatsManagerInstance.FallingSpeed = 1;
                    GameManager.Instance.StatsManagerInstance.SpawningSize = 1;
                    GameManager.Instance.StatsManagerInstance.TempestForce = 0;
                    break;
                case WeatherTypeSO.WeatherType.MIST:
                    GameManager.Instance.StatsManagerInstance.SpawnTimeMultiplier = 1;
                    GameManager.Instance.StatsManagerInstance.FallingSpeed = 1;
                    GameManager.Instance.StatsManagerInstance.SpawningSize = 1;
                    GameManager.Instance.StatsManagerInstance.TempestForce = 0;
                        break;
                case WeatherTypeSO.WeatherType.STORM:
                    GameManager.Instance.StatsManagerInstance.SpawnTimeMultiplier = 0.5f;
                    GameManager.Instance.StatsManagerInstance.FallingSpeed = 3;
                    GameManager.Instance.StatsManagerInstance.SpawningSize = 8;
                    GameManager.Instance.StatsManagerInstance.TempestForce = 1;
                    GameManager.Instance.StatsManagerInstance.TempestDirection = new Vector2(Random.value * 2 - 1, Random.value * -1).normalized;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            for (int i = 0; i < weathers.Length-1; i++)
            {
                weathers[i] = weathers[i + 1];
                weathers[i].percent += Random.Range(0, 15);
                if (weathers[i].percent > 100)
                {
                    weathers[i].percent = 100;
                } else if (weathers[i].percent <= 50 && Random.value > 0.5f)
                {
                    weathers[i].weatherType = weatherTypes[Random.Range(0, weatherTypes.Count)]; //TODO percent weather
                }
            }
            {
                Weather weather = new Weather();
                weather.percent = Random.Range(0, 15);
                weather.weatherType = weatherTypes[Random.Range(0, weatherTypes.Count)];
                weathers[6] = weather;
            }
            for (int i = 0; i < 7; i++)
            {
                string weekDayText = "NoName";
                switch (weekDay)
                {
                    case 0:
                    {
                        weekDayText = "Mon";
                        break;
                    }
                    case 1:
                    {
                        weekDayText = "Tue";
                        break;
                    }
                    case 2:
                    {
                        weekDayText = "Wed";
                        break;
                    }
                    case 3:
                    {
                        weekDayText = "Thi";
                        break;
                    }
                    case 4:
                    {
                        weekDayText = "Fri";
                        break;
                    }
                    case 5:
                    {
                        weekDayText = "Sat";
                        break;
                    }
                    case 6:
                    {
                        weekDayText = "Sun";
                        break;
                    }
                }
                if (timeEvents[i] !=null)
                {
                    dayObjects[i].GetComponent<DayPanel>().DisplayWeather(weekDayText, weathers[i].weatherType.WeatherImage, weathers[i].weatherType.Name, weathers[i].percent.ToString(), timeEvents[i].Name);
                } else
                {
                    dayObjects[i].GetComponent<DayPanel>().DisplayWeather(weekDayText, weathers[i].weatherType.WeatherImage, weathers[i].weatherType.Name, weathers[i].percent.ToString(), "");
                }
            }
        }
    }
}
