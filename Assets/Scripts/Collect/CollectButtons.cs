using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectButtons : MonoBehaviour
{
    [SerializeField] CollectManager collectManager;
    [SerializeField] int basketCapacityPrice = 10;
    [SerializeField] int containerCapacityPrice = 10;
    [SerializeField] int increaseCapacityBasketNumber = 10;
    [SerializeField] int increaseCapacityContainerNumber = 30;

    public void IncreaseBasketCapacity()
    {
        if (GameManager.Instance.MoneyAmount * GameManager.Instance.StatsManagerInstance.SalesMultiplier > basketCapacityPrice)           //You have money to pay
        {
            collectManager.MaximumSnowBasket += increaseCapacityBasketNumber;
            GameManager.Instance.MoneyAmount -= (int)(basketCapacityPrice * GameManager.Instance.StatsManagerInstance.SalesMultiplier);
            basketCapacityPrice *= collectManager.PrinceMultiplicator;
        }
    }

    public void IncreaseContainerCapacity()
    {
        if (GameManager.Instance.MoneyAmount * GameManager.Instance.StatsManagerInstance.SalesMultiplier > containerCapacityPrice)           //You have money to pay
        {
            collectManager.MaximumSnowConainer += increaseCapacityContainerNumber;
            GameManager.Instance.MoneyAmount -= (int)(containerCapacityPrice * GameManager.Instance.StatsManagerInstance.SalesMultiplier);
            containerCapacityPrice *= collectManager.PrinceMultiplicator;
        }
    }
}
