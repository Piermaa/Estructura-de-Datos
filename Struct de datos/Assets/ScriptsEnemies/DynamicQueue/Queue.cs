using System;
using UnityEngine;
using UnityEngine.Timeline;

namespace ScriptsEnemies.DynamicQueue
{
    public class Queue<T>
    {
        public int Size => size;
        private Node<T> front;
        private Node<T> rear;
        private int size;

        public void InitializeQueue()
        {
            front = null;
            rear = null;
            size = 0;
        }

        public bool IsEmpty()
        {
            return size == 0;
        }

        public void Enqueue(T data)
        {
            Node<T> newNode = new Node<T>(data);
            
            if (IsEmpty()) front = rear = newNode;
            else
            {
                rear.Next = newNode;
                rear = newNode;
            }

            size++;
        }

        public T Dequeue()
        {
            if (IsEmpty()) throw new InvalidOperationException("Queue is empty");

            T data = front.Data;
            front = front.Next;
            size--;
            return data;
        }

        public T Peek()
        {
            if(IsEmpty()) throw new InvalidOperationException("Queue is empty");
            return front.Data;
        }
    }
}
