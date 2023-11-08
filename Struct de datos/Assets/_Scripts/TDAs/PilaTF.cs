using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PilaTF<T> : IPilaTDA<T>
{
    public int Count => _index;
    
    T[] _array; 
    int _index; 

    public void Init(int length)
    {
        _array = new T[length];
        _index = -1;
    }

    public void Add(T x)
    {
        _index++;
        _array[_index] = x;
    }
    
    public T Pop()
    {
       // var aux= 
        _index--;
        return _array[_index];
    }

    public bool IsEmpty()
    {
        return _index < 0;
    }
    public T Peek()
    {
        return _array[_index - 1];
    }
}


