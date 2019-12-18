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
    [SerializeField] private GameObject disabledGameObject;
    [SerializeField] private GameObject[] snowBallPrefab;
    [SerializeField] private GameObject[] particle;
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
            GameManager.Instance.MoneyAmount -= (int)(moldManager.CurrentMoldSO.UnlockedCost * GameManager.Instance.StatsManagerInstance.SalesMultiplier);
            moldManager.CurrentMoldClass.unlocked = true;
            ready = false;
            buttonProduction.interactable = false;
            currentTimer = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.InPause)
        {
            return;
        }
        if (GameManager.Instance.SnowAmount >= moldManager.CurrentMoldSO.SnowCost && moldManager.CurrentMoldClass.Unlocked && GameManager.Instance.SnowballAmount[moldManager.SelectedMold] < moldManager.CurrentMoldSO.MaxStock * moldManager.CurrentMoldClass.StockLevel)
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
                    currentTimer += moldManager.CurrentMoldSO.TimerSpeed * Time.deltaTime * moldManager.CurrentMoldClass.SpeedLevel * GameManager.Instance.StatsManagerInstance.MoldTimeMultiplier;
                }
            }
        }
        else
        {
            if (!moldManager.CurrentMoldClass.Unlocked)
            {
                buttonText.text = "Unlock for " + moldManager.CurrentMoldSO.UnlockedCost * GameManager.Instance.StatsManagerInstance.SalesMultiplier;
                buttonProduction.interactable = GameManager.Instance.MoneyAmount >= moldManager.CurrentMoldSO.UnlockedCost * GameManager.Instance.StatsManagerInstance.SalesMultiplier;
                currentTimer = 0;
            }
            else if (GameManager.Instance.SnowAmount < moldManager.CurrentMoldSO.SnowCost)
            {
                buttonText.text = "Not Enough Snow";
                buttonProduction.interactable = false;
                currentTimer = 0;
            }
            else if (GameManager.Instance.SnowballAmount[moldManager.SelectedMold] >= moldManager.CurrentMoldSO.MaxStock * moldManager.CurrentMoldClass.StockLevel)
            {
                buttonText.text = "Out of stack";
                buttonProduction.interactable = false;
                currentTimer = 0;
            }
        }
        for (int i = 0; i < 4; i++)
        {
            if (moldManager.MoldClasses[i].AutomationLevel > 0 && GameManager.Instance.SnowAmount >= moldManager.MoldsSO[i].SnowCost && moldManager.MoldClasses[i].Unlocked && GameManager.Instance.SnowballAmount[i] < moldManager.MoldsSO[i].MaxStock * moldManager.MoldClasses[i].StockLevel)
            {
                currentAutomationTimer[i] += moldManager.MoldsSO[i].AutomationSpeed * moldManager.MoldClasses[i].AutomationLevel * Time.deltaTime * GameManager.Instance.StatsManagerInstance.MoldTimeMultiplier;
                automationTimer[i].fillAmount = currentAutomationTimer[i];
                if (currentAutomationTimer[i] > 1)
                {
                    currentAutomationTimer[i] = 0;
                    GameManager.Instance.SnowballAmount[i] += moldManager.MoldsSO[i].BallRate;
                    GameManager.Instance.ClientManager.CheckStorage();
                    if (GameManager.Instance.SnowballAmount[i] > moldManager.MoldsSO[i].MaxStock * moldManager.MoldClasses[i].StockLevel)
                    {
                        GameManager.Instance.SnowballAmount[i] = moldManager.MoldsSO[i].MaxStock * moldManager.MoldClasses[i].StockLevel;
                    }
                    GameManager.Instance.SnowAmount -= moldManager.MoldsSO[i].SnowCost;
                    if (GameManager.Instance.Type == GameManager.GameState.IN_GAME_2)
                    {
                        GameObject.Instantiate(snowBallPrefab[i], Camera.main.ScreenToWorldPoint(automationTimer[i].transform.position) + Vector3.forward * 10, Quaternion.identity, disabledGameObject.transform);
                    }
                }
            }
            else
            {
                automationTimer[i].fillAmount = 0;
            }
        }
    }

    public void OnButtonClick()
    {
        if (GameManager.Instance.InPause)
        {
            return;
        }
        if (ready)
        {
            ready = false;
            buttonProduction.interactable = false;
            currentTimer = 0;
            GameManager.Instance.SnowballAmount[moldManager.SelectedMold] += moldManager.CurrentMoldSO.BallRate;
            GameManager.Instance.ClientManager.CheckStorage();
            if (GameManager.Instance.SnowballAmount[moldManager.SelectedMold] > moldManager.MoldsSO[moldManager.SelectedMold].MaxStock * moldManager.MoldClasses[moldManager.SelectedMold].StockLevel)
            {
                GameManager.Instance.SnowballAmount[moldManager.SelectedMold] = moldManager.MoldsSO[moldManager.SelectedMold].MaxStock * moldManager.MoldClasses[moldManager.SelectedMold].StockLevel;
            }
            GameManager.Instance.SnowAmount -= moldManager.CurrentMoldSO.SnowCost;
            if (GameManager.Instance.Type == GameManager.GameState.IN_GAME_2)
            {
                GameObject newObject = Instantiate(snowBallPrefab[moldManager.SelectedMold], Vector3.zero, Quaternion.identity, disabledGameObject.transform);
                newObject.GetComponent<SnowProduction>().TargetPosition = new Vector2(3, -1.5f);
                GameObject newParticle = Instantiate(particle[moldManager.SelectedMold], Vector3.zero, Quaternion.identity);
                Destroy(newParticle, 2);
            }
        } else if (!moldManager.CurrentMoldClass.Unlocked && GameManager.Instance.MoneyAmount >= moldManager.CurrentMoldSO.UnlockedCost * GameManager.Instance.StatsManagerInstance.SalesMultiplier)

        {
            GameManager.Instance.MoneyAmount -= (int)(moldManager.CurrentMoldSO.UnlockedCost * GameManager.Instance.StatsManagerInstance.SalesMultiplier);
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
