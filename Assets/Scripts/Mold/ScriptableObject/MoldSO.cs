using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu]
public class MoldSO : ScriptableObject
{
    [SerializeField] private float maxTimer;
    public float MaxTimer => maxTimer;
    [SerializeField] private float timerSpeed;
    public float TimerSpeed => timerSpeed;
    [SerializeField] private int ballRate;
    public int BallRate => ballRate;
    [SerializeField] private int snowCost;
    public int SnowCost => snowCost;
    [SerializeField] private int maxStock;
    public int MaxStock => maxStock;
    [SerializeField] private int automationCost;
    public int AutomationCost => automationCost;
    [SerializeField] private int speedUpgradeCost;
    public int SpeedUpgradeCost => speedUpgradeCost;
    [SerializeField] private int stockUpgradeCost;
    public int StockUpgradeCost => stockUpgradeCost;
    [SerializeField] private int unlockedCost;
    public int UnlockedCost => unlockedCost;

}
