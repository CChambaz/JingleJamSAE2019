using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowBallSpawner : MonoBehaviour
{
    [SerializeField] float spawnY = 0;
    [SerializeField] float spawnMinX = 0;
    [SerializeField] float spawnMaxX = 0;
    [SerializeField] GameObject snowBallPrefab;
    [SerializeField] GameObject stonePrefab;
    [SerializeField] float spawnTime = 0;
    [SerializeField] float currentTime = 0;

    [SerializeField] float randomSpawnFloatTime = 0.25f;
    [SerializeField] float currentRandom = 0;
    [SerializeField] int amountOfSnowforOneStone = 10;
    [SerializeField] int randomPrefabNumber;


    private void Start()
    {
        
    }

    private void Update()
    {
        currentTime += Time.deltaTime;
        if (currentTime + currentRandom >= spawnTime * GameManager.Instance.StatsManagerInstance.SpawnTimeMultiplier)
        {
            if (randomPrefabNumber == 0)
            {
                Instantiate(stonePrefab, new Vector3(Random.Range(spawnMinX, spawnMaxX), spawnY, 0), Quaternion.identity, gameObject.transform);
            }
            else
            {
                Instantiate(snowBallPrefab, new Vector3(Random.Range(spawnMinX, spawnMaxX), spawnY, 0), Quaternion.identity, gameObject.transform);
            }
            currentRandom = Random.Range(-randomSpawnFloatTime * GameManager.Instance.StatsManagerInstance.RandomSpawnFloatTimeMultiplier, randomSpawnFloatTime * GameManager.Instance.StatsManagerInstance.RandomSpawnFloatTimeMultiplier);
            currentTime = 0;
            randomPrefabNumber = Random.Range(0, amountOfSnowforOneStone + 1);
        }
    }


}
