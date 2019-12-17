using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowProduction : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Vector2 targetPosition;
    void Update()
    {
        if (Mathf.Abs(transform.position.y - targetPosition.y) > speed)
        {
            transform.position -= Vector3.up * speed * Mathf.Sign(transform.position.y - targetPosition.y);
        } else
        {
            if (Mathf.Abs(transform.position.x - targetPosition.x) > speed)
            {
                transform.position -= Vector3.right * speed * Mathf.Sign(transform.position.x - targetPosition.x);
            } else
            {
                Destroy(gameObject);
            }

        }
    }
}
