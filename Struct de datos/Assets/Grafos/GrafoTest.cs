using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct Arista
{
    public int origen;
    public int destino;
    public int peso;
}

public class GrafoTest : MonoBehaviour
{
    [SerializeField] private List<Arista> aristas = new();
    
    private GrafoMA grafo;
    private int vertices=12;

    [SerializeField] private List<Nodo> _nodosConectados = new();

    private void Awake()
    {
        grafo = new();
        grafo.InicializarGrafo();
        
        for (int i = 0; i < 12; i++)
        {
            grafo.AgregarVertice(i);
        }

     //   grafo.AgregarArista(1,1,1);
        
        foreach (var arista in aristas)
        {
            print($"{arista.origen},{arista.destino}, {arista.peso}");
            grafo.AgregarArista(arista.origen,arista.destino, arista.peso);
        }

        int pesoTotal=0;
        for (int i = 0; i < _nodosConectados.Count-1; i++)
        {
            pesoTotal += grafo.PesoArista(_nodosConectados[i].info, _nodosConectados[i+1].info);
        }

        print($"peso total {pesoTotal}");
    }
}
