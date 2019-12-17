using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Charge : MonoBehaviour
{
    [SerializeField] private TMP_Text taxeText;
    [SerializeField] private TMP_Text maintainText;
    
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

    private void Start()
    {
        slaves = FindObjectOfType<Slaves>();
    }

    // Update is called once per frame
    /*void Update()
    {
        maintainText.text = (installationMaintenance * automationCount).ToString();
        taxeText.text = (taxesSlaveCost * slaves.AmountSlaves + taxesInstallationCost * automationCount).ToString();
    }*/

    public void ApplyMaintenanceCost()
    {
        GameManager.Instance.MoneyAmount -= installationMaintenance * automationCount;
    }

    public void ApplyTaxes()
    {
        GameManager.Instance.MoneyAmount -= taxesSlaveCost * slaves.AmountSlaves + taxesInstallationCost * automationCount + (int)((float)annualRevenu * taxesOnAnnualRevenu);
        annualRevenu = 0;
        
    }
}
