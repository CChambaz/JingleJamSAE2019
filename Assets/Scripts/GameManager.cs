using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    enum SnowballType
    {
        ROUND,
        STAR,
        SQUARE,
        FLAKE
    }
    
    public static GameManager Instance;
    void Awake()
    {
        Instance = this;
    }

    private int snowAmount;
    public int SnowAmount
    {
        get => snowAmount;
        set => snowAmount = value;
    }

    private int[] snowballAmount;
    public int[] SnowballAmount
    {
        get => snowballAmount;
        set => snowballAmount = value;
    }

    private int moneyAmount;
    public int MoneyAmount
    {
        get => moneyAmount;
        set => moneyAmount = value;
    }
}
