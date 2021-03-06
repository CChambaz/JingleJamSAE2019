﻿using System;
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
    private int currentHours;
    [SerializeField] int weekDay = 0;
    private int currentDay;
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

    private Weather[] weathers = new Weather[7];
    [SerializeField] private List<WeatherTypeSO> weatherTypes;
    private int weekCount = 0;

    private CollectButtons collectButtons;
    private MoldManager moldManager;
    private BasketController basketController;
    private ClientManager clientManager;

    private bool tornado;


    void Start()
    {
        collectButtons = FindObjectOfType<CollectButtons>();
        moldManager = FindObjectOfType<MoldManager>();
        basketController = FindObjectOfType<BasketController>();
        clientManager = FindObjectOfType<ClientManager>();
        eventPanel.SetActive(false);
        {
            dayObjects[0] = Instantiate(dayPrefab, allDayPanel);
            {
                Weather weather = new Weather();
                weather.percent = 100;
                weather.weatherType = weatherTypes[2];
                weathers[0] = weather;
            }
            timeEvents[0] = null;
            string weekDayText = "Today";
            dayObjects[0].GetComponent<DayPanel>().DisplayWeather(weekDayText, weathers[0].weatherType.WeatherImage,
                    weathers[0].weatherType.Name, weathers[0].percent.ToString(), "");
        }

        for (int j = 1; j < 7; j++)
        {
            dayObjects[j] = Instantiate(dayPrefab, allDayPanel);
            {
                Weather weather = new Weather();
                weather.percent = Random.Range(0, 15);
                weather.weatherType = weatherTypes[Random.Range(0, weatherTypes.Count)];
                weathers[j] = weather;
            }

            if (lasteventtimer <= 0)
            {

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
                    timeEvents[j] = possibleEvents[possibleEventsIndex];
                    lasteventtimer = timeEvents[j].Duration -1;
                }
                else
                    timeEvents[j] = null;
            }
            else
            {
                timeEvents[j] = timeEvents[j - 1];
                lasteventtimer--;
            }

            string weekDayText = "NoName";
            switch ((weekDay + j) % 7)
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
        lasteventtimer = 0;
    }

    void Update()
    {
        if (GameManager.Instance.InPause)
            return;

        if (!currentTimeWasSet)
        {
            currentTime = Time.time;
            currentTimeWasSet = true;
        }

        if (Time.time > currentTime + timeForHours)
        {
            currentHours++;
            eventPanel.SetActive(false);
            if (currentHours >= dayHours)
            {
                currentDay++;
                currentHours = 0;
                if (currentDay >= weekCount)
                {
                    GameManager.Instance.Charge.ApplyMaintenanceCost();
                    weekCount += 1;
                    currentDay = 0;

                    if (weekCount >= 4)
                    {
                        GameManager.Instance.Charge.ApplyTaxes();
                        weekCount = 0;
                    }
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

                if (currenTimeEvent != null && currenTimeEvent != timeEvents[1])
                {
                    currenTimeEvent.EndEvent();
                    if (currenTimeEvent.Name == "NuclearWar")
                    {
                        MotherFuckingAudioManager.Instance.PlayMusic(MotherFuckingAudioManager.MusicList.MAIN, true);
                    }
                    if (currenTimeEvent.Name == "Tornado")
                    {
                        basketController.WelcomeBackBasket();
                        MotherFuckingAudioManager.Instance.PlayMusic(MotherFuckingAudioManager.MusicList.MAIN, true);
                        tornado = false;
                    }
                    if (currenTimeEvent.Name == "MassExtinction")
                    {
                        MotherFuckingAudioManager.Instance.PlayMusic(MotherFuckingAudioManager.MusicList.MAIN, true);
                    }
                }


                //========Weather==========
                if (!tornado)
                {
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
                    if (weathers[0].percent < Random.value * 100)
                        weathers[0].weatherType = weatherTypes[Random.Range(0, weatherTypes.Count)];
                    switch (weathers[0].weatherType.Type)
                    {
                        case WeatherTypeSO.WeatherType.SUN:
                            GameManager.Instance.StatsManagerInstance.FallingSpeed = 1;
                            GameManager.Instance.StatsManagerInstance.SpawnTimeMultiplier = 2;
                            GameManager.Instance.StatsManagerInstance.RandomSpawnFloatTimeMultiplier = 1;
                            GameManager.Instance.StatsManagerInstance.Speed = 1;
                            GameManager.Instance.StatsManagerInstance.SpawningSize = 1;
                            GameManager.Instance.StatsManagerInstance.TempestForce = 0;
                            break;
                        case WeatherTypeSO.WeatherType.CLOUDY:
                            GameManager.Instance.StatsManagerInstance.FallingSpeed = 1;
                            GameManager.Instance.StatsManagerInstance.SpawnTimeMultiplier = 1.5f;
                            GameManager.Instance.StatsManagerInstance.RandomSpawnFloatTimeMultiplier = 1;
                            GameManager.Instance.StatsManagerInstance.Speed = 1;
                            GameManager.Instance.StatsManagerInstance.SpawningSize = 1;
                            GameManager.Instance.StatsManagerInstance.TempestForce = 0;
                            break;
                        case WeatherTypeSO.WeatherType.SNOW:
                            GameManager.Instance.StatsManagerInstance.FallingSpeed = 1;
                            GameManager.Instance.StatsManagerInstance.SpawnTimeMultiplier = 1;
                            GameManager.Instance.StatsManagerInstance.RandomSpawnFloatTimeMultiplier = 1;
                            GameManager.Instance.StatsManagerInstance.Speed = 1;
                            GameManager.Instance.StatsManagerInstance.SpawningSize = 1;
                            GameManager.Instance.StatsManagerInstance.TempestForce = 0;
                            break;
                        case WeatherTypeSO.WeatherType.MIST:
                            GameManager.Instance.StatsManagerInstance.FallingSpeed = 1;
                            GameManager.Instance.StatsManagerInstance.SpawnTimeMultiplier = 1;
                            GameManager.Instance.StatsManagerInstance.RandomSpawnFloatTimeMultiplier = 1;
                            GameManager.Instance.StatsManagerInstance.Speed = 1;
                            GameManager.Instance.StatsManagerInstance.SpawningSize = 1;
                            GameManager.Instance.StatsManagerInstance.TempestForce = 0;
                            break;
                        case WeatherTypeSO.WeatherType.STORM:
                            GameManager.Instance.StatsManagerInstance.FallingSpeed = 1;
                            GameManager.Instance.StatsManagerInstance.SpawnTimeMultiplier = 0;
                            GameManager.Instance.StatsManagerInstance.RandomSpawnFloatTimeMultiplier = 0;
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
                }

                //========Event===========
               
                for (int i = 0; i < timeEvents.Length - 1; i++)
                    timeEvents[i] = timeEvents[i + 1];

                if (lasteventtimer <= 0)
                {
                    List<TimeEvent> possibleEvents = events.FindAll(s => s.Seasons[(int)season] && s.WeekDays[currentDay]);
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
                        lasteventtimer = timeEvents[6].Duration-1;
                    }
                    else
                    {
                        timeEvents[6] = null;
                    }
                }
                else
                {
                    timeEvents[6] = timeEvents[5];
                    lasteventtimer--;
                }

                if (timeEvents[0] != null && currenTimeEvent != timeEvents[0])
                {
                    currenTimeEvent = timeEvents[0];
                    currenTimeEvent.StartEvent();
                    if (currenTimeEvent.Name == "NuclearWar")
                    {
                        moldManager.KillAllThisFuckingAutomation(0.9f);
                        GameManager.Instance.SetTypeGame(2);
                    }
                    if (currenTimeEvent.Name == "Tornado")
                    {
                        collectButtons.KillAllThisFuckingSlaves(0.9f);
                        GameManager.Instance.StatsManagerInstance.SpawnTimeMultiplier = 0.1f;
                        GameManager.Instance.StatsManagerInstance.RandomSpawnFloatTimeMultiplier = 0;
                        GameManager.Instance.StatsManagerInstance.Speed = 2;
                        GameManager.Instance.StatsManagerInstance.FallingSpeed = 0.25f;
                        GameManager.Instance.StatsManagerInstance.TempestForce = 2;
                        basketController.FuckYouBasket();
                        GameManager.Instance.SetTypeGame(1);
                        tornado = true;
                    }
                    if (currenTimeEvent.Name == "MassExtinction")
                    {
                        clientManager.KillAllThisFuckingClient();
                        GameManager.Instance.SetTypeGame(3);
                    }

                    eventPanel.SetActive(true);
                    //eventImage.sprite = currenTimeEvent.Sprite;
                    eventName.text = currenTimeEvent.Name;
                }
                else
                {
                    currenTimeEvent = timeEvents[0];
                }
                for (int i = 0; i < 7; i++)
                {
                    string weekDayText = "NoName";
                    switch ((currentDay + i) % 7)
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
                    if (i == 0)
                    {
                        weekDayText = "Today";
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
            }
            currentTimeWasSet = false;
        }

    }

}