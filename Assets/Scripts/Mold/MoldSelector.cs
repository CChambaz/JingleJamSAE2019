﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using UnityEngine;

public class MoldSelector : MonoBehaviour {
    [SerializeField] private GameObject[] moldSprites;
    private MoldManager moldManager;

    private Vector2 centerPosition;

    float currentPressedTime = -1;
    float currentDragPosition;
    [SerializeField] float minDragTime;
    [SerializeField] float minDragDistance;

    [SerializeField] float animationSpeed;
    private int[] currentMoving = new int[]{-1,-1};
    private float[] currentDestination = new float[2];
    private int[] currentOrientation = new int[2];
    // Start is called before the first frame update
    void Start()
    {
        centerPosition = moldSprites[0].transform.position;
        moldManager = FindObjectOfType<MoldManager>();
        currentMoving[0] = 0;
        currentDestination[0] = centerPosition.x;
    }

    void Update()
    {
        AnimationMove();
        if (GameManager.Instance.InPause || GameManager.Instance.Type != GameManager.GameState.IN_GAME_2)
        {
            currentPressedTime = Time.time;
            return;
        }
        if (Input.GetMouseButtonDown(0))
        {
            currentPressedTime = Time.time;
            currentDragPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition).x;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            if (Time.time > currentPressedTime + minDragTime)
            {
                if (minDragDistance < Mathf.Abs(currentDragPosition - Camera.main.ScreenToWorldPoint(Input.mousePosition).x))
                {
                    if (currentDragPosition > Camera.main.ScreenToWorldPoint(Input.mousePosition).x)
                    {
                        MoveSelector(1);
                    } else
                    {
                        MoveSelector(-1);
                    }
                }
            }
        }
    }
    public void MoveSelector(int arrowIndex) {
        if (GameManager.Instance.InPause)
        {
            return;
        }
        arrowIndex *= -1;
        if (MotherFuckingAudioManager.Instance)
        {
            MotherFuckingAudioManager.Instance.PlayAlert(MotherFuckingAudioManager.AlertList.CLICK);
        }

        //moldSprites[moldManager.SelectedMold].SetActive(false);
        currentMoving[0] = moldManager.SelectedMold;
        currentDestination[0] = centerPosition.x + 5 * arrowIndex;
        currentOrientation[0] = arrowIndex;
        moldSprites[moldManager.SelectedMold].transform.position = centerPosition;
        moldManager.SelectedMold -= arrowIndex;
        //moldSprites[(int)moldManager.SelectedMold].SetActive(true);
        currentMoving[1] = moldManager.SelectedMold;
        currentDestination[1] = centerPosition.x;
        currentOrientation[1] = arrowIndex;
        moldSprites[moldManager.SelectedMold].transform.position = centerPosition - Vector2.right * 4 * arrowIndex;
    }

    void AnimationMove()
    {
        for (int i = 0; i < 4; i++)
        {
            int index = -1;
            if (currentMoving[0] == i)
            {
                index = 0;
            }
            else if (currentMoving[1] == i)
            {
                index = 1;
            }
            if (index != -1)
            {
                if (Mathf.Abs(moldSprites[i].transform.position.x - currentDestination[index]) > 0.2f)
                {
                    moldSprites[i].transform.position += Vector3.right * currentOrientation[index] * Time.deltaTime * animationSpeed;
                }
                else
                {
                    moldSprites[i].transform.position = new Vector2(currentDestination[index], moldSprites[i].transform.position.y);
                }
            }
            else
            {
                moldSprites[i].transform.position = centerPosition + Vector2.right * 4;
            }
        }
    }
}
