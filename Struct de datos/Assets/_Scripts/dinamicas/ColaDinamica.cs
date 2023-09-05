using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColaDinamica : MonoBehaviour
{
    private QuequeEnlazada _listaEnlazada = new();


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            int rand = UnityEngine.Random.Range(-1, 10);
            print("num added: " + rand);

            _listaEnlazada.Add(rand);
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            print(_listaEnlazada.DevolverUltimo());
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            print(_listaEnlazada.DevolverPrimerDato());
        }
    }

   
}

