using System;
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

        try
        {
            if (IsEmpty())
                throw new Exception("Stack Empty");
            else
            {
                return _array[_index];
            }
        }
        catch
        {
            Debug.Log("Stack Empty. Que se yo try catch. No se me ocurre una mejor manera a las 6 AM");
            return default;
        }

    }
    public bool IsEmpty()
    {
        return _index < 0;
    }
    public T Peek()
    {
        return _array[_index - 1];
    }
    public void ResetIndex()
    {
        _index = 0;
    }
}


