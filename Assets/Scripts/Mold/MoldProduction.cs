using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class MoldProduction : MonoBehaviour
{
    private float currentTimer = 0;
    [SerializeField] private Slider timerSlider;
    [SerializeField] private Button buttonProduction;
    [SerializeField] private Text buttonText;
    private MoldManager moldManager;

    private bool ready = false;
    // Start is called before the first frame update
    void Start()
    {
        moldManager = FindObjectOfType<MoldManager>();
        timerSlider.maxValue = moldManager.CurrentMold.MaxTimer;
        timerSlider.minValue = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.SnowAmount >= moldManager.CurrentMold.SnowCost)
        {
            buttonText.text = "Create";
            if (!ready)
            {
                timerSlider.value = currentTimer;
                if (currentTimer > moldManager.CurrentMold.MaxTimer)
                {
                    ready = true;
                    buttonProduction.interactable = true;
                }
                else
                {
                    currentTimer += moldManager.CurrentMold.TimerSpeed * Time.deltaTime;
                }
            }
        } else
        {
            buttonText.text = "Not Enough Snow";
        }
    }

    public void OnButtonClick()
    {
        ready = false;
        buttonProduction.interactable = false;
        currentTimer = 0;
        GameManager.Instance.SnowballAmount[moldManager.SelectedMold] += moldManager.CurrentMold.BallRate;
        GameManager.Instance.SnowAmount -= moldManager.CurrentMold.SnowCost;
    }

    public void OnMoldChange()
    {
        timerSlider.maxValue = moldManager.CurrentMold.MaxTimer;
        timerSlider.minValue = 0;
        currentTimer = 0;
        ready = false;
        buttonProduction.interactable = false;
    }
    
}
