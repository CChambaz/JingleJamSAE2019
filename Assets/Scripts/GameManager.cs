﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject View_1;
    [SerializeField] private Canvas View_2;
    [SerializeField] private Canvas View_3;
    
    [SerializeField] private Text TxtSnowAmount;
    [SerializeField] private Text TxtSnowBallAmount;
    [SerializeField] private Text TxtMoneyAmount;

    private Canvas canvasSelect;
    private ClientManager clientManager;
    public ClientManager ClientManager
    {
        get => clientManager;
        set => clientManager = value;
    }
    
    public enum GameState
    {
        MAIN_MENU,
        IN_GAME_1,
        IN_GAME_2,
        IN_GAME_3,
        IN_PAUSE
    }

    private GameState type = GameState.IN_GAME_1;
    public GameState Type => type;

    public const int SNOWBALL_TYPE_COUNT = 4;

    enum SnowballType
    {
        ROUND,
        STAR,
        SQUARE,
        FLAKE
    }

    public static GameManager Instance;

    private float snowAmount;
    public float SnowAmount
    {
        get => snowAmount;
        set
        {
            snowAmount = value;
            TxtSnowAmount.text = value.ToString();
        }
    }

    private int[] snowballAmount = new int[SNOWBALL_TYPE_COUNT];

    public int[] SnowballAmount
    {
        get
        {
            TxtSnowBallAmount.text = snowballAmount[0].ToString();
            return snowballAmount;
        }
        set
        {
            snowballAmount = value;
            clientManager.CheckStorage();
        }
    }

    private int moneyAmount;
    public int MoneyAmount
    {
        get => moneyAmount;
        set
        {
            moneyAmount = value;
            TxtMoneyAmount.text = value.ToString();
        }
    }

    void Awake()
    {
        Instance = this;
    }
    
    public void Start()
    {
        SetTypeGame((int)type);
    }
    
    public void SetTypeGame(int type)
    {
        switch (type)
        {
            case (int)GameState.IN_GAME_1:
                this.type = GameState.IN_GAME_1;
                View_1.gameObject.SetActive(true);
                View_2.GetComponent<CanvasGroup>().interactable = false;
                View_2.GetComponent<CanvasGroup>().alpha = 0;
                View_3.GetComponent<CanvasGroup>().interactable = false;
                View_3.GetComponent<CanvasGroup>().alpha = 0;
                break;
            case (int)GameState.IN_GAME_2:
                this.type = GameState.IN_GAME_2;
                View_1.gameObject.SetActive(false);
                View_2.GetComponent<CanvasGroup>().interactable = true;
                View_2.GetComponent<CanvasGroup>().alpha = 1;
                View_3.GetComponent<CanvasGroup>().interactable = false;
                View_3.GetComponent<CanvasGroup>().alpha = 0;
                break;
            case (int)GameState.IN_GAME_3:
                this.type = GameState.IN_GAME_3;
                View_1.gameObject.SetActive(false);
                View_2.GetComponent<CanvasGroup>().interactable = false;
                View_2.GetComponent<CanvasGroup>().alpha = 0;
                View_3.GetComponent<CanvasGroup>().interactable = true;
                View_3.GetComponent<CanvasGroup>().alpha = 1;
                break;
        }
    }

    [SerializeField] private Canvas menu;
    private bool inPause = false;
    public bool InPause
    {
        get => inPause;
        set => inPause = value;
    }

    public void BtnPause()
    {
        inPause = !inPause;
        Debug.Log(inPause);
        if (inPause)
        {
            menu.gameObject.SetActive(true);
            menu.GetComponent<RectTransform>().position = Vector3.zero;
        }
        else
        {
            menu.GetComponent<RectTransform>().position = Vector3.up * 10;
            menu.gameObject.SetActive(false);
        }
    }
}
