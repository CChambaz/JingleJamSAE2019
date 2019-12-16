using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class MoldProduction : MonoBehaviour
{
    private float currentTimer = 0;
    private float currentAutomationTimer = 0;
    [SerializeField] private Slider timerSlider;
    [SerializeField] private Button buttonProduction;
    [SerializeField] private TextMeshProUGUI buttonText;
    private MoldManager moldManager;

    private bool ready = false;
    // Start is called before the first frame update
    void Start()
    {
        moldManager = FindObjectOfType<MoldManager>();
        timerSlider.maxValue = moldManager.CurrentMoldSO.MaxTimer;
        timerSlider.minValue = 0;
        if (GameManager.Instance.SnowAmount >= moldManager.CurrentMoldSO.SnowCost && moldManager.CurrentMoldClass.Unlocked && GameManager.Instance.SnowballAmount[moldManager.SelectedMold] <= moldManager.CurrentMoldSO.MaxStock * moldManager.CurrentMoldClass.StockLevel)
        {
            ready = false;
            buttonProduction.interactable = false;
            currentTimer = 0;
            GameManager.Instance.SnowballAmount[moldManager.SelectedMold] += moldManager.CurrentMoldSO.BallRate;
            GameManager.Instance.SnowAmount -= moldManager.CurrentMoldSO.SnowCost;
        }
        else 
        {
            GameManager.Instance.MoneyAmount -= moldManager.CurrentMoldSO.UnlockedCost;
            moldManager.CurrentMoldClass.unlocked = true;
            ready = false;
            buttonProduction.interactable = false;
            currentTimer = 0;
            GameManager.Instance.SnowballAmount[moldManager.SelectedMold] += moldManager.CurrentMoldSO.BallRate;
            GameManager.Instance.SnowAmount -= moldManager.CurrentMoldSO.SnowCost;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.SnowAmount >= moldManager.CurrentMoldSO.SnowCost && moldManager.CurrentMoldClass.Unlocked && GameManager.Instance.SnowballAmount[moldManager.SelectedMold] <= moldManager.CurrentMoldSO.MaxStock * moldManager.CurrentMoldClass.StockLevel)
        {
            buttonText.text = "Create";
            if (!ready)
            {
                timerSlider.value = currentTimer;
                if (currentTimer > moldManager.CurrentMoldSO.MaxTimer)
                {
                    ready = true;
                    buttonProduction.interactable = true;
                }
                else
                {
                    currentTimer += moldManager.CurrentMoldSO.TimerSpeed * Time.deltaTime * moldManager.CurrentMoldClass.SpeedLevel;
                }
            }
        } else
        {
            if (!moldManager.CurrentMoldClass.Unlocked)
            {
                buttonText.text = "Unlock for " + moldManager.CurrentMoldSO.UnlockedCost;
                buttonProduction.interactable = GameManager.Instance.MoneyAmount >= moldManager.CurrentMoldSO.UnlockedCost;
            } else if (GameManager.Instance.SnowAmount < moldManager.CurrentMoldSO.SnowCost)
            {
                buttonText.text = "Not Enough Snow";
            } else if (GameManager.Instance.SnowballAmount[moldManager.SelectedMold] > moldManager.CurrentMoldSO.MaxStock * moldManager.CurrentMoldClass.StockLevel)
            {
                buttonText.text = "Out of stack";
            }
        }
        if (moldManager.CurrentMoldClass.AutomationLevel > 0)
        {
            currentAutomationTimer += moldManager.CurrentMoldSO.AutomationSpeed * moldManager.CurrentMoldClass.AutomationLevel * Time.deltaTime;
            if (currentAutomationTimer > 1)
            {
                currentAutomationTimer = 0;
                GameManager.Instance.SnowballAmount[moldManager.SelectedMold] += moldManager.CurrentMoldSO.BallRate;
                GameManager.Instance.SnowAmount -= moldManager.CurrentMoldSO.SnowCost / 2;
            }
        }
    }

    public void OnButtonClick()
    {
        if (ready)
        {
            ready = false;
            buttonProduction.interactable = false;
            currentTimer = 0;
            GameManager.Instance.SnowballAmount[moldManager.SelectedMold] += moldManager.CurrentMoldSO.BallRate;
            GameManager.Instance.SnowAmount -= moldManager.CurrentMoldSO.SnowCost;
        } else if (!moldManager.CurrentMoldClass.Unlocked && GameManager.Instance.MoneyAmount >= moldManager.CurrentMoldSO.UnlockedCost)
        {
            GameManager.Instance.MoneyAmount -= moldManager.CurrentMoldSO.UnlockedCost;
            moldManager.CurrentMoldClass.unlocked = true;
            ready = false;
            buttonProduction.interactable = false;
            currentTimer = 0;
        }
    }

    public void OnMoldChange()
    {
        timerSlider.maxValue = moldManager.CurrentMoldSO.MaxTimer;
        timerSlider.minValue = 0;
        currentTimer = 0;
        ready = false;
        buttonProduction.interactable = false;
    }
    
}
