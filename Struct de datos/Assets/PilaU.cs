using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PilaU : MonoBehaviour
{
    private PilaTF<int> pila=new PilaTF<int>();
    private void Awake()
    {
        pila.Add(3);
        pila.Add(4);
        pila.Add(5);
        pila.Add(1);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            
            pila.Add(Random.Range(int.MinValue,int.MaxValue));
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            print(pila.Peek());
            pila.Pop();
        }
    }
}