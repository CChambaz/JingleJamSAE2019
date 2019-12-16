using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowBallPrefabManager : MonoBehaviour
{
    [SerializeField] Rigidbody2D snowBallRB;
    [SerializeField] float fallingSpeed;
    [SerializeField] float YDistanceDestroy;

    private void Start()
    {
        snowBallRB.velocity = new Vector3(0, fallingSpeed, 0);
    }

    private void Update()
    {
        if (snowBallRB.transform.position.y <= YDistanceDestroy)
        {
            Destroy(gameObject);                //To change when pooling system
        }
    }
}
