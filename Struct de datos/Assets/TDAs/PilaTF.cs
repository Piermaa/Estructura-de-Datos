using System.Collections;
<<<<<<<< HEAD:Struct de datos/Assets/PilaTF.cs
========
using System.Collections.Generic;
using UnityEngine;
>>>>>>>> main:Struct de datos/Assets/TDAs/PilaTF.cs

public class PilaTF<T> : IPilaTDA<T>
{
    T[] _array; // arreglo en donde se guarda la informacion
    int _index; // variable entera en donde se guarda la cantidad de elementos que se tienen guardados
<<<<<<<< HEAD:Struct de datos/Assets/PilaTF.cs
    
========


>>>>>>>> main:Struct de datos/Assets/TDAs/PilaTF.cs
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
<<<<<<<< HEAD:Struct de datos/Assets/PilaTF.cs
    
    public T Pop()
    {
        _index--;
        return _array[(_index)];
========

    public T Pop()
    {
        _index--;
        return _array[_index];
>>>>>>>> main:Struct de datos/Assets/TDAs/PilaTF.cs
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
        return _array[_index - 1];
    }
}


