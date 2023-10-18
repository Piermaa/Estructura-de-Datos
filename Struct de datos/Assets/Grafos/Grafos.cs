using UnityEngine;
    using System;

    public interface ConjuntoTDA

    {

        void InicializarConjunto();

        bool ConjuntoVacio();

        void Agregar(int x);

        int Elegir();

        void Sacar(int x);

        bool Pertenece(int x);

    }
 
    [Serializable]
    public class Nodo

    {

        public int info;

        public Nodo sig;

    }
 
    // IMPLEMENTACIÓN DINÁMICA //

    public class ConjuntoLD : ConjuntoTDA

    {

        Nodo c;

        public void InicializarConjunto()

        {

            c = null;

        }

        public bool ConjuntoVacio()

        {

            return (c == null);

        }

        public void Agregar(int x)

        {

            /* Verifica que x no este en el conjunto */

            if (!this.Pertenece(x))

            {

                Nodo aux = new Nodo();

                aux.info = x;

                aux.sig = c;

                c = aux;

            }

        }

        public int Elegir()

        {

            return c.info;

        }

        public void Sacar(int x)

        {

            if (c != null)

            {

                // si es el primer elemento de la lista

                if (c.info == x)

                {

                    c = c.sig;

                }

                else

                {

                    Nodo aux = c;

                    while (aux.sig != null && aux.sig.info != x)

                        aux = aux.sig;

                    if (aux.sig != null)

                        aux.sig = aux.sig.sig;

                }

            }

        }

        public bool Pertenece(int x)

        {

            Nodo aux = c;

            while ((aux != null) && (aux.info != x))

            {

                aux = aux.sig;

            }

            return (aux != null);

        }

    }

    // IMPLEMENTACIÓN ESTÁTICA //

public class ConjuntoTA : ConjuntoTDA

{

    int[] a;

    int cant;

    public void Agregar(int x)

    {

        if (!this.Pertenece(x))

        {

            a[cant] = x;

            cant++;

        }

    }

    public bool ConjuntoVacio()

    {

        return cant == 0;

    }

    public int Elegir()

    {

        return a[cant - 1];

    }

    public void InicializarConjunto()

    {

        a = new int[100];

        cant = 0;

    }

    public bool Pertenece(int x)

    {

        int i = 0;

        while (i < cant && a[i] != x)

        {

            i++;

        }

        return (i < cant);

    }

    public void Sacar(int x)

    {

        int i = 0;

        while (i < cant && a[i] != x)

        {

            i++;

        }

        if (i < cant)

        {

            a[i] = a[cant - 1];

            cant--;

        }
    }
}
  public interface GrafoTDA
    {
        void InicializarGrafo();
        void AgregarVertice(int v);
        void EliminarVertice(int v);
        ConjuntoTDA Vertices();
        void AgregarArista(int v1, int v2, int peso);
        void EliminarArista(int v1, int v2);
        bool ExisteArista(int v1, int v2);
        int PesoArista(int v1, int v2);
    }

public class GrafoMA : GrafoTDA
{
    static int n = 12;
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

    public ConjuntoTDA Vertices()
    {
        ConjuntoTDA Vert = new ConjuntoLD();
        Vert.InicializarConjunto();
        for (int i = 0; i < cantNodos; i++)
        {
            Vert.Agregar(Etiqs[i]);
        }

        return Vert;
    }

    public void AgregarArista(int v1, int v2, int peso)
    {
        int o = Vert2Indice(v1-1);
        int d = Vert2Indice(v2-1);
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
}
 
