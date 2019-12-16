using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoldManager : MonoBehaviour
{
    [SerializeField] private MoldSO[] molds;
    public MoldSO[] Molds => molds;

    private MoldSO currentMold;
    public MoldSO CurrentMold => currentMold;

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
            currentMold = molds[selectedMold];
            moldProduction.OnMoldChange();
        }
    }

    void Awake()
    {
        currentMold = molds[selectedMold];
    }

    void Start()
    {
        moldProduction = FindObjectOfType<MoldProduction>();
    }

    public void UpdradeSpeed(float newSpeed)
    {
        molds[selectedMold].TimerSpeed += newSpeed;
    }
}
