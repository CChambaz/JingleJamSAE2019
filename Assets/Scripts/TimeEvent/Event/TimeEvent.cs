using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeEvent : ScriptableObject
{
    [SerializeField] private Sprite sprite = null;
    public Sprite Sprite => sprite;

    [SerializeField] private float duration = 0;
    public float Duration => duration;

    [SerializeField] private float percentage = 0;
    public float Percentage => percentage;

    [SerializeField] private bool[] seasons = new bool[4];
    public bool[] Seasons => seasons;

    [SerializeField] private bool[] weekDays = new bool[7];
    public bool[] WeekDays => weekDays;

    [SerializeField] protected string index = "";
    public string Index => index;

    public string IndexReset{
        get => index;
        set => index = value;
    }

    [SerializeField] private string name = "Null";
    public string Name => name;

    public virtual void StartEvent(){}
    public virtual void EndEvent(){}
}
