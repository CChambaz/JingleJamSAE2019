using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Canvas View_1;
    [SerializeField] private Canvas View_2;
    [SerializeField] private Canvas View_3;

    private Canvas canvasSelect;

    public enum TypeGame
    {
        MAIN_MENU,
        IN_GAME_1,
        IN_GAME_2,
        IN_GAME_3,
        IN_PAUSE
    }

    private TypeGame type = TypeGame.IN_GAME_1;

    public const int SNOWBALL_TYPE_COUNT = 4;

    enum SnowballType
    {
        ROUND,
        STAR,
        SQUARE,
        FLAKE
    }

    public void Start()
    {

        canvasSelect = View_1;

        SetTypeGame((int)type);
    }

    void ChangeCanavasSelect()
    {


    }

    public static GameManager Instance;

    void Awake()
    {
        Instance = this;
    }

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

    public void SetTypeGame(int type)
    {
        switch (type)
        {
            case (int)TypeGame.IN_GAME_1:
                this.type = TypeGame.IN_GAME_1;
                View_1.gameObject.SetActive(true);
                View_2.gameObject.SetActive(false);
                View_3.gameObject.SetActive(false);
                break;
            case (int)TypeGame.IN_GAME_2:
                this.type = TypeGame.IN_GAME_2;
                View_1.gameObject.SetActive(false);
                View_2.gameObject.SetActive(true);
                View_3.gameObject.SetActive(false);
                break;
            case (int)TypeGame.IN_GAME_3:
                this.type = TypeGame.IN_GAME_3;
                View_1.gameObject.SetActive(false);
                View_2.gameObject.SetActive(false);
                View_3.gameObject.SetActive(true);
                break;
        }
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
