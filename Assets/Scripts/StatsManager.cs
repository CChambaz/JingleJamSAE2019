using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsManager : MonoBehaviour
{
    //const float spawnTime;
    //const float randomSpawnFloadTime;
    [SerializeField] float spawnTimeMultiplier = 1;
    public float SpawnTimeMultiplier
    {
        get => spawnTimeMultiplier;
        set => spawnTimeMultiplier = value;
    }

    [SerializeField] float randomSpawnFloatTimeMultiplier = 1;
    public float RandomSpawnFloatTimeMultiplier
    {
        get => randomSpawnFloatTimeMultiplier;
        set => randomSpawnFloatTimeMultiplier = value;
    }

    [SerializeField] float salesMultiplier = 1;
    public float SalesMultiplier
    {
        get => salesMultiplier;
        set => salesMultiplier = value;
    }

    [SerializeField] float moldTimeMultiplier = 1;
    public float MoldTimeMultiplier
    {
        get => moldTimeMultiplier;
        set => moldTimeMultiplier = value;
    }
}
