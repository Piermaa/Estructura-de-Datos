using System;
using System.Collections.Generic;

public static class ABBOrders
{
    public static int altura(NodoABB ab)
    {
        if (ab == null)
        {
            return -1;
        }
        else
        {
            return (1 + Math.Max(altura(ab.hijoIzq), altura(ab.hijoDer)));
        }
    }

    public static void preOrder_FE(NodoABB a)
    {
        if (a != null)
        {
            // accion mientras recorro //
            a.Process();
            Console.WriteLine("Nodo Padre: " + a.info.ToString());

            a.hijoDer.Process();
            Console.WriteLine("Altura Izquierda: " + altura(a.hijoDer));
            
            a.hijoIzq.Process();
            Console.WriteLine("Altura Derecha: " + altura(a.hijoIzq));
            
            Console.WriteLine();
            //                         //

            preOrder_FE(a.hijoIzq);
            preOrder_FE(a.hijoDer);
        }
    }

    public static void preOrder(NodoABB a)
    {
        if (a != null)
        {
            a.Process();
            Console.WriteLine(a.info.ToString());
            preOrder(a.hijoIzq);
            preOrder(a.hijoDer);
        }
    }

    public static void inOrder(NodoABB a)
    {
        if (a != null)
        {
            inOrder(a.hijoIzq);
            a.Process();
            Console.WriteLine(a.info.ToString());
            inOrder(a.hijoDer);
        }
    }

    public static void postOrder(NodoABB a)
    {
        if (a != null)
        {
            postOrder(a.hijoIzq);
            postOrder(a.hijoDer);
            a.Process();
            Console.WriteLine(a.info.ToString());
        }
    }

    public static void level_Order(NodoABB nodo)
    {
        Queue<NodoABB> q = new Queue<NodoABB>();

        q.Enqueue(nodo);

        while (q.Count > 0)
        {
            nodo = q.Dequeue();
            Console.WriteLine(nodo.info.ToString());
            nodo.Process();
            
            if (nodo.hijoIzq != null)
            {
                q.Enqueue(nodo.hijoIzq);
            }

            if (nodo.hijoDer != null)
            {
                q.Enqueue(nodo.hijoDer);
            }
        }
    }

    public static void levelOrder(NodoABB nodo)
    {
        Queue<NodoABB> q = new Queue<NodoABB>();

        q.Enqueue(nodo);

        while (q.Count > 0)
        {
            nodo = q.Dequeue();

            Console.WriteLine("Padre: " + nodo.info.ToString());
            nodo.Process();
            if (nodo.hijoIzq != null)
            {
                q.Enqueue(nodo.hijoIzq);            
                nodo.hijoIzq.Process();
                Console.WriteLine("Hijo Izq: " + nodo.hijoIzq.info.ToString());
            }
            else
            {
                Console.WriteLine("Hijo Izq: null");
            }

            if (nodo.hijoDer != null)
            {
                q.Enqueue(nodo.hijoDer);
                nodo.hijoDer.Process();
                Console.WriteLine("Hijo Der: " + nodo.hijoDer.info.ToString());
            }
            else
            {
                Console.WriteLine("Hijo Der: null");
            }
        }
    }
}

