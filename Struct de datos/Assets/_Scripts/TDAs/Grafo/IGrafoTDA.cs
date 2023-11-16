using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGrafoTDA
{
    void InicializarGrafo();
    void AgregarVertice(int v, Node node);
    void EliminarVertice(int v);
    IConjuntoTDA Vertices();
    void AgregarArista(int v1, int v2, int peso);
    void EliminarArista(int v1, int v2);
    bool ExisteArista(int v1, int v2);
    int PesoArista(int v1, int v2);
}
