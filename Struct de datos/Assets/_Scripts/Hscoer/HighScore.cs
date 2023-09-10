using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = System.Random;

public class HighScore : MonoBehaviour
{
   [SerializeField] private int _playersCount;
   private readonly ColaPrioridadTF<Jugador> _players = new();
   [SerializeField] private Transform scoreTextsParent;
   [SerializeField] private TextMeshProUGUI[] scoreTexts;
   private void Awake()
   {
      scoreTexts = new TextMeshProUGUI[_playersCount];
      
      for (int i = 0; i < _playersCount; i++)
      {
         scoreTexts[i] = scoreTextsParent.GetChild(i).GetComponent<TextMeshProUGUI>();
      }
   }

   public void GenerarHighScore()
   {
      _players.InicializarCola();

      for (int i = 0; i < _playersCount; i++)
      {
         var j = new Jugador();
         j.SetNameAndScore(UnityEngine.Random.Range(0,10000),$"Jugador{i+1}");
         _players.Acolar(j);
      }

      for (int i = 0; i < _playersCount; i++)
      {
         scoreTexts[i].gameObject.SetActive(true);
         scoreTexts[i].text = $"{i+1} - {_players.Primero().PlayerName} {_players.Primero().Priority.ToString()}pts";
         _players.Desacolar();
      }
   }
}
