using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectManager : MonoBehaviour
{
    [SerializeField] int maximumSnow = 0;
    public int MaximumSnow
    {
        get => maximumSnow;
        set => maximumSnow = value;
    }

    [SerializeField] int currentSnow = 0;
    public int CurrentSnow
    {
        get => currentSnow;
        set => currentSnow = value;
    }
}
