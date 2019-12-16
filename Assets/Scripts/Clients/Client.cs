using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Client : MonoBehaviour
{
    public const int SNOWBALL_TYPE_COUNT_TMP = 4;

    [SerializeField] private CanvasGroup[] itemsPanel;
    [SerializeField] private Image clientImage;
    [SerializeField] private Image timerImage;
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
    
    private float startWaitingAt;

    private float waitingTime;

    private bool satisfied = false;
    
    private ClientManager clientManager;
    
    // Start is called before the first frame update
    void Start()
    {
        clientManager = FindObjectOfType<ClientManager>();
        
        order = new int[SNOWBALL_TYPE_COUNT_TMP];
    }

    // Update is called once per frame
    void Update()
    {
        if (!isWaiting)
            return;

        if (satisfied || waitingTime >= waitingFor)
        {
            isWaiting = false;
            clientManager.DespawnClient(index, satisfied);
        }
        
        waitingTime += Time.deltaTime;
        timerImage.fillAmount = waitingTime / waitingFor;
    }

    public void StartWaiting()
    {
        isWaiting = true;
        startWaitingAt = Time.time;
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
            if(order[i] > 0)
                itemsPanel[i].alpha = 1.0f;
            else
                itemsPanel[i].alpha = 0.0f;
        }
    }

    public void AchieveOrder()
    {
        for (int i = 0; i < order.Length; i++)
        {
            // Check if the stocks have enough to complete the order
        }

        satisfied = true;
    }
}
