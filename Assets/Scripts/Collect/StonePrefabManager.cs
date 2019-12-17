using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StonePrefabManager : MonoBehaviour
{
    [SerializeField] Rigidbody2D stoneRB;
    [SerializeField] float fallingSpeed;
    [SerializeField] float YDistanceDestroy;

    private void Start()
    {
        stoneRB.velocity = new Vector3(0, fallingSpeed, 0);
    }

    private void Update()
    {
        if (GameManager.Instance.InPause)
        {
            return;
        }
        if (stoneRB.transform.position.y <= YDistanceDestroy)
        {
            Destroy(gameObject);                //To change when pooling system
        }
    }
}
