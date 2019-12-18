using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelpFingerDown : MonoBehaviour
{
    [SerializeField] float fingerDistance;
    [SerializeField] GameObject fingerPrefab;
    [SerializeField] CollectManager collectManager;
    [SerializeField] float timeBeforeItAppears;
    [SerializeField] SpriteRenderer fingerSpriteRenderer;
    GameObject fingerPrefabInstance;
    Rigidbody2D rigidbody2D;
    bool hasStartedTime = false;
    bool showFinger = false;

    float initialPosition;
    float currentTime;
    float currentColor;
    [SerializeField] float timeToAppear;
    [SerializeField] float timeToDisapear;
    [SerializeField] float speed;

    enum FingerStates
    {
        FINGER_APPEARS,
        FINGER_GO_DOWN,
        FINGER_WAIT_TO_DISAPEAR,
        FINGER_DISAPEAR,
    }

    FingerStates fingerStates;

    private void Update()
    {
        if (GameManager.Instance.InPause)
        {
            currentTime += Time.deltaTime;
            return;
        }

        if (collectManager.CurrentSnowBasket >= collectManager.MaximumSnowBasket)
        {
            if (!hasStartedTime && !showFinger)
            {
                currentTime = Time.time;
                hasStartedTime = true;
            }
            if (Time.time > currentTime + timeBeforeItAppears && !showFinger)
            {
                fingerPrefabInstance = Instantiate(fingerPrefab, transform);
                fingerSpriteRenderer = fingerPrefabInstance.GetComponentInChildren<SpriteRenderer>();

                fingerSpriteRenderer.color = new Color(1,1,1,0);
                currentColor = 0;
                rigidbody2D = fingerPrefabInstance.GetComponent<Rigidbody2D>();
                initialPosition = fingerPrefabInstance.transform.position.y;

                showFinger = true;
            }

            if (showFinger)
            {
                switch (fingerStates)
                {
                    case FingerStates.FINGER_APPEARS:
                        FingerAppears();
                        break;
                    case FingerStates.FINGER_GO_DOWN:
                        FingerGoDown();
                        break;
                    case FingerStates.FINGER_WAIT_TO_DISAPEAR:
                        FingerWaitToDisapear();
                        break;
                    case FingerStates.FINGER_DISAPEAR:
                        FingerDisapear();
                        break;
                }
            }
        }
        else
        {
            showFinger = false;
            hasStartedTime = false;
            Destroy(fingerPrefabInstance);
        }

        void FingerAppears()
        {
            currentColor += timeToAppear * Time.deltaTime;

            if (currentColor >= 1)
            {
                currentColor = 1;
                fingerSpriteRenderer.color = new Color(1, 1, 1, 1);
                fingerStates = FingerStates.FINGER_GO_DOWN;
            }
            else
            {
                fingerSpriteRenderer.color = new Color(1, 1, 1, currentColor);
            }
        }

        void FingerGoDown()
        {
            rigidbody2D.velocity = new Vector2(0, -speed);
            Debug.Log(fingerPrefabInstance.transform.position.y);
            Debug.Log(initialPosition - fingerDistance);
            if (fingerPrefabInstance.transform.position.y < initialPosition - fingerDistance)
            {
                rigidbody2D.velocity = new Vector2(0, 0);
                fingerStates = FingerStates.FINGER_DISAPEAR;
            }
        }

        void FingerWaitToDisapear()
        {

        }

        void FingerDisapear()
        {
            currentColor -= timeToAppear * Time.deltaTime;

            if (currentColor <= 0)
            {
                currentColor = 0;
                fingerSpriteRenderer.color = new Color(1, 1, 1, 0);
                fingerStates = FingerStates.FINGER_APPEARS;
                showFinger = false;
            }
            else
            {
                fingerSpriteRenderer.color = new Color(1, 1, 1, currentColor);
            }
        }
    }

}
