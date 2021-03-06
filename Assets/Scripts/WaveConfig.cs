﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy wave config")]
public class WaveConfig : ScriptableObject
{
    [Header("Prefabs")]
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] GameObject pathPrefab;

    [Header("Spawn Config")]
    [SerializeField] float timeBetweenSpawns = 0.5f;
    [SerializeField] float spawnRandomFactor = 0.3f;
    [SerializeField] float moveSpeed = 2f;
    [SerializeField] int numberOfEnemies = 6;

    public GameObject GetEnemyPrefab()
    {
        return enemyPrefab;
    }

    public List<Transform> GetWaypoints()
    {
        var waypoints = new List<Transform>();

        foreach (Transform child in pathPrefab.transform)
        {
            waypoints.Add(child);
        }

        return waypoints;
    }

    public float GetTimeBetweenSpawns()
    {
        return timeBetweenSpawns;
    }

    public float GetSpawnRandomFactor()
    {
        return spawnRandomFactor;
    }

    public float GetMoveSpeed()
    {
        return moveSpeed;
    }

    public int GetNumberOfEnemies()
    {
        return numberOfEnemies;
    }
}
