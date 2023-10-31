using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ABB : ABBTDA
{
    public NodoABB _raiz;

    public int Raiz()
    {
        return _raiz.info;
    }

    public bool ArbolVacio()
    {
        return (_raiz == null);
    }

    public void InicializarArbol()
    {
        _raiz = null;
    }

    public NodoABB HijoDer()
    {
        return _raiz.hijoDer;
    }

    public NodoABB HijoIzq()
    {
        return _raiz.hijoIzq;
    }

    public void AgregarElem(NodoABB raiz, int x)
    {
        // NO NOS ANDA SI LO AGREGAMOS CON ESTA FORMA!
        // tambien nos tiraba error si usabamos el ref!
        //tpyeof
        if (_raiz == null)
        {
            raiz = new NodoABB();
            raiz.info = x;
        }
        else if (raiz.info > x)
        {
            AgregarElem(raiz.hijoIzq, x);
        }
        else if (raiz.info < x)
        {
            AgregarElem(raiz.hijoDer, x);
        }
    }

    public void EliminarElem( NodoABB raiz, int x)
    {
        if (raiz != null)
        {
            if (raiz.info == x && (raiz.hijoIzq == null) && (raiz.hijoDer == null))
            {
                raiz = null;
            }
            else if (raiz.info == x && raiz.hijoIzq != null)
            {
                raiz.info = this.mayor(raiz.hijoIzq);
                EliminarElem( raiz.hijoIzq, raiz.info);
            }
            else if (raiz.info == x && raiz.hijoIzq == null)
            {
                raiz.info = this.menor(raiz.hijoDer);
                EliminarElem( raiz.hijoDer, raiz.info);
            }
            else if (raiz.info < x)
            {
                EliminarElem( raiz.hijoDer, x);
            }
            else
            {
                EliminarElem( raiz.hijoIzq, x);
            }
        }
    }

    public int mayor(NodoABB a)
    {
        if (a.hijoDer == null)
        {
            return a.info;
        }
        else
        {
            return mayor(a.hijoDer);
        }
    }

    public int menor(NodoABB a)
    {
        if (a.hijoIzq == null)
        {
            return a.info;
        }
        else
        {
            return menor(a.hijoIzq);
        }
    }
}