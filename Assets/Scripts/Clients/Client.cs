using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Client : MonoBehaviour
{
    [SerializeField] private CanvasGroup[] itemsPanel;
    [SerializeField] private Image clientImage;
    [SerializeField] private Image timerImage;
    [SerializeField] private TMP_Text moneyText;
    [SerializeField] private Button validationButton;
    
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

    private float waitingTime;

    private bool satisfied = false;
    
    private ClientManager clientManager;
    
    // Start is called before the first frame update
    void Start()
    {
        clientManager = FindObjectOfType<ClientManager>();
        
        order = new int[GameManager.SNOWBALL_TYPE_COUNT];

        for (int i = 0; i < order.Length; i++)
            order[i] = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isWaiting)
            return;

        waitingTime += Time.deltaTime;

        if (satisfied || waitingTime >= waitingFor)
        {
            clientManager.DespawnClient(index, satisfied);
            satisfied = false;
            isWaiting = false;
            return;
        }
        
        timerImage.fillAmount = waitingTime / waitingFor;

        for (int i = 0; i < order.Length; i++)
        {
            if (order[i] <= 0)
                continue;
            
            /*if(order[i] <= GameManager.Instance.SnowballAmount[i])
                itemsPanel[i].GetComponent<Image>().color = Color.cyan;
            else
                itemsPanel[i].GetComponent<Image>().color = Color.red;*/
        }
    }

    public void StartWaiting()
    {
        isWaiting = true;
        waitingTime = 0.0f;
        SetRandomOrder();
    }
    
    public void SetRandomOrder()
    {
        for (int i = 0; i < order.Length; i++)
        {
            order[i] = Random.Range(0, 10);
        }
        
        for (int i = 0; i < order.Length; i++)
        {
            if (order[i] > 0)
            {
                itemsPanel[i].alpha = 1.0f;
                
                // TODO: Add stock values instead of the 0
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
        for (int i = 0; i < order.Length; i++)
        {
            // Check if the stocks have enough to complete the order
        }

        GameManager.Instance.MoneyAmount += moneyGiven;
        moneyGiven = 0;
        satisfied = true;
    }
}
