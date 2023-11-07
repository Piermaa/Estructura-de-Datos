using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class AristaLab
{
    public NodoLab destino;
    public int peso=1;
}

public class NodoLab : Nodo
{
    public AristaLab[] _aristas;

    private void OnDrawGizmos()
    {
        Gizmos.color=Color.red;
        foreach (var arista in _aristas)
        {
            if (arista.destino != null)
            {
                Gizmos.DrawLine(transform.position, arista.destino.transform.position);
            }
        }
    }
}
