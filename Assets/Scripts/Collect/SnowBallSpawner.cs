﻿using System.Collections;
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

    [SerializeField] float randomSpawnFloatTime = 0.25f;
    [SerializeField] float currentRandom = 0;

    private void Start()
    {
        
    }

    private void Update()
    {
        currentTime += Time.deltaTime;
        if (currentTime + currentRandom >= spawnTime)
        {
            Instantiate(snowBallPrefab, new Vector3(Random.Range(spawnMinX, spawnMaxX), spawnY, 0), Quaternion.identity, gameObject.transform);
            currentRandom = Random.Range(-randomSpawnFloatTime, randomSpawnFloatTime);
            currentTime = 0;
        }
    }


}
