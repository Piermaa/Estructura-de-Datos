using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//NO PROBE NADA DE ESTO Y POR AHI NO LO USAMOS UN SALUDO
public interface IElementoConPrioridad
{
    int Priority { get; }
}
//Que conio esta pasando aca
//Como despues se requiere verificar el valor int de Priority pero tiene que ser generica la cola le meto ese constraint
//El new es para poder instanciar un T 
public class ColaPrioridadTF<T> : MonoBehaviour, IColaTDA<T> where T:IElementoConPrioridad, new()
{
    private IElementoConPrioridad[] elementos;
    private int[] prioridades; // arreglo en donde se guarda la informacion
    private int indice; // variable entera en donde se guarda la cantidad de elementos que se tienen guardados

    public int Count => throw new System.NotImplementedException();

    public void InicializarCola()
    {
        elementos = new IElementoConPrioridad[100];
        prioridades = new int[100];
        indice = 0;
    }

    public void Acolar(T e) 
    {
        if (e is IElementoConPrioridad x)
        {
            int j;
            int puntaje = e.Priority;
        
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
            T e = (T)elementos[indice - 1];
            return e;
        }
        return default;
    }

    public int Prioridad()
    {
        return prioridades[indice - 1];
    }

    public void Clear()
    {
        throw new System.NotImplementedException();
    }
}
