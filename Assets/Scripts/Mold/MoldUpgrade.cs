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
            automationUpgradeButton.interactable = moldManager.CurrentMoldSO.AutomationCost * (moldManager.CurrentMoldClass.AutomationLevel+1) * GameManager.Instance.StatsManagerInstance.SalesMultiplier <= GameManager.Instance.MoneyAmount;
            speedUpgradeButton.interactable = moldManager.CurrentMoldSO.SpeedUpgradeCost * moldManager.CurrentMoldClass.SpeedLevel * GameManager.Instance.StatsManagerInstance.SalesMultiplier <= GameManager.Instance.MoneyAmount;
            stockUpgradeButton.interactable = moldManager.CurrentMoldSO.StockUpgradeCost * moldManager.CurrentMoldClass.StockLevel * GameManager.Instance.StatsManagerInstance.SalesMultiplier <= GameManager.Instance.MoneyAmount;
        } else
        {
            automationUpgradeButton.interactable = false;
            speedUpgradeButton.interactable = false;
            stockUpgradeButton.interactable = false;
        }
        automationUpgradeText.text = "Level : " + moldManager.CurrentMoldClass.AutomationLevel + "\n Price : " + (moldManager.CurrentMoldSO.AutomationCost * (moldManager.CurrentMoldClass.AutomationLevel+1) * GameManager.Instance.StatsManagerInstance.SalesMultiplier).ToString();
        speedUpgradeText.text = "Level : " + moldManager.CurrentMoldClass.SpeedLevel + "\n Price : " + (moldManager.CurrentMoldSO.SpeedUpgradeCost * moldManager.CurrentMoldClass.SpeedLevel * GameManager.Instance.StatsManagerInstance.SalesMultiplier).ToString();
        stockUpgradeText.text = "Level : " + moldManager.CurrentMoldClass.StockLevel + "\n Price : " + (moldManager.CurrentMoldSO.StockUpgradeCost * moldManager.CurrentMoldClass.StockLevel * GameManager.Instance.StatsManagerInstance.SalesMultiplier).ToString();
    }

    public void UpgradeAutomation()
    {
        GameManager.Instance.MoneyAmount -= (int)(moldManager.CurrentMoldSO.AutomationCost * (moldManager.CurrentMoldClass.AutomationLevel+1) * GameManager.Instance.StatsManagerInstance.SalesMultiplier);
        moldManager.CurrentMoldClass.AutomationLevel++;
    }

    public void UpdradeSpeed()
    {
        GameManager.Instance.MoneyAmount -= (int)(moldManager.CurrentMoldSO.SpeedUpgradeCost * moldManager.CurrentMoldClass.SpeedLevel * GameManager.Instance.StatsManagerInstance.SalesMultiplier);
        moldManager.CurrentMoldClass.SpeedLevel++;
    }

    public void UpgradeStock()
    {
        GameManager.Instance.MoneyAmount -= (int)(moldManager.CurrentMoldSO.StockUpgradeCost * moldManager.CurrentMoldClass.StockLevel * GameManager.Instance.StatsManagerInstance.SalesMultiplier);
        moldManager.CurrentMoldClass.StockLevel++;
    }

}
