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
           
        AlgDijkstra.Dijkstra(grafo,1);
       // MuestroResultadosAlg(AlgDijkstra.distance, grafo.cantNodos, grafo.Etiqs, AlgDijkstra.nodos);
        
        print(AlgDijkstra.EncontrarCaminoEntre(grafo,1,10));
      //  print($"peso total {grafo.PesoCamino(_nodosConectadosNodoGrafos)}");
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
