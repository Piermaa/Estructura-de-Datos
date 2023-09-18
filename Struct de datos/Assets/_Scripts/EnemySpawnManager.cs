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
    private ColaDinamicaTF<GameObject> _enemyQueue = new ColaDinamicaTF<GameObject>();
    // private ScriptsEnemies.DynamicQueue.Queue<GameObject> _enemyQueue = new ScriptsEnemies.DynamicQueue.Queue<GameObject>();
    [SerializeField] private int enemyAmount;

    [SerializeField] private Transform[] spawns;
    [SerializeField] private int maxSpawnedAmount;
    private int _currentSpawnedAmount;
    void Start()
    {
        _enemyQueue.InicializarCola();

        for (int i = 0; i < enemyAmount; i++) _enemyQueue.Acolar(GenerateEnemy(GenerateNumber(enemies.Length)));
        //Usar Quicksort con los enemigos según su dificultad
    }

    void Update()
    {
        if (_currentSpawnedAmount < maxSpawnedAmount)
        {
            print("Genero enemigo");
            Instantiate(_enemyQueue.Primero(), spawns[GenerateNumber(spawns.Length-1)].position, Quaternion.identity);
            _enemyQueue.Desacolar();
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
