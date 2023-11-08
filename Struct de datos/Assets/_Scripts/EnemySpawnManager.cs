using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawnManager : MonoBehaviour
{
    
    [SerializeField] private Enemy[] enemies;
    [SerializeField] private Enemy[] _enemyArray;
    private ColaDinamicaTF<Enemy> _enemyQueue = new ColaDinamicaTF<Enemy>();
    [SerializeField] private int enemyAmount;

    [SerializeField] private Transform[] spawns;
    [SerializeField] private int maxSpawnedAmount;
    private int _currentSpawnedAmount;
    
    void Start()
    {
        ActionsManager.RegisterAction(ActionKeys.ENEMY_DEATH_KEY);
        ActionsManager.SubscribeToAction(ActionKeys.ENEMY_DEATH_KEY,EnemyDied);
        _enemyQueue.InicializarCola();
        _enemyArray = new Enemy[enemyAmount];
        
        for (int i = 0; i < enemyAmount; i++) _enemyArray[i] = GenerateEnemy(GenerateNumber(enemies.Length));
        QuickSort<Enemy>.QuickSortMethod(_enemyArray, 0, _enemyArray.Length -1);
        
        for (int i = 0; i < _enemyArray.Length; i++)
        {
            _enemyQueue.Acolar(_enemyArray[i]);
        }
        
        InvokeRepeating("SpawnEnemies", 1, 1);
    }

    int GenerateNumber(int max)
    {
        int randomInt = Random.Range(0, max);
        return randomInt;
    }
    
    Enemy GenerateEnemy(int x)
    {
        return enemies[x];
    }

    public void SpawnEnemies()
    {
        if (_currentSpawnedAmount < maxSpawnedAmount && !_enemyQueue.ColaVacia())
        {
//            print("spawneo enemigo");
            Instantiate(_enemyQueue.Primero(), spawns[GenerateNumber(spawns.Length-1)].position, Quaternion.identity);
            _enemyQueue.Desacolar();
            _currentSpawnedAmount++;
        }
    }
    public void EnemyDied() 
    {
    //    Debug.Log("Enemy died");
        _currentSpawnedAmount--;

        if (_currentSpawnedAmount<=0 && _enemyQueue.ColaVacia())
        {
            GameManager.Instance.Win();
        }
    }
}
