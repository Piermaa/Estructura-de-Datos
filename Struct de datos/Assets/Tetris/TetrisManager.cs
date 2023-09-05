using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class TetrisManager : MonoBehaviour
{
    [SerializeField] private GameObject[] pieces;
    [SerializeField] private Transform panel;
    private Queue<GameObject> _piecesQueue = new Queue<GameObject>();
    private int _shapesAmount = 5;
    public void DequeueAndEnqueue()
    {
        if (QueueIsEmpty())
        {
            DeQueuePiece();
            EnqueuePiece();
        }
    }

    private void DeQueuePiece()
    {
        if (QueueIsEmpty())
        {
            var obj = _piecesQueue.Dequeue();
            print(obj.name);
            Destroy(obj);
        }
    }

    private void EnqueuePiece()
    {
        GameObject obj = Instantiate( pieces[Random.Range(0, pieces.Length)], this.panel);
        _piecesQueue.Enqueue(obj);
    }

    public void GenerateQueue()
    {
        for (int i = 0; i < _shapesAmount; i++)
        {
            EnqueuePiece();
        }
    }

    private bool QueueIsEmpty()
    {
        return _piecesQueue.Count > 0;
    }
}
