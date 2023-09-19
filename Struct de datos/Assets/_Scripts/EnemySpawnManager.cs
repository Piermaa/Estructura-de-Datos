using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawnManager : MonoBehaviour
{
    
    [SerializeField] private GameObject[] enemies;
    private ColaDinamicaTF<GameObject> _enemyQueue = new ColaDinamicaTF<GameObject>();
    [SerializeField] private int enemyAmount;

    [SerializeField] private Transform[] spawns;
    [SerializeField] private int maxSpawnedAmount;
    private int _currentSpawnedAmount;
    void Start()
    {
        _enemyQueue.InicializarCola();

        for (int i = 0; i < enemyAmount; i++) _enemyQueue.Acolar(GenerateEnemy(GenerateNumber(enemies.Length)));
        //Usar Quicksort con los enemigos según su dificultad
        SpawnEnemies();
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

    public void SpawnEnemies()
    {
        for (int i = _currentSpawnedAmount; i <= maxSpawnedAmount; i++)
        {
            print("spawneo enemigo");
            Instantiate(_enemyQueue.Primero(), spawns[GenerateNumber(spawns.Length-1)].position, Quaternion.identity);
            _enemyQueue.Desacolar();
            _currentSpawnedAmount++;
        }
    }
    public void EnemyDied() //Esto tendría que dispachearse como evento cuando muera un enemy
    {
        _currentSpawnedAmount--;
        if (!_enemyQueue.ColaVacia())
        {
            SpawnEnemies();
        }
    }
}
