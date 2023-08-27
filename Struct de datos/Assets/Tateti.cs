using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Tateti : MonoBehaviour
{
    public bool playerATurn
    {
        get => _playerATurn;
        set => _playerATurn = value;
    }

    private const int Columns = 3;
    private const int Rows = 3;

    private TextMeshProUGUI[,] _texts = new TextMeshProUGUI[Rows, Columns];

    private bool _playerATurn = false;
    
    private void Awake()
    {
        _playerATurn = Random.Range(0, 1) == 1 ? false : true;

        var buttons = GetComponentsInChildren<Button>();

   
        for (int j = 0; j < Rows; j++)
        {
            for (int k = 0; k < Columns; k++)
            {
                _texts[j, k] = buttons[j*Rows+k].GetComponentInChildren<TextMeshProUGUI>();
                _texts[j, k].text = $"{j},{k}";
            }
        }

    }

    public void Check()
    {
        print("check");
        /*
        for (int i = 0; i < Rows; i++)
        { 
            for (int j = 0; j < Columns; j++)
            {
                if (_texts[i, 0].text == _texts[i, Mathf.Clamp(j + 1, 0, Columns - 1)].text 
                    && _texts[i, 0].text == _texts[i, Mathf.Clamp(j + 2, 0, Columns - 1)].text)
                {
                    print("horizontal win");
                    //terminar el juego
                }
            }
        }
        */

        //board esta al reves
        for (int i = 0; i < Rows; i++)
        {
            for (int j = 0; j < Columns; j++)
            {
                int clampedNext = Mathf.Clamp(i + 1, 0, Columns - 1);

                //Esquina Derecha
                if (_texts[i, 0].text == _texts[clampedNext, clampedNext].text
                    && _texts[i, 0].text == _texts[clampedNext+1, clampedNext+1].text)
                {
                    print("diag right win");
                    //EndGame(); terminar el juego
                }
            }
        }
    }
}