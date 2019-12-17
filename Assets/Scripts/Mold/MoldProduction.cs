using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class MoldProduction : MonoBehaviour
{
    private float currentTimer = 0;
    private float[] currentAutomationTimer = new float[4];
    [SerializeField] private Slider timerSlider;
    [SerializeField] private Image[] automationTimer;
    [SerializeField] private Button buttonProduction;
    [SerializeField] private TextMeshProUGUI buttonText;
    [SerializeField] private GameObject[] snowBallPrefab;
    private MoldManager moldManager;

    private bool ready = false;

    // Start is called before the first frame update
    void Start()
    {
        moldManager = FindObjectOfType<MoldManager>();
        timerSlider.maxValue = moldManager.CurrentMoldSO.MaxTimer;
        timerSlider.minValue = 0;
        foreach (Image image in automationTimer)
        {
            image.fillAmount = 0;
        }
        if (GameManager.Instance.SnowAmount >= moldManager.CurrentMoldSO.SnowCost && moldManager.CurrentMoldClass.Unlocked && GameManager.Instance.SnowballAmount[moldManager.SelectedMold] <= moldManager.CurrentMoldSO.MaxStock * moldManager.CurrentMoldClass.StockLevel)
        {
            ready = false;
            buttonProduction.interactable = false;
            currentTimer = 0;
        }
        else 
        {
            GameManager.Instance.MoneyAmount -= moldManager.CurrentMoldSO.UnlockedCost;
            moldManager.CurrentMoldClass.unlocked = true;
            ready = false;
            buttonProduction.interactable = false;
            currentTimer = 0;
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
        }
        else
        {
            if (!moldManager.CurrentMoldClass.Unlocked)
            {
                buttonText.text = "Unlock for " + moldManager.CurrentMoldSO.UnlockedCost;
                buttonProduction.interactable = GameManager.Instance.MoneyAmount >= moldManager.CurrentMoldSO.UnlockedCost;
                currentTimer = 0;
            }
            else if (GameManager.Instance.SnowAmount < moldManager.CurrentMoldSO.SnowCost)
            {
                buttonText.text = "Not Enough Snow";
                buttonProduction.interactable = false;
                currentTimer = 0;
            }
            else if (GameManager.Instance.SnowballAmount[moldManager.SelectedMold] > moldManager.CurrentMoldSO.MaxStock * moldManager.CurrentMoldClass.StockLevel)
            {
                buttonText.text = "Out of stack";
                buttonProduction.interactable = false;
                currentTimer = 0;
            }
        }
        for (int i = 0; i < 4; i++)
        {
            if (moldManager.MoldClasses[i].AutomationLevel > 0 && GameManager.Instance.SnowAmount >= moldManager.MoldsSO[i].SnowCost && moldManager.MoldClasses[i].Unlocked && GameManager.Instance.SnowballAmount[moldManager.SelectedMold] <= moldManager.MoldsSO[i].MaxStock * moldManager.MoldClasses[i].StockLevel)
            {
                currentAutomationTimer[i] += moldManager.MoldsSO[i].AutomationSpeed * moldManager.MoldClasses[i].AutomationLevel * Time.deltaTime;
                automationTimer[i].fillAmount = currentAutomationTimer[i];
                if (currentAutomationTimer[i] > 1)
                {
                    currentAutomationTimer[i] = 0;
                    GameManager.Instance.SnowballAmount[i] += moldManager.MoldsSO[i].BallRate;
                    GameManager.Instance.SnowAmount -= moldManager.MoldsSO[i].SnowCost;
                    GameObject.Instantiate(snowBallPrefab[i], Camera.main.ScreenToWorldPoint(automationTimer[i].transform.position) + Vector3.forward * 10, Quaternion.identity);
                }
            } else
            {
                automationTimer[i].fillAmount = 0;
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
            GameObject.Instantiate(snowBallPrefab[moldManager.SelectedMold], Vector3.zero, Quaternion.identity);
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
