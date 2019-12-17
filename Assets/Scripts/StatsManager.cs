using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsManager : MonoBehaviour
{
    //const float spawnTime;
    //const float randomSpawnFloadTime;
    [SerializeField] float spawnTimeMultiplier = 1;
    float SpawnTimeMultiplier
    {
        get => spawnTimeMultiplier;
        set => spawnTimeMultiplier = value;
    }

    [SerializeField] float randomSpawnFloatTimeMultiplier = 1;
    float RandomSpawnFloatTimeMultiplier
    {
        get => randomSpawnFloatTimeMultiplier;
        set => randomSpawnFloatTimeMultiplier = value;
    }

    [SerializeField] float salesMultiplier = 1;
    float SalesMultiplier
    {
        get => salesMultiplier;
        set => salesMultiplier = value;
    }

    [SerializeField] float moldTimeMultiplier = 1;
    float MoldTimeMultiplier
    {
        get => moldTimeMultiplier;
        set => moldTimeMultiplier = value;
    }
}
