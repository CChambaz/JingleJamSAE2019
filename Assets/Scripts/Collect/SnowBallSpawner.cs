using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowBallSpawner : MonoBehaviour
{
    [SerializeField] float spawnY = 0;
    [SerializeField] float spawnMinX = 0;
    [SerializeField] float spawnMaxX = 0;
    [SerializeField] GameObject snowBallPrefab;
    [SerializeField] float spawnTime = 0;
    [SerializeField] float currentTime = 0;

    private void Start()
    {
        
    }

    private void Update()
    {
        currentTime += Time.deltaTime;
        if (currentTime >= spawnTime)
        {
            Instantiate(snowBallPrefab, new Vector3(Random.Range(spawnMinX, spawnMaxX), spawnY, 0), Quaternion.identity);
            currentTime = 0;
        }
    }


}
