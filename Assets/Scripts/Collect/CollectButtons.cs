using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CollectButtons : MonoBehaviour
{
    [SerializeField] CollectManager collectManager;
    [SerializeField] Slaves slavesManager;
    [SerializeField] int basketCapacityPrice = 10;
    [SerializeField] int containerCapacityPrice = 10;
    [SerializeField] int increaseCapacityBasketNumber = 10;
    [SerializeField] int increaseCapacityContainerNumber = 30;
    [SerializeField] TextMeshProUGUI IncreaseBasketTMP;
    [SerializeField] TextMeshProUGUI IncreaseContainerTMP;
    [SerializeField] TextMeshProUGUI IncreaseSlavesTMP;

    private void Start()
    {
        slavesManager = FindObjectOfType<Slaves>();
    }

    private void Update()
    {
        if (GameManager.Instance.InPause)
        {
            return;
        }
        IncreaseBasketTMP.text = "Capacity: " + collectManager.MaximumSnowBasket + "\n" + "Cost: " + (basketCapacityPrice * GameManager.Instance.StatsManagerInstance.SalesMultiplier);
        IncreaseContainerTMP.text = "Capacity: " + collectManager.MaximumSnowConainer + "\n" + "Cost: " + (containerCapacityPrice * GameManager.Instance.StatsManagerInstance.SalesMultiplier);
        IncreaseSlavesTMP.text = "Slaves: " + slavesManager.AmountSlaves + "\n" + "Cost: " + (slavesManager.SlavePrice * GameManager.Instance.StatsManagerInstance.SalesMultiplier);
    }

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

    public void IncreaseSlavesAmount()
    {
        if (GameManager.Instance.MoneyAmount > slavesManager.SlavePrice * GameManager.Instance.StatsManagerInstance.SalesMultiplier)
        {
            slavesManager.AmountSlaves += 1;
            GameManager.Instance.MoneyAmount -= (int)(slavesManager.SlavePrice * GameManager.Instance.StatsManagerInstance.SalesMultiplier);
        }
    }
}
