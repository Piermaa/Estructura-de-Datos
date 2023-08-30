using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IColaTDA<T>
{
    void InicializarCola();
    // siempre que la cola este inicializada
    void Acolar(T x);
    // siempre que la cola este inicializada y no este vacıa
    void Desacolar();
    // siempre que la cola este inicializada
    bool ColaVacia();
    // siempre que la cola este inicializada y no este vacıa
    T Primero();
}
