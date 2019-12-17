using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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
    bool hasStartedWaitTimer = false;
    [SerializeField] float waitTime;
    [SerializeField] TMP_Text amountBasketTMP;
    [SerializeField] Transform basketSpriteTransform;
    [SerializeField] Image basketContentPercentageImage;

    enum BasketEmptyingStatus
    {
        GO_DOWN,
        WAIT_EMPTY,
        GO_UP,
    }
    float waitTimeBasketDown;

    BasketEmptyingStatus basketEmptyingStatus = BasketEmptyingStatus.GO_DOWN;

    private void Update()
    {
        if (GameManager.Instance.InPause)
        {
            return;
        }
        //amountBasketTMP
        //amountBasketTMP.rectTransform.position = Camera.main.WorldToScreenPoint(basketSpriteTransform.position);
        amountBasketTMP.text = collectManager.CurrentSnowBasket.ToString();
        basketContentPercentageImage.fillAmount = (float)collectManager.CurrentSnowBasket / (float)collectManager.MaximumSnowBasket;
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

            if (collision.tag == "Stone")
            {
                Destroy(collision.gameObject);
                collectManager.CurrentSnowBasket = 0;
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
        if (!hasStartedWaitTimer)
        {
            waitTimeBasketDown = Time.time;
            hasStartedWaitTimer = true;
        }

        if (Time.time > waitTimeBasketDown + waitTime)
        {

            GameManager.Instance.SnowAmount += collectManager.CurrentSnowBasket;
            if (GameManager.Instance.SnowAmount > collectManager.MaximumSnowConainer)
            {
                GameManager.Instance.SnowAmount = collectManager.MaximumSnowConainer;
            }
            collectManager.CurrentSnowBasket = 0;
            basketEmptyingStatus = BasketEmptyingStatus.GO_UP;
            hasStartedWaitTimer = false;
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
}
