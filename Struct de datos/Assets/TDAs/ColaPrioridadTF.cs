using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IElementoConPrioridad
{
    int Priority { get; }
}

public class ColaPrioridadTF<T> : MonoBehaviour, IColaTDA<T> 
{
    T[] elementos;
    int[] prioridades; // arreglo en donde se guarda la informacion
    int indice; // variable entera en donde se guarda la cantidad de elementos que se tienen guardados

    public void InicializarCola()
    {
        elementos = new T[100];
        prioridades = new int[100];
        indice = 0;
    }

    public void Acolar(T e) 
    {
        if (e is IElementoConPrioridad x)
        {
            int j;
            int puntaje = x.Priority;
        
            // al ingresar cada elemento se 
            for (j = indice; j > 0 && elementos[j - 1].Priority >= puntaje; j--)
            {
                elementos[j] = elementos[j - 1];
                prioridades[j] = prioridades[j - 1];
            }

            elementos[j] = x;
            prioridades[j] = puntaje;

            indice++;
        }
        
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
        if (elementos[indice - 1] is IElementoConPrioridad)
        {
            return elementos[indice - 1];
        }

        return default;
    }

    public int Prioridad()
    {
        return prioridades[indice - 1];
    }
}
