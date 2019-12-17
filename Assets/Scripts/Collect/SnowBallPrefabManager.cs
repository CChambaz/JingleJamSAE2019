using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowBallPrefabManager : MonoBehaviour
{
    [SerializeField] Rigidbody2D snowBallRB;
    [SerializeField] float fallingSpeed;
    [SerializeField] float speed;
    [SerializeField] float YDistanceDestroy;
    int tempestForce;
    [SerializeField] private AnimationCurve curve;
    private float timer = 0;
    private float startObjective;

    private void Start()
    {
        tempestForce = GameManager.Instance.StatsManagerInstance.TempestForce;
        fallingSpeed *= GameManager.Instance.StatsManagerInstance.FallingSpeed;
        switch (tempestForce)
        {
            case 0:
            {
                snowBallRB.velocity = Vector2.down * fallingSpeed;
                break;
            }
            case 1:
            {
                snowBallRB.velocity = GameManager.Instance.StatsManagerInstance.TempestDirection * fallingSpeed;
                break;
            }
            case 3:
            {
                startObjective = (transform.position - transform.position * 2f).x;
                snowBallRB.velocity = new Vector2(-Mathf.Sign(transform.position.x), -fallingSpeed);
                break;
            }
        }
        snowBallRB.velocity *= speed;
    }

    private void Update()
    {
        if (snowBallRB.transform.position.y <= YDistanceDestroy)
        {
            Destroy(gameObject);                //To change when pooling system
        }
        switch (tempestForce)
        {
            case 0:
                {
                    snowBallRB.velocity = Vector2.down * fallingSpeed;
                    break;
                }
            case 1:
            {
                snowBallRB.velocity = GameManager.Instance.StatsManagerInstance.TempestDirection * fallingSpeed;
                break;
            }
            case 2:
            {
                snowBallRB.velocity = new Vector2(curve.Evaluate(timer), -fallingSpeed) * speed;
                timer += Time.deltaTime;
                break;
            }
            case 3:
            {
                if (Mathf.Abs(transform.position.x - startObjective)<0.1f)
                {
                    startObjective = (transform.position - transform.position * 1.5f).x;
                    snowBallRB.velocity = new Vector2(-Mathf.Sign(transform.position.x), -fallingSpeed) * speed;
                } else
                {
                    snowBallRB.velocity = new Vector2(-Mathf.Sign(transform.position.x), -fallingSpeed);
                }
                break;
            }
        }
    }
}
