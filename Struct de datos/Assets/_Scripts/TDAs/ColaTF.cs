using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColaTF<T> : IColaTDA<T>
{
    T[] a; // arreglo en donde se guarda la informacion
    int indice; // variable entera en donde se guarda la cantidad de elementos que se tienen guardados

    public void InicializarCola()
    {
        a = new T[100];
        indice = 0;
    }

    public void Acolar(T x)
    {
        for (int i = indice - 1; i >= 0; i--)
        {
            a[i + 1] = a[i];
        }
        a[0] = x;
            
        indice++;
    }
    public void Desacolar()
    {
        indice--;
    }

    public bool ColaVacia()
    {
        return (indice == 0);
    }

    public T Primero()
    {
        return a[indice - 1];
    }
}