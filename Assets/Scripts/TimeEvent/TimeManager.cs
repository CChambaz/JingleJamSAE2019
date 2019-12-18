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
    float lasteventtimer = 0;
    TimeEvent currenTimeEvent = null;

    enum Season
    {
        WINTER,
        SPRING,
        SUMMER,
        FALL
    }

    [SerializeField] Season season = Season.WINTER;

    [SerializeField] GameObject eventPanel;
    [SerializeField] TextMeshProUGUI eventName;
    [SerializeField] Image eventImage;
    TimeEvent[] timeEvents = new TimeEvent[7];


    [SerializeField] Transform allDayPanel;
    [SerializeField] GameObject dayPrefab;
    GameObject[] dayObjects = new GameObject[7];

    Weather[] weathers = new Weather[7];
    [SerializeField] List<WeatherTypeSO> weatherTypes;

    private CollectButtons collectButtons;
    private MoldManager moldManager;
    private BasketController basketController;
    private ClientManager clientManager;

    void Start()
    {
        collectButtons = FindObjectOfType<CollectButtons>();
        moldManager = FindObjectOfType<MoldManager>();
        basketController = FindObjectOfType<BasketController>();
        clientManager = FindObjectOfType<ClientManager>();
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
            List<TimeEvent> possibleEvents = events.FindAll(s => s.Seasons[(int) season] && s.WeekDays[weekDay]);
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
                timeEvents[j] = possibleEvents[possibleEventsIndex];
            else
                timeEvents[j] = null;
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
                dayObjects[j].GetComponent<DayPanel>().DisplayWeather(weekDayText, weathers[j].weatherType.WeatherImage,
                    weathers[j].weatherType.Name, weathers[j].percent.ToString(), timeEvents[j].Name);
            else
                dayObjects[j].GetComponent<DayPanel>().DisplayWeather(weekDayText, weathers[j].weatherType.WeatherImage,
                    weathers[j].weatherType.Name, weathers[j].percent.ToString(), "");
        }
    }

    void Update()
    {
        if (GameManager.Instance.InPause) return;
        if (!currentTimeWasSet)
        {
            currentTime = Time.time;
            currentTimeWasSet = true;
        }

        if (Time.time > currentTime + timeForHours)
        {
            eventPanel.SetActive(false);
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
            
            //========Weather==========
            if (weathers[0].percent < Random.value * 100)
                weathers[0].weatherType = weatherTypes[Random.Range(0, weatherTypes.Count)];
            switch (weathers[0].weatherType.Type)
            {
                case WeatherTypeSO.WeatherType.SUN:
                    GameManager.Instance.StatsManagerInstance.SpawnTimeMultiplier = 2;
                    GameManager.Instance.StatsManagerInstance.Speed = 1;
                    GameManager.Instance.StatsManagerInstance.SpawningSize = 1;
                    GameManager.Instance.StatsManagerInstance.TempestForce = 0;
                    break;
                case WeatherTypeSO.WeatherType.CLOUDY:
                    GameManager.Instance.StatsManagerInstance.SpawnTimeMultiplier = 1.5f;
                    GameManager.Instance.StatsManagerInstance.Speed = 1;
                    GameManager.Instance.StatsManagerInstance.SpawningSize = 1;
                    GameManager.Instance.StatsManagerInstance.TempestForce = 0;
                    break;
                case WeatherTypeSO.WeatherType.SNOW:
                    GameManager.Instance.StatsManagerInstance.SpawnTimeMultiplier = 1;
                    GameManager.Instance.StatsManagerInstance.Speed = 1;
                    GameManager.Instance.StatsManagerInstance.SpawningSize = 1;
                    GameManager.Instance.StatsManagerInstance.TempestForce = 0;
                    break;
                case WeatherTypeSO.WeatherType.MIST:
                    GameManager.Instance.StatsManagerInstance.SpawnTimeMultiplier = 1;
                    GameManager.Instance.StatsManagerInstance.Speed = 1;
                    GameManager.Instance.StatsManagerInstance.SpawningSize = 1;
                    GameManager.Instance.StatsManagerInstance.TempestForce = 0;
                    break;
                case WeatherTypeSO.WeatherType.STORM:
                    GameManager.Instance.StatsManagerInstance.SpawnTimeMultiplier = 0.01f;
                    GameManager.Instance.StatsManagerInstance.Speed = 2;
                    GameManager.Instance.StatsManagerInstance.SpawningSize = 8;
                    GameManager.Instance.StatsManagerInstance.TempestForce = 1;
                    float angle = Mathf.Deg2Rad * Random.Range(200, 340);
                    GameManager.Instance.StatsManagerInstance.TempestDirection =
                        new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)).normalized;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            for (int i = 0; i < weathers.Length - 1; i++)
            {
                weathers[i] = weathers[i + 1];
                weathers[i].percent += Random.Range(0, 15);
                if (weathers[i].percent > 100)
                    weathers[i].percent = 100;
                else if (weathers[i].percent <= 50 && Random.value > 0.5f)
                    weathers[i].weatherType = weatherTypes[Random.Range(0, weatherTypes.Count)]; //TODO percent weather
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
                switch ((weekDay + i)%7)
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

                if (timeEvents[i] != null)
                    dayObjects[i].GetComponent<DayPanel>().DisplayWeather(weekDayText,
                        weathers[i].weatherType.WeatherImage, weathers[i].weatherType.Name,
                        weathers[i].percent.ToString(), timeEvents[i].Name);
                else
                    dayObjects[i].GetComponent<DayPanel>().DisplayWeather(weekDayText,
                        weathers[i].weatherType.WeatherImage, weathers[i].weatherType.Name,
                        weathers[i].percent.ToString(), "");
            }
            //========Event===========
            if (currenTimeEvent != null)
            {
                currenTimeEvent.EndEvent();
                if (currenTimeEvent.Index == "Big") events.Remove(currenTimeEvent);
                currenTimeEvent = null;
            }

            currenTimeEvent = timeEvents[0];
            if (currenTimeEvent != null)
            {
                currenTimeEvent.StartEvent();
                if (currenTimeEvent.Name == "NuclearWar")
                {
                    moldManager.KillAllThisFuckingAutomation(0.9f);
                    GameManager.Instance.SetTypeGame(1);
                }
                if (currenTimeEvent.Name == "Tornado")
                {
                    collectButtons.KillAllThisFuckingSlaves(0.9f);
                    basketController.FuckYouBasket();
                    GameManager.Instance.SetTypeGame(0);
                }
                if (currenTimeEvent.Name == "MassExtinction")
                {
                    clientManager.KillAllThisFuckingClient();
                    GameManager.Instance.SetTypeGame(2);
                }

                eventPanel.SetActive(true);
                eventImage.sprite = currenTimeEvent.Sprite;
                eventName.text = currenTimeEvent.Name;
            }

            for (int i = 0; i < timeEvents.Length - 1; i++) timeEvents[i] = timeEvents[i + 1];
            if (lasteventtimer <= 0)
            {
                if (currenTimeEvent.Name == "NuclearWar")
                {
                }
                if (currenTimeEvent.Name == "Tornado")
                {
                    basketController.WelcomeBackBasket();
                }
                if (currenTimeEvent.Name == "MassExtinction")
                {
                }
                List<TimeEvent> possibleEvents = events.FindAll(s => s.Seasons[(int)season] && s.WeekDays[weekDay]);
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
                    lasteventtimer = timeEvents[6].Duration;
                }
                else
                {
                    timeEvents[6] = null;
                }
            }
            else
            {
                lasteventtimer--;
            }

        }
    }
}