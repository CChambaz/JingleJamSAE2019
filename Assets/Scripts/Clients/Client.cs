﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Client : MonoBehaviour
{
    [SerializeField] private CanvasGroup[] itemsPanel;
    [SerializeField] private Image[] itemsImage;
    [SerializeField] private int[] itemsUnlockAtClient;
    [SerializeField] private Image clientImage;
    [SerializeField] private Image timerImage;
    [SerializeField] private TMP_Text moneyText;

    private bool isWaiting;
    public bool IsWaiting
    {
        get => isWaiting;
        set => isWaiting = value;
    }

    private float waitingFor;
    public float WaitingFor
    {
        get => waitingFor;
        set => waitingFor = value;
    }

    private int[] order;
    public int[] Order
    {
        get => order;
        set => order = value;
    }

    private int index;
    public int Index
    {
        get => index;
        set => index = value;
    }

    private int moneyGiven;
    public int MoneyGiven
    {
        get => moneyGiven;
        set => moneyGiven = value;
    }

    private bool orderCanBeAchieved = false;
    public bool OrderCanBeAchieved
    {
        get => orderCanBeAchieved;
        set => orderCanBeAchieved = value;
    }
    
    private float waitingTime;

    private bool satisfied = false;
    
    private ClientManager clientManager;

    private int minItemIndex = 0;
    private int maxItemIndex = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        clientManager = FindObjectOfType<ClientManager>();
        
        order = new int[GameManager.SNOWBALL_TYPE_COUNT];
        itemsImage = new Image[GameManager.SNOWBALL_TYPE_COUNT];
        
        for (int i = 0; i < order.Length; i++)
            order[i] = 0;

        for (int i = 0; i < order.Length; i++)
            itemsImage[i] = itemsPanel[i].GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isWaiting || GameManager.Instance.InPause)
            return;

        waitingTime += Time.deltaTime;

        if (satisfied || waitingTime >= waitingFor)
        {
            clientManager.DespawnClient(index, satisfied);
            moneyGiven = 0;
            satisfied = false;
            isWaiting = false;
            return;
        }
        
        timerImage.fillAmount = waitingTime / waitingFor;
    }

    public void StartWaiting()
    {
        isWaiting = true;
        waitingTime = 0.0f;
        SetRandomOrder();
    }
    
    public void SetRandomOrder()
    {
        int forceItemID = Random.Range(minItemIndex, maxItemIndex + 1);
        
        order[forceItemID] = Random.Range(1, 10);
        
        for (int i = 0; i < maxItemIndex; i++)
        {
            if (i == forceItemID)
                continue;
            
            if (Random.Range(0, 100) > 50)
                order[i] = Random.Range(1, 10);
        }
        
        for (int i = 0; i < order.Length; i++)
        {
            if (order[i] > 0)
            {
                itemsPanel[i].alpha = 1.0f;
                itemsPanel[i].GetComponentInChildren<TMP_Text>().text = order[i].ToString();
                moneyGiven += clientManager.snowballValues[i] * order[i];
            }
            else
                itemsPanel[i].alpha = 0.0f;
        }

        moneyText.text = moneyGiven.ToString();
    }

    public void AchieveOrder()
    {
        if (!orderCanBeAchieved)
            return;

        GameManager.Instance.MoneyAmount += moneyGiven;

        for (int i = 0; i < order.Length; i++)
            GameManager.Instance.SnowballAmount[i] -= order[i];
        
        clientManager.CheckStorage();
        satisfied = true;
    }

    public void UpdateMaxItemIndex()
    {
        if (maxItemIndex + 1 >= GameManager.SNOWBALL_TYPE_COUNT)
            maxItemIndex = GameManager.SNOWBALL_TYPE_COUNT - 1;
        
        if (itemsUnlockAtClient[maxItemIndex + 1] < clientManager.ClientSatisfiedCount)
            maxItemIndex++;
    }
    
    public void UpdateItemImage(int index, bool canBeSell = true)
    {
        if(canBeSell)
            itemsImage[index].color = Color.cyan;
        else
            itemsImage[index].color = Color.red;
    }
}
