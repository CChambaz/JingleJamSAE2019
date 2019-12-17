using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SnowBallSpawner : MonoBehaviour
{
    [SerializeField] float spawnY = 0;
    [SerializeField] float spawnMinX = 0;
    [SerializeField] float spawnMaxX = 0;
    [SerializeField] GameObject snowBallPrefab;
    [SerializeField] float spawnTime = 0;
    [SerializeField] float currentTime = 0;

    [SerializeField] float randomSpawnFloatTime = 0.25f;
    [SerializeField] float currentRandom = 0;

    void Start()
    {
        spawnMaxX *= GameManager.Instance.StatsManagerInstance.SpawningSize;
        spawnMinX *= GameManager.Instance.StatsManagerInstance.SpawningSize;
    }

    private void Update()
    {
        currentTime += Time.deltaTime;
        if (currentTime + currentRandom >= spawnTime * GameManager.Instance.StatsManagerInstance.SpawnTimeMultiplier)
        {
            Instantiate(snowBallPrefab, new Vector3(Random.Range(spawnMinX, spawnMaxX), spawnY, 0), Quaternion.identity, gameObject.transform);
            currentRandom = Random.Range(-randomSpawnFloatTime * GameManager.Instance.StatsManagerInstance.RandomSpawnFloatTimeMultiplier, randomSpawnFloatTime * GameManager.Instance.StatsManagerInstance.RandomSpawnFloatTimeMultiplier);
            currentTime = 0;
        }
    }
}
