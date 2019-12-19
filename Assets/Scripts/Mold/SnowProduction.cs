using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowProduction : MonoBehaviour
{
    [SerializeField] private float verticalSpeed;
    [SerializeField] private float horizontalSpeed;
    [SerializeField] private Vector2 targetPosition;
    private float currentTime = 0;
    private bool verical = true;
    private Vector2 startPositon;

    void Start()
    {
        startPositon = transform.position;
    }

    public Vector2 TargetPosition
    {
        get => targetPosition;
        set => targetPosition = value;
    }
    void Update()
    {
        if (verical)
        {
            currentTime += Time.deltaTime * verticalSpeed;
            if (currentTime < 1)
            {
                transform.position = Vector3.Lerp(startPositon, new Vector3(transform.position.x, targetPosition.y), currentTime);
            }
            else
            {
                transform.position = new Vector3(transform.position.x, targetPosition.y);
                verical = false;
                currentTime = 0;
                startPositon = transform.position;
            }
        } else
        {
            currentTime += Time.deltaTime * horizontalSpeed;
            if (currentTime < 1)
            {
                transform.position = Vector3.Lerp(startPositon, targetPosition, currentTime);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}
