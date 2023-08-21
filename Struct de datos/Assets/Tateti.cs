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
        for (int i = 0; i < Rows; i++)
        {
            string lastJ=_texts[i, 0].text;
            int counterJ=0;
            
            for (int j = 1; j < Columns; j++)
            {
                if (_texts[i, j].text == lastJ && (lastJ != "" && _texts[i, j].text != ""))
                {
                    counterJ++;
                }
                else
                {
                    break;
                }
            }

            if (counterJ == 2)
            {
                print($"Won {lastJ}");
                break;
            }
        }
        
        for (int j = 0; j < Columns; j++)
        {
            string lastI = _texts[0, j].text;
            int counterI = 0;

            for (int i = 1; i < Rows; i++)
            {
                if (_texts[i, j].text == lastI && (lastI != "" && _texts[i, j].text != ""))
                {
                    counterI++;
                }
                else
                {
                    break;
                }
            }

            if (counterI==2)
            {
                print($"Won {lastI}");
                break;
            }
            
        }

        // for (int i = 1; i < Rows; i++)
        // {
        //     string last=_texts[0, 0].text;
        //     int counterJ=0;
        //
        //     if (_texts[i, i].text == last && (last != "" && _texts[i, i].text != ""))
        //     {
        //         counterJ++;
        //     }
        //     else
        //     {
        //         break;
        //     }
        //      
        //     if (counterJ == 2)
        //     {
        //         print($"Won {last}");
        //         break;
        //     }
        // }
        
        // 0,0
        // 0,columns
        
        
        //
        // string[] currentTexts = new string[Rows * Columns];
        // for (int j = 0; j < Rows; j++)
        // {
        //     for (int k = 0; k < Columns; k++)
        //     {
        //         currentTexts[j*Rows+k] = _texts[j, k].text;
        //     }
        // }
        //
        // for (int i = 0; i < UPPER; i++)
        // {
        //     
        // }

    }
}