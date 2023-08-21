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
        
        
        string[] currentTexts = new string[Rows * Columns];
        for (int j = 0; j < Rows; j++)
        {
            for (int k = 0; k < Columns; k++)
            {
                currentTexts[j*Rows+k] = _texts[j, k].text;
            }
        }

        // 0 1 2
        // 3 4 5
        // 6 7 8
        
        string last = string.Empty;
        
        for (int i = 0; i < currentTexts.Length; i+=Rows+1)
        {
            last = currentTexts[0];
            if (i>0)
            {
                if (currentTexts[i]!=last)
                {
                    break;
                }
                else if(i==currentTexts.Length-1)
                {
                    print("Won " + last);
                } 
            }
        }

        for (int i = Rows-1; i < currentTexts.Length; i+=Rows-1)
        {
            last = currentTexts[Rows-1];
            if (i>Rows-1)
            {
                if (currentTexts[i]!=last)
                {
                    print("es distinto");
                    break;
                }
                else if(i==currentTexts.Length-Rows)
                {
                    print("Won " + last);
                } 
            }
        }
    }
}