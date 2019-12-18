using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowProduction : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Vector2 targetPosition;
    public Vector2 TargetPosition
    {
        get => targetPosition;
        set => targetPosition = value;
    }
    void Update()
    {
        if (Mathf.Abs(transform.position.y - targetPosition.y) > 0.1)
        {
            transform.position -= Vector3.up * speed * Mathf.Sign(transform.position.y - targetPosition.y) * Time.deltaTime * 10;
        } else
        { 
            if (Mathf.Abs(transform.position.x - targetPosition.x) > 0.1)
            {
                transform.position -= Vector3.right * speed * Mathf.Sign(transform.position.x - targetPosition.x) * Time.deltaTime;
            } else
            {
                Destroy(gameObject);
            }
        }
    }
}
