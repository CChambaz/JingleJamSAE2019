﻿using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //Variables
    [Header("View")]
    [SerializeField] private GameObject View_1;
    [SerializeField] private Canvas View_2;
    [SerializeField] private GameObject View_2_Background;
    [SerializeField] private Canvas View_3;
    [SerializeField] private Canvas View_4;
    [Header("SnowAmount")]
    [SerializeField] private Text TxtSnowAmount;
    [Header("SnowBallAmount")]
    [SerializeField] private Text TxtSnowBallAmountWhite;
    [SerializeField] private Text TxtSnowBallAmountGreen;
    [SerializeField] private Text TxtSnowBallAmountBlue;
    [SerializeField] private Text TxtSnowBallAmountRed;
    [Header("MoneyAmount")]
    [SerializeField] private Text TxtMoneyAmount;

    private bool inPause = false;

    private Canvas menu;

    private Charge charge;
    public Charge Charge
    {
        get => charge;
        set => charge = value;
    }

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
        IN_GAME_4,
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

    private StatsManager statsManagerInstance;
    public StatsManager StatsManagerInstance
    {
        get => statsManagerInstance;
        set => statsManagerInstance = value;
    }

    private CameraScript cameraScript;
    public CameraScript CameraScript
    {
        get => cameraScript;
        set => cameraScript = value;
    }

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
            TxtSnowBallAmountWhite.text = snowballAmount[0].ToString();
            TxtSnowBallAmountGreen.text = snowballAmount[1].ToString();
            TxtSnowBallAmountRed.text = snowballAmount[2].ToString();
            TxtSnowBallAmountBlue.text = snowballAmount[3].ToString();
            return snowballAmount;
        }
        set
        {
            snowballAmount = value;
        }
    }

    private int moneyAmount;
    public int MoneyAmount
    {
        get => moneyAmount;
        set
        {
            charge.AnnualRevenu += value;
            moneyAmount = value;
            TxtMoneyAmount.text = value.ToString();
        }
    }

    void Awake()
    {
        if(Instance != this && Instance != null)
            Destroy(gameObject);
        else
            Instance = this;
        
        statsManagerInstance = GetComponent<StatsManager>();
        cameraScript = FindObjectOfType<CameraScript>();
        charge = FindObjectOfType<Charge>();
    }
    
    public void Start()
    {
        SetTypeGame((int)type);
        menu = GameObject.Find("CanvasMenu").GetComponent<Canvas>();
    }
    
    public void SetTypeGame(int state)
    {
        switch (state)
        {
            case (int)GameState.IN_GAME_1:
                this.type = GameState.IN_GAME_1;
                View_1.gameObject.SetActive(true);
                View_2.GetComponent<CanvasGroup>().interactable = false;
                View_2.GetComponent<CanvasGroup>().alpha = 0;
                View_2.GetComponent<CanvasGroup>().blocksRaycasts = false;
                View_2_Background.SetActive(false);
                View_3.GetComponent<CanvasGroup>().interactable = false;
                View_3.GetComponent<CanvasGroup>().alpha = 0;
                View_3.GetComponent<CanvasGroup>().blocksRaycasts = false;
                View_4.GetComponent<CanvasGroup>().interactable = false;
                View_4.GetComponent<CanvasGroup>().alpha = 0;
                View_4.GetComponent<CanvasGroup>().blocksRaycasts = false;
                break;

            case (int)GameState.IN_GAME_2:
                this.type = GameState.IN_GAME_2;
                View_1.gameObject.SetActive(false);
                View_2.GetComponent<CanvasGroup>().interactable = true;
                View_2.GetComponent<CanvasGroup>().alpha = 1;
                View_2.GetComponent<CanvasGroup>().blocksRaycasts = true;
                View_2_Background.SetActive(true);
                View_3.GetComponent<CanvasGroup>().interactable = false;
                View_3.GetComponent<CanvasGroup>().alpha = 0;
                View_3.GetComponent<CanvasGroup>().blocksRaycasts = false;
                View_4.GetComponent<CanvasGroup>().interactable = false;
                View_4.GetComponent<CanvasGroup>().alpha = 0;
                View_4.GetComponent<CanvasGroup>().blocksRaycasts = false;
                break;

            case (int)GameState.IN_GAME_3:
                this.type = GameState.IN_GAME_3;
                View_1.gameObject.SetActive(false);
                View_2.GetComponent<CanvasGroup>().interactable = false;
                View_2.GetComponent<CanvasGroup>().alpha = 0;
                View_2.GetComponent<CanvasGroup>().blocksRaycasts = false;
                View_2_Background.SetActive(false);
                View_3.GetComponent<CanvasGroup>().interactable = true;
                View_3.GetComponent<CanvasGroup>().alpha = 1;
                View_3.GetComponent<CanvasGroup>().blocksRaycasts = true;
                View_4.GetComponent<CanvasGroup>().interactable = false;
                View_4.GetComponent<CanvasGroup>().alpha = 0;
                View_4.GetComponent<CanvasGroup>().blocksRaycasts = false;
                break;
            case (int)GameState.IN_GAME_4:
                this.type = GameState.IN_GAME_4;
                View_1.gameObject.SetActive(false);
                View_2.GetComponent<CanvasGroup>().interactable = false;
                View_2.GetComponent<CanvasGroup>().alpha = 0;
                View_2.GetComponent<CanvasGroup>().blocksRaycasts = false;
                View_2_Background.SetActive(false);
                View_3.GetComponent<CanvasGroup>().interactable = false;
                View_3.GetComponent<CanvasGroup>().alpha = 0;
                View_3.GetComponent<CanvasGroup>().blocksRaycasts = false;
                View_4.GetComponent<CanvasGroup>().interactable = true;
                View_4.GetComponent<CanvasGroup>().alpha = 1;
                View_4.GetComponent<CanvasGroup>().blocksRaycasts = true;
                break;
        }
        
        MotherFuckingAudioManager.Instance.PlaySound(MotherFuckingAudioManager.SoundList.REACTION_HAPPY);
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            
        }
    }
  
    public bool InPause
    {
        get => inPause;
        set => inPause = value;
    }

    public void BtnPause()
    {
        inPause = !inPause;
        if (inPause)
        {
            menu.GetComponent<CanvasGroup>().interactable = true;
            menu.GetComponent<CanvasGroup>().alpha = 1;
            menu.GetComponent<CanvasGroup>().blocksRaycasts = true;
        }
        else
        {
            menu.GetComponent<CanvasGroup>().interactable = false;
            menu.GetComponent<CanvasGroup>().alpha = 0;
            menu.GetComponent<CanvasGroup>().blocksRaycasts = false;
        }
    }


}
