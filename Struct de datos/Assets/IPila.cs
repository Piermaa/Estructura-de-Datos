
public interface IPilaTDA<T>
{
    void Init(int length);
    // siempre que la pila est´e inicializada
    void Add(T x);
    // siempre que la pila est´e inicializada y no est´e vac´ıa
    void Pop();
    // siempre que la pila est´e inicializada
    bool isEmpty();
    // siempre que la pila est´e inicializada y no est´e vac´ıa
    T Peek();
}

public class PilaTF<T> : IPilaTDA<T>
{
    T[] _array; // arreglo en donde se guarda la informacion
    int _index; // variable entera en donde se guarda la cantidad de elementos que se tienen guardados

    
    public void Init(int length)
    {
        _array = new T[length];
        _index = 0;
    }
    
    public void Add(T x)
    {
        _array[_index] = x;
        _index++;
    }
    public void Pop()
    {
        _index--;
    }

    public bool isEmpty()
    {
        return _index < 0;
    }

    public bool IsEmpty()
    {
        return (_index == 0);
    }

    public T Peek()
    {
        return (T)_array[_index - 1];
    }
}

