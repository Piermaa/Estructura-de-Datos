using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class Jugador : IElementoConPrioridad
{
    public int Priority => _score;
    public string PlayerName => _playerName;

    private int _score;
    private string _playerName;
    
    public void SetNameAndScore(int newScore, string newPlayerName)
    {
        _score = newScore;
        _playerName = newPlayerName;
    }

}
