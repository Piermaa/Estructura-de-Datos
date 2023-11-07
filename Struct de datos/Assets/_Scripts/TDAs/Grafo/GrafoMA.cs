using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Nodo : MonoBehaviour
{
    public int info;
    public Nodo sig;
}

public class GrafoMA : IGrafoTDA
{
    public int[,] MAdy
    {
        get
        {
            int[,] ma = _MAdy;
            return ma;
        }
     
    }
    public int cantNodos=> _cantNodos;
    public int[] Etiqs => _etiqs;
    
    static int n = 100;
    int[,] _MAdy;
    int[] _etiqs;
    int _cantNodos;

    public void InicializarGrafo()
    {
        _MAdy = new int[n, n];
        _etiqs = new int[n];
        _cantNodos = 0;
    }

    public void AgregarVertice(int v)
    {
        _etiqs[_cantNodos] = v;
        for (int i = 0; i <= _cantNodos; i++)
        {
            _MAdy[_cantNodos, i] = 0;
            _MAdy[i, _cantNodos] = 0;
        }

        _cantNodos++;
    }

    public void EliminarVertice(int v)
    {
        int ind = Vert2Indice(v);

        for (int k = 0; k < _cantNodos; k++)
        {
            _MAdy[k, ind] = _MAdy[k, _cantNodos - 1];
        }

        for (int k = 0; k < _cantNodos; k++)
        {
            _MAdy[ind, k] = _MAdy[_cantNodos - 1, k];
        }

        _etiqs[ind] = _etiqs[_cantNodos - 1];
        _cantNodos--;
    }

    public int Vert2Indice(int v)
    {
        int i = _cantNodos - 1;
        while (i >= 0 && _etiqs[i] != v)
        {
            i--;
        }

        return i;
    }

    public IConjuntoTDA Vertices()
    {
        IConjuntoTDA Vert = new ConjuntoLD();
        Vert.InicializarConjunto();
        for (int i = 0; i < _cantNodos; i++)
        {
            Vert.Agregar(_etiqs[i]);
        }

        return Vert;
    }

    public void AgregarArista(int v1, int v2, int peso)
    {
        int o = Vert2Indice(v1);
        int d = Vert2Indice(v2);
        _MAdy[o, d] = peso;
    }

    public void EliminarArista(int v1, int v2)
    {
        int o = Vert2Indice(v1);
        int d = Vert2Indice(v2);
        _MAdy[o, d] = 0;
    }

    public bool ExisteArista(int v1, int v2)
    {
        int o = Vert2Indice(v1);
        int d = Vert2Indice(v2);
        return _MAdy[o, d] != 0;
    }

    public int PesoArista(int v1, int v2)
    {
        int o = Vert2Indice(v1);
        int d = Vert2Indice(v2);
        return _MAdy[o, d];
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