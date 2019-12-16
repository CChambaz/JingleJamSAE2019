using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectManager : MonoBehaviour
{
    [SerializeField] int maximumSnowBasket = 0;
    public int MaximumSnowBasket
    {
        get => maximumSnowBasket;
        set => maximumSnowBasket = value;
    }

    [SerializeField] int currentSnowBasket = 0;
    public int CurrentSnowBasket
    {
        get => currentSnowBasket;
        set => currentSnowBasket = value;
    }

    [SerializeField] int maximumSnowContainer;
    public int MaximumSnowConainer
    {
        get => maximumSnowContainer;
        set => maximumSnowContainer = value;
    }

    [SerializeField] int priceMultiplicator;
    public int PrinceMultiplicator
    {
        get => priceMultiplicator;
        set => priceMultiplicator = value;
    }
}
