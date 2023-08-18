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

        for (int i = 0; i < buttons.Length; i++)
        {
            for (int j = 0; j < Rows; j++)
            {
                for (int k = 0; k < Columns; k++)
                {
                    _texts[j, k] = buttons[i].GetComponentInChildren<TextMeshProUGUI>();
                    _texts[j, k].text = "";
                }
            }
        }
    }

    public void Check()
    {
        string last=String.Empty;
        for (int i = 0; i < Rows; i++)
        {
            print($"I: {i}");
            
            for (int j = 0; j < Columns; j++)
            {
                print($"J: {j}");
                
                print(_texts[i, j].text);
                if (j > 0)
                {
                    // last = _texts[i, j - 1].text;
                    // if ( != last)
                    // {
                    //     print("Es distinto");
                    //     break;
                    // }
                }
              
            }
        
        }
    }
}