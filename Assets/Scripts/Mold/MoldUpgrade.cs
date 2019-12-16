using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MoldUpgrade : MonoBehaviour
{
    private MoldManager moldManager;
    [SerializeField] private Button automationUpgradeButton;
    [SerializeField] private TextMeshProUGUI automationUpgradeText;
    [SerializeField] private Button speedUpgradeButton;
    [SerializeField] private TextMeshProUGUI speedUpgradeText;
    [SerializeField] private Button stockUpgradeButton;
    [SerializeField] private TextMeshProUGUI stockUpgradeText;

    void Start()
    {
        moldManager = FindObjectOfType<MoldManager>();
    }

    void Update()
    {
        if (moldManager.CurrentMoldClass.Unlocked)
        {
            automationUpgradeButton.interactable = moldManager.CurrentMoldSO.AutomationCost * moldManager.CurrentMoldClass.AutomationLevel <= GameManager.Instance.MoneyAmount;
            automationUpgradeText.text = (moldManager.CurrentMoldSO.AutomationCost * moldManager.CurrentMoldClass.AutomationLevel).ToString();
            speedUpgradeButton.interactable = moldManager.CurrentMoldSO.SpeedUpgradeCost * moldManager.CurrentMoldClass.SpeedLevel <= GameManager.Instance.MoneyAmount;
            speedUpgradeText.text = (moldManager.CurrentMoldSO.SpeedUpgradeCost * moldManager.CurrentMoldClass.SpeedLevel).ToString();
            stockUpgradeButton.interactable = moldManager.CurrentMoldSO.StockUpgradeCost * moldManager.CurrentMoldClass.StockLevel <= GameManager.Instance.MoneyAmount;
            stockUpgradeText.text = (moldManager.CurrentMoldSO.StockUpgradeCost * moldManager.CurrentMoldClass.StockLevel).ToString();
        } else
        {
            automationUpgradeButton.interactable = false;
            automationUpgradeText.text = (moldManager.CurrentMoldSO.AutomationCost * moldManager.CurrentMoldClass.AutomationLevel).ToString();
            speedUpgradeButton.interactable = false;
            speedUpgradeText.text = (moldManager.CurrentMoldSO.SpeedUpgradeCost * moldManager.CurrentMoldClass.SpeedLevel).ToString();
            stockUpgradeButton.interactable = false;
            stockUpgradeText.text = (moldManager.CurrentMoldSO.StockUpgradeCost * moldManager.CurrentMoldClass.StockLevel).ToString();
        }
    }

    public void UpgradeAutomation()
    {
        GameManager.Instance.MoneyAmount -= moldManager.CurrentMoldSO.AutomationCost * moldManager.CurrentMoldClass.AutomationLevel;
        moldManager.CurrentMoldClass.AutomationLevel++;
    }

    public void UpdradeSpeed()
    {
        GameManager.Instance.MoneyAmount -= moldManager.CurrentMoldSO.SpeedUpgradeCost * moldManager.CurrentMoldClass.SpeedLevel;
        moldManager.CurrentMoldClass.SpeedLevel++;
    }

    public void UpgradeStock()
    {
        GameManager.Instance.MoneyAmount -= moldManager.CurrentMoldSO.StockUpgradeCost * moldManager.CurrentMoldClass.StockLevel;
        moldManager.CurrentMoldClass.StockLevel++;
    }

}
