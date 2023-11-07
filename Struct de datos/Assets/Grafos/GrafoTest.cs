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


    [SerializeField] private List<NodoLab> _nodosLaberinto;
    private void Awake()
    {
        grafo = new();
        grafo.InicializarGrafo();
        
        for (int i = 0; i < _nodosLaberinto.Count; i++)
        {
            _nodosLaberinto[i].info = i;
            grafo.AgregarVertice(i);
        }

        for (int i = 0; i <  _nodosLaberinto.Count; i++)
        {
            foreach (var arista in _nodosLaberinto[i]._aristas)
            {
                grafo.AgregarArista(_nodosLaberinto[i].info,arista.destino.info, 1);
            }
        }
        
        print(AlgDijkstra.EncontrarCaminoEntre(grafo,0,34));
    }
    
    
    public static void MuestroResultadosAlg(int[] distance, int verticesCount, int[] Etiqs, string[] caminos)
    {
        string distancia = "";

        print("Vertice    Distancia desde origen    Nodos");

        for (int i = 0; i < verticesCount; ++i)
        {
            if (distance[i] == int.MaxValue)
            {
                distancia = "---";
            }
            else
            {
                distancia = distance[i].ToString();
            }
            print( Etiqs[i] +"                      "+ distancia +"                                   "+  caminos[i]);
        }
    }
}
