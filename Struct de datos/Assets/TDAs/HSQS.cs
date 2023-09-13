using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class HSQS : MonoBehaviour
{
    private Jugador[] _players;
    [SerializeField] private int _playersCount;
    [SerializeField] private Transform scoreTextsParent;
    [SerializeField] public static TextMeshProUGUI[] scoreTexts;
    private void Awake()
    {
        _players = new Jugador[_playersCount];
        scoreTexts = new TextMeshProUGUI[_playersCount];
      
        for (int i = 0; i < _playersCount; i++)
        {
            scoreTexts[i] = scoreTextsParent.GetChild(i).GetComponent<TextMeshProUGUI>();
        }
    }
    
    [ContextMenu("GENERAR")]
    public void GenerarHighScore()
    {
        
        QuickSort<Jugador> jugadoresOrdenados = new QuickSort<Jugador>();
        
        for (int i = 0; i < _playersCount; i++)
        {
            var j = new Jugador();
            j.SetNameAndScore(UnityEngine.Random.Range(0,10000),$"Jugador{i+1}");
            _players[i]=j;
        }
        
        jugadoresOrdenados.quickSort(_players, 0, _players.Length - 1);

        for (int i = 0; i < _playersCount; i++)
        {
            scoreTexts[_playersCount-i-1].gameObject.SetActive(true);
            scoreTexts[_playersCount-i-1].text = $"{_playersCount-i-1+1} - {_players[_playersCount-i-1].PlayerName} {_players[_playersCount-i-1].Priority.ToString()}pts";
        }
    }
}
