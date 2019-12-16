﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasketController : MonoBehaviour
{
    [SerializeField] float movementMinX;
    [SerializeField] float movementMaxX;
    [SerializeField] CollectManager collectManager;

    [SerializeField] float downDragDistance;
    [SerializeField] float downDragTime;
    [SerializeField] Rigidbody2D basketRB;

    float currentPressedTime;
    float currentDragYPosition;

    bool canMoveBasket = true;

    [SerializeField] float moveBasketVelocity;
    [SerializeField] float basketEmptyingPositionY;
    float basketPositionYBackup;
    bool hasStartedWaitCoroutine = false;
    [SerializeField] float waitTime;

    enum BasketEmptyingStatus
    {
        GO_DOWN,
        WAIT_EMPTY,
        GO_UP,
    }

    BasketEmptyingStatus basketEmptyingStatus = BasketEmptyingStatus.GO_DOWN;

    private void Update()
    {
        if (canMoveBasket)
        {
            if (Input.GetMouseButtonDown(0))                //Not sure it works on mobile
            {
                currentPressedTime = Time.time;
                currentDragYPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition).y;
            }
            else if (Input.GetMouseButtonUp(0))             //Not sure it works on mobile
            {
                if (Time.time < currentPressedTime + downDragTime)
                {
                    if (downDragDistance < currentDragYPosition - Camera.main.ScreenToWorldPoint(Input.mousePosition).y)
                    {
                        Debug.Log("Test");
                        canMoveBasket = false;
                        basketPositionYBackup = transform.position.y;
                        basketEmptyingStatus = BasketEmptyingStatus.GO_DOWN;
                    }
                }
            }
        }
        else
        {
            switch (basketEmptyingStatus)
            {
                case BasketEmptyingStatus.GO_DOWN:
                    MoveBasketDown();
                    break;
                case BasketEmptyingStatus.WAIT_EMPTY:
                    WaitEmpty();
                    break;
                case BasketEmptyingStatus.GO_UP:
                    MoveBasketUp();
                    break;
            }
        }
    }

    private void OnMouseDrag()
    {
        if (canMoveBasket)
        {
            gameObject.transform.position = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, transform.position.y);

            if (gameObject.transform.position.x < movementMinX)
            {
                gameObject.transform.position = new Vector2(movementMinX, transform.position.y);
            }
            else if (gameObject.transform.position.x > movementMaxX)
            {
                gameObject.transform.position = new Vector2(movementMaxX, transform.position.y);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (canMoveBasket)
        {
            if (collision.tag == "SnowBall")
            {
                Destroy(collision.gameObject);
                collectManager.CurrentSnowBasket++;
                if (collectManager.CurrentSnowBasket > collectManager.MaximumSnowBasket)
                {
                    collectManager.CurrentSnowBasket = collectManager.MaximumSnowBasket;
                }
            }
        }
    }

    void MoveBasketDown()
    {
        basketRB.velocity = new Vector2(basketRB.velocity.x, -moveBasketVelocity);
        if (transform.position.y < basketEmptyingPositionY)
        {
            basketRB.velocity = Vector2.zero;
            transform.position = new Vector2(transform.position.x, basketEmptyingPositionY);
            basketEmptyingStatus = BasketEmptyingStatus.WAIT_EMPTY;
        }
    }

    void WaitEmpty()
    {
        if (!hasStartedWaitCoroutine)
        {
            StartCoroutine("WaitEmptyCoroutine");
            hasStartedWaitCoroutine = true;
        }
    }

    void MoveBasketUp()
    {
        basketRB.velocity = new Vector2(basketRB.velocity.x, moveBasketVelocity);
        if (transform.position.y > basketPositionYBackup)
        {
            basketRB.velocity = Vector2.zero;
            transform.position = new Vector2(transform.position.x, basketPositionYBackup);
            canMoveBasket = true;
            basketEmptyingStatus = BasketEmptyingStatus.GO_DOWN;
        }
    }

    IEnumerator WaitEmptyCoroutine()
    {
        yield return new WaitForSeconds(waitTime);
        GameManager.Instance.SnowAmount += collectManager.CurrentSnowBasket;
        if (GameManager.Instance.SnowAmount > collectManager.MaximumSnowConainer)
        {
            GameManager.Instance.SnowAmount = collectManager.MaximumSnowConainer;
        }
        Debug.Log(GameManager.Instance.SnowAmount);
        collectManager.CurrentSnowBasket = 0;
        basketEmptyingStatus = BasketEmptyingStatus.GO_UP;
        hasStartedWaitCoroutine = false;
    }
}
