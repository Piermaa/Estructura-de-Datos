using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PilaTF<T> : IPilaTDA<T>
{
    T[] _array; 
    int _index; 

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
    
    public T Pop()
    {
        _index--;
        return _array[(_index)];
    }
    public bool isEmpty()
    {
        return _index < 0;
    }

    public T Peek()
    {
        return _array[_index - 1];
    }
}


