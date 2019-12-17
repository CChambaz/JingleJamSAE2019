using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoldClass
{

    public bool unlocked = false;
    public bool Unlocked
    {
        get => unlocked;
        set => unlocked = value;
    }

    private int stockLevel = 1;
    public int StockLevel
    {
        get => stockLevel;
        set
        {
            stockLevel = value;
        }
    }

    private int speedLevel = 1;
    public int SpeedLevel
    {
        get => speedLevel;
        set
        {
            speedLevel = value;
        }
    }

    private int automationLevel = 0;
    public int AutomationLevel
    {
        get => automationLevel;
        set
        {
            automationLevel = value;
        }
    }
}

public class MoldManager : MonoBehaviour
{
    [SerializeField] private MoldSO[] moldsSO;
    public MoldSO[] MoldsSO => moldsSO;

    private MoldClass[] moldClasses = new MoldClass[4];
    public MoldClass[] MoldClasses
    {
        get => moldClasses;
        set => moldClasses = value;
    }

    private MoldSO currentMoldSO;
    public MoldSO CurrentMoldSO => currentMoldSO;

    private MoldClass currentMoldClass;
    public MoldClass CurrentMoldClass
    {
        get => currentMoldClass;
        set => currentMoldClass = value;
    }

    private MoldProduction moldProduction;

    private const int snowballTypeCount = 4;

    private int selectedMold = 0;
    public int SelectedMold
    {
        get => selectedMold;
        set
        {
            selectedMold = value;
            selectedMold = selectedMold % (snowballTypeCount);
            if (selectedMold < 0)
            {
                selectedMold = snowballTypeCount - 1;
            }
            currentMoldSO = moldsSO[selectedMold];
            currentMoldClass = moldClasses[selectedMold];
            moldProduction.OnMoldChange();
        }
    }

    void Awake()
    {
        for (int i = 0; i < moldClasses.Length; i++)
        {
            moldClasses[i] = new MoldClass();
        }
        currentMoldSO = moldsSO[selectedMold];
        currentMoldClass = moldClasses[selectedMold];
    }

    void Start()
    {
        moldProduction = FindObjectOfType<MoldProduction>();
    }
}
