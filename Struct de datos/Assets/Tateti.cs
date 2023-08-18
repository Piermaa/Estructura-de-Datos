using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Tateti : MonoBehaviour
{
    public bool PlayerATurn
    {
        get => playerATurn;
        set => playerATurn = value;
    }

    private Button[,] _cells = new Button[3,3];

    private bool playerATurn=false;
    private void Awake()
    {
        playerATurn = Random.Range(0, 1) ==1 ? false : true;
    }

    public void Check()
    {
        
    }

}
