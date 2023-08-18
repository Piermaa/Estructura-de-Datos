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

    private const int Three=3;

    private TextMeshProUGUI[,] _texts = new TextMeshProUGUI[Three,Three];

    private bool _playerATurn=false;
    private void Awake()
    {
        _playerATurn = Random.Range(0, 1) ==1 ? false : true;
        
        var buttons = GetComponentsInChildren<Button>();

        for (int i = 0; i < buttons.Length; i++)
        {
            for (int j = 0; j < Three; j++)
            {
                for (int k = 0; k < Three; k++)
                {
                    _texts[j,k]= buttons[i].GetComponentInChildren<TextMeshProUGUI>();
                    _texts[j, k].text = "";
                }
            }
        }
    }

    public void Check()
    {
        for (int i = 0; i < Three; i++)
        {
            for (int j = 0; j < Three; j++)
            {
                string last = _texts[i, j].text;
                if (j>0)
                {
                
                }
                
            }
        }
    }
    
}
