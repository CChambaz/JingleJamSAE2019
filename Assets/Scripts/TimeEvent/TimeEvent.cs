﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TimeEvent : ScriptableObject
{
    [SerializeField] private Sprite sprite;
    public Sprite Sprite => sprite;

    [SerializeField] private float duration;
    public float Duration => duration;

    [SerializeField] private float percentage;
    public float Percentage => percentage;

    [SerializeField] private bool[] seasons = new bool[4];
    public bool[] Seasons => seasons;

    [SerializeField] private bool[] weekDays = new bool[7];
    public bool[] WeekDays => weekDays;

    [SerializeField] private string index;
    public string Index => index;

    public abstract void StartEvent();
    public abstract void EndEvent();
}
