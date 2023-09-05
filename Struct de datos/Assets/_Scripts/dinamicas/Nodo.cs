using System;
using UnityEngine;

public class Nodo
    {
        // datos a almacenar 
        // en este caso un entero
        public int datos;
        
        // referencia al siguiente Nodo
        public Nodo siguiente;
    }

    // Clase que imprime todos los nodos de la lista
public class ListaEnlazada
{
    protected Nodo raiz;
    
    /*
     * public TuClase this[int p_id]
{
    get => laListaAAcceder[p_id];
    set => laListaAAcceder[p_id] = value;
}
     */
    
    public void InsertarComienzo(int nuevodato)
    {
        // creo un nuevo nodo
        Nodo nuevo = new Nodo();
        // le agrego el dato al nodo creado
        nuevo.datos = nuevodato;

        // como el nuevo nodo va a ser ahora el primero (la raiz)
        // hago que este nuevo nodo tenga como siguiente a la actual raiz
        nuevo.siguiente = raiz;

        // ahora hago que la referencia a la raiz apunte al nodo nuevo
        raiz = nuevo;
    }
    
    public void InsertarFinal(int nuevodato)
    {
        if (raiz == null)
        {
            // si no hay ningun nodo, le asigno los datos a raiz directamente
            raiz = new Nodo();
            raiz.datos = nuevodato;
            raiz.siguiente = null;
        }
        else
        {
            // creo un nuevo nodo con el dato ingresado
            Nodo nuevo = new Nodo();
            nuevo.datos = nuevodato;

            // uso nodo auxiliar para recorrer la lista
            // para empezar le asigno como referencia el incio (raiz)
            Nodo auxiliar = raiz;
            // con este bucle voy pasando de nodo en nodo
            while (auxiliar.siguiente != null)
            {
                // cuando el nodo siguiente es "null" ya llegué al final
                auxiliar = auxiliar.siguiente;
            }

            // cuando salí del recorrido anterior el nodo auxiliar está 
            // posicionado al final, ahi le agrego como siguiente el nuevo nodo
            auxiliar.siguiente = nuevo;
        }
    }

    public int DevolverUltimo()
    {
        Nodo auxiliar = raiz;
        while (auxiliar.siguiente != null)
        {
            // cuando el nodo siguiente es "null" ya llegué al final
            auxiliar = auxiliar.siguiente;
            //repeticiones++;
        }
        return auxiliar.datos;
    }

 

    public void imprimeTodosLosNodos()
    {
        Nodo actual = raiz;
        while (actual != null)
        {
            actual = actual.siguiente;
        }
    }
}

public class QuequeEnlazada: ListaEnlazada, 
{
    public int DevolverPrimerDato()
    {
        var data = raiz.datos;
        raiz = raiz.siguiente;
        return data;
    }
    
    public void Add(int value)
    {
       InsertarComienzo(value);
    }
}

