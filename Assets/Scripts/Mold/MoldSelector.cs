using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using UnityEngine;

public class MoldSelector : MonoBehaviour {
    [SerializeField] private GameObject[] moldSprites;
    private MoldManager moldManager;

    private Vector2 centerPosition;

    float currentPressedTime;
    float currentDragPosition;
    [SerializeField] float minDragTime;
    [SerializeField] float minDragDistance;

    [SerializeField] float animationSpeed;
    private int[] currentMoving = new int[2];
    // Start is called before the first frame update
    void Start()
    {
        centerPosition = moldSprites[0].transform.position;
        moldManager = FindObjectOfType<MoldManager>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))                //Not sure it works on mobile
        {
            currentPressedTime = Time.time;
            currentDragPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition).x;
        }
        else if (Input.GetMouseButtonUp(0))             //Not sure it works on mobile
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
        for (int i = 0; i < 4; i++)
        {
            moldSprites[i].transform.position = centerPosition + Vector2.left * 550 * arrowIndex;
        }
        //moldSprites[moldManager.SelectedMold].SetActive(false);
        currentMoving[0] = moldManager.SelectedMold;
        moldSprites[moldManager.SelectedMold].transform.position = centerPosition;
        StartCoroutine(AnimationMove(arrowIndex, centerPosition + Vector2.left * 550 * arrowIndex, moldManager.SelectedMold));
        moldManager.SelectedMold += arrowIndex;
        //moldSprites[(int)moldManager.SelectedMold].SetActive(true);
        currentMoving[1] = moldManager.SelectedMold;
        moldSprites[moldManager.SelectedMold].transform.position = centerPosition + Vector2.right * 550 * arrowIndex;
        StartCoroutine(AnimationMove(arrowIndex, centerPosition, moldManager.SelectedMold));
    }
    
    IEnumerator AnimationMove(int arrowIndex, Vector2 dest, int moldIndex)
    {
        while (Mathf.Abs(moldSprites[moldIndex].transform.position.x - dest.x) > 10 && currentMoving.Contains(moldIndex))
        {
            moldSprites[moldIndex].transform.position += Vector3.left * arrowIndex * animationSpeed;
            yield return null;
        }
        moldSprites[moldIndex].transform.position = dest;
    }
}
