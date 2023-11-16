using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Nodo
{
    public int info;
    public Nodo sig;
}

public class GrafoMA 
{
    static int n = 100;
    int[,] MAdy;
    int[] Etiqs;
    int cantNodos;

    public void InicializarGrafo()
    {
        MAdy = new int[n, n];
        Etiqs = new int[n];
        cantNodos = 0;
    }

    public void AgregarVertice(int v)
    {
        Etiqs[cantNodos] = v;
        for (int i = 0; i <= cantNodos; i++)
        {
            MAdy[cantNodos, i] = 0;
            MAdy[i, cantNodos] = 0;
        }

        cantNodos++;
    }

    public void EliminarVertice(int v)
    {
        int ind = Vert2Indice(v);

        for (int k = 0; k < cantNodos; k++)
        {
            MAdy[k, ind] = MAdy[k, cantNodos - 1];
        }

        for (int k = 0; k < cantNodos; k++)
        {
            MAdy[ind, k] = MAdy[cantNodos - 1, k];
        }

        Etiqs[ind] = Etiqs[cantNodos - 1];
        cantNodos--;
    }

    private int Vert2Indice(int v)
    {
        int i = cantNodos - 1;
        while (i >= 0 && Etiqs[i] != v)
        {
            i--;
        }

        return i;
    }

    public IConjuntoTDA Vertices()
    {
        IConjuntoTDA Vert = new ConjuntoLD();
        Vert.InicializarConjunto();
        for (int i = 0; i < cantNodos; i++)
        {
            Vert.Agregar(Etiqs[i]);
        }

        return Vert;
    }

    public void AgregarArista(int v1, int v2, int peso)
    {
        int o = Vert2Indice(v1);
        int d = Vert2Indice(v2);
        MAdy[o, d] = peso;
    }

    public void EliminarArista(int v1, int v2)
    {
        int o = Vert2Indice(v1);
        int d = Vert2Indice(v2);
        MAdy[o, d] = 0;
    }

    public bool ExisteArista(int v1, int v2)
    {
        int o = Vert2Indice(v1);
        int d = Vert2Indice(v2);
        return MAdy[o, d] != 0;
    }

    public int PesoArista(int v1, int v2)
    {
        int o = Vert2Indice(v1);
        int d = Vert2Indice(v2);
        return MAdy[o, d];
    }

    public int PesoCamino(List<Nodo> nodos)
    {
        int pesoTotal=0;
        for (int i = 0; i < nodos.Count-1; i++)
        {
            if (ExisteArista(nodos[i].info,nodos[i+1].info))
            {
                pesoTotal += PesoArista(nodos[i].info, nodos[i+1].info);
            }
            else
            {
                Debug.LogWarning($"No existe arista entre: {nodos[i].info} y {nodos[i+1].info}");
                return pesoTotal;
            }
        }

        return pesoTotal;
    }
}