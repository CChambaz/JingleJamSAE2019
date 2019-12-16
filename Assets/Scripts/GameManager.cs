using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    enum SnowballType
    {
        ROUND,
        STAR,
        SQUARE,
        FLAKE
    }

    public static GameManager Instance;

    private int snowAmount;
    public int SnowAmount
    {
        get => snowAmount;
        set => snowAmount = value;
    }

    private int[] snowballAmount;
    public int[] SnowballAmount
    {
        get => snowballAmount;
        set => snowballAmount = value;
    }

    private int moneyAmount;
    public int MoneyAmount
    {
        get => moneyAmount;
        set => moneyAmount = value;
    }

    [SerializeField] private Canvas menu;
    private bool inPause = false;

    public void BtnPause()
    {
        inPause = !inPause;
        Debug.Log(inPause);
        if (inPause)
        {
            menu.gameObject.SetActive(true);
            menu.GetComponent<RectTransform>().position = Vector3.zero;

        }
        else
        {
            menu.GetComponent<RectTransform>().position = Vector3.up * 10;
            menu.gameObject.SetActive(false);
        }
    }
}
