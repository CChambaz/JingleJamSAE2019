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
        if (GameManager.Instance.MoneyAmount > basketCapacityPrice)           //You have money to pay
        {
            collectManager.MaximumSnowBasket += increaseCapacityBasketNumber;
            GameManager.Instance.MoneyAmount -= basketCapacityPrice;
            basketCapacityPrice *= collectManager.PrinceMultiplicator;
            Debug.Log(collectManager.MaximumSnowBasket);
            Debug.Log(GameManager.Instance.MoneyAmount);
        }
    }

    public void IncreaseContainerCapacity()
    {
        if (GameManager.Instance.MoneyAmount > containerCapacityPrice)           //You have money to pay
        {
            collectManager.MaximumSnowConainer += increaseCapacityContainerNumber;
            GameManager.Instance.MoneyAmount -= containerCapacityPrice;
            containerCapacityPrice *= collectManager.PrinceMultiplicator;
            Debug.Log(collectManager.MaximumSnowConainer);
            Debug.Log(GameManager.Instance.MoneyAmount);
        }
    }
}
