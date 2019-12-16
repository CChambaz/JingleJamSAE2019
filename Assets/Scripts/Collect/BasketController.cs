using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasketController : MonoBehaviour
{
    [SerializeField] float movementMinX;
    [SerializeField] float movementMaxX;
    [SerializeField] CollectManager collectManager;

    [SerializeField] float downDragDistance;
    [SerializeField] float downDragTime;
    float currentPressedTime;
    float currentDragYPosition;

    bool canMoveBasket = true;

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
                    }
                }
            }
        }
        else
        {
            switch (basketEmptyingStatus)
            {
                case BasketEmptyingStatus.GO_DOWN:
                    break;
                case BasketEmptyingStatus.WAIT_EMPTY:
                    break;
                case BasketEmptyingStatus.GO_UP:
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
                GameManager.Instance.SnowAmount++;
                if (GameManager.Instance.SnowAmount > collectManager.MaximumSnow)
                {
                    GameManager.Instance.SnowAmount = collectManager.MaximumSnow;
                }
                Debug.Log(GameManager.Instance.SnowAmount);
            }
        }
    }
}
