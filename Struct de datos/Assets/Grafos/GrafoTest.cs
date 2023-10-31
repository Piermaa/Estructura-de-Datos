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
    [SerializeField] private List<NodoGrafo> _nodosConectadosNodoGrafos = new();

    private void Awake()
    {
        grafo = new();
        grafo.InicializarGrafo();
        
        for (int i = 1; i < 13; i++)
        {
            grafo.AgregarVertice(i);
        }

        foreach (var arista in aristas)
        {
            grafo.AgregarArista(arista.origen,arista.destino, arista.peso);
        }
        
           print($"peso total {grafo.PesoCamino(_nodosConectados)}");
      //  print($"peso total {grafo.PesoCamino(_nodosConectadosNodoGrafos)}");
    }
}
