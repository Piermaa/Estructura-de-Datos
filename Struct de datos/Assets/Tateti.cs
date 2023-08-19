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
        string lastI=String.Empty;
        for (int i = 0; i < Rows; i++)
        {
            string lastJ=String.Empty;
            
            for (int j = 0; j < Columns; j++)
            {
               // print(_texts[i, j].text);
                if (j > 0)
                {
                    lastJ = _texts[i, j - 1].text;
                    if (_texts[i, j].text != lastJ)
                    {
                    //    print("Es distinto");
                        break;
                    }
                    else if( _texts[i, j].text!="" && j==2)
                    {
                        print($"Player {_texts[i, j].text} Won");
                        break;
                    }
                }
                
                if (i > 0)
                {
                    lastI = _texts[i-1, j].text;
                    if (_texts[i, j].text != lastI)
                    {
                        print("Es distinto");
                        break;
                    }
                    else if( _texts[i, j].text!="" && i==2)
                    {
                        print($"Player {_texts[i, j].text} Won");
                        break;
                    }
                }
            }
        }
    }
}