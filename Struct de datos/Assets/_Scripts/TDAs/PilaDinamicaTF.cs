using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PilaDinamicaTF<T> : IPilaTDA<T>
{
    // la estructura siempre guarda la referencia a UN nodo
    // en el caso de la pila es al primero
    Nodo<T> primero;

    public bool PilaVacia()
    {
        return (primero == null);
    }

    public void Init(int length)
    {
        primero = null;
    }

    public void Add(T x)
    {
        // creo un nuevo nodo
        Nodo<T> nuevo = new Nodo<T>();
        // le asigno el dato a apilar
        nuevo.datos = x;
        // asigno la referencia al proximo nodo
        nuevo.siguiente = primero;
        // le asigno al nodo "primero" su nuevo valor
        // en el caso de la pila, el primero siempre es el ultimo que entró
        primero = nuevo;
    }

    public T Pop()
    {
        var aux = primero;
        // Desapilar es quitar el primer valor de la pila
        // alcanza con asignarle al nodo primero el del siguiente
        primero = primero.siguiente;

        return aux.datos;
    }

    public bool IsEmpty()
    {
        return primero == null;
    }

    public T Peek()
    {
        return primero.datos;
    }
}
