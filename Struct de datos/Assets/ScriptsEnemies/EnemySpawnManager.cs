using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawnManager : MonoBehaviour
{
    public int CurrentSpawnedAmount
    {
        get => _currentSpawnedAmount;
        set => _currentSpawnedAmount = value;
    }
    
    [SerializeField] private GameObject[] enemies;
    private ScriptsEnemies.DynamicQueue.Queue<GameObject> _enemyQueue = new ScriptsEnemies.DynamicQueue.Queue<GameObject>();
    [SerializeField] private int enemyAmount;

    [SerializeField] private Transform[] spawns;
    [SerializeField] private int maxSpawnedAmount;
    private int _currentSpawnedAmount;
    void Start()
    {
        _enemyQueue.InitializeQueue();

        for (int i = 0; i < enemyAmount; i++) _enemyQueue.Enqueue(GenerateEnemy(GenerateNumber(enemies.Length)));
    }

    void Update()
    {
        if (_currentSpawnedAmount < maxSpawnedAmount)
        {
            Instantiate(_enemyQueue.Dequeue(), spawns[GenerateNumber(spawns.Length-1)].position, Quaternion.identity);
            _currentSpawnedAmount++;
        }
    }

    int GenerateNumber(int max)
    {
        int randomInt = Random.Range(0, max);
        return randomInt;
    }
    
    GameObject GenerateEnemy(int x)
    {
        return enemies[x];
    }
}
