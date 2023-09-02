public interface IPilaTDA<T>
{
    void Init(int length);
    // siempre que la pila est´e inicializada
    void Add(T x);
    // siempre que la pila est´e inicializada y no est´e vac´ıa
    T Pop();
    // siempre que la pila est´e inicializada
    bool isEmpty();
    // siempre que la pila est´e inicializada y no est´e vac´ıa
    T Peek();
}