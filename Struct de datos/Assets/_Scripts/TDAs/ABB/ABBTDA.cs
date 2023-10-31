 public interface ABBTDA
 {
     int Raiz();
     NodoABB HijoIzq();
     NodoABB HijoDer();
     bool ArbolVacio();
     void InicializarArbol();
     void AgregarElem(NodoABB n, int x);
     void EliminarElem(NodoABB n, int x);
 }