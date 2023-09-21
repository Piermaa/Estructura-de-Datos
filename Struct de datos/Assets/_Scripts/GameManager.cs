using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] private string _winScene;
    [SerializeField] private string _gameOverScene;
    
    private void Awake()
    {
        if (Instance==null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        
        ActionsManager.ResetManager(); // <===== si le meten un dont destroy on load al gamemanager esto se hace chacon
        
        ActionsManager.RegisterAction(ActionKeys.PLAYER_DEATH_KEY);    
        ActionsManager.SubscribeToAction(ActionKeys.PLAYER_DEATH_KEY, GameOver);
    }
    
    public void Win()
    {
        SceneManager.LoadScene(_winScene);
    }

    public void GameOver()
    {
        SceneManager.LoadScene(_gameOverScene);
    }
}
