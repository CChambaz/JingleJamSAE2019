using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Charge : MonoBehaviour
{
    [SerializeField] private Text monthlyText;
    [SerializeField] private Text annualText;
    
    [SerializeField] private int taxesSlaveCost;
    [SerializeField] private int taxesInstallationCost;
    [SerializeField] private float taxesOnAnnualRevenu;
    [SerializeField] private int installationMaintenance;
    
    private Slaves slaves;

    private int automationCount = 0;
    public int AutomationCount
    {
        get => automationCount;
        set => automationCount = value;
    }
    
    private int annualRevenu = 0;
    public int AnnualRevenu
    {
        get => annualRevenu;
        set => annualRevenu = value;
    }

    private int maintainCost = 0;
    private int taxesCost = 0;
    
    private void Start()
    {
        slaves = FindObjectOfType<Slaves>();
    }

    // Update is called once per frame
    void Update()
    {
        maintainCost = installationMaintenance * automationCount;
        taxesCost = taxesSlaveCost * slaves.AmountSlaves + taxesInstallationCost * automationCount + (int)((float)annualRevenu * taxesOnAnnualRevenu);

        monthlyText.text = "Monthly charges : " + maintainCost;
        annualText.text = "Annual charges : " + taxesCost;
    }

    public void ApplyMaintenanceCost()
    {
        GameManager.Instance.MoneyAmount -= maintainCost;
    }

    public void ApplyTaxes()
    {
        GameManager.Instance.MoneyAmount -= taxesCost;
        annualRevenu = 0;
        
    }
}
