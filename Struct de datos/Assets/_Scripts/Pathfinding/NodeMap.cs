using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeMap : MonoBehaviour
{
    //----PRIVATE VARS---------
    private List<Node> nodeMap = new List<Node>();
    private GraphAM nodeGraph;

    //################ #################
    //----------UNITY EV FUNC-----------
    //################ #################
    private void Start()
    {
        nodeGraph = new GraphAM();
        nodeGraph.InicializarGrafo();

        LoadNodeMap();
        int totalNodes = nodeMap.Count;

        for (int i = 0; i < totalNodes; i++)
        {
            nodeGraph.AgregarVertice(i);
        }

        foreach (Node node in nodeMap)
        {
            foreach (Node.NodeConnection nodeConnection in node.nodeConnectionData)
            {
                nodeGraph.AgregarArista(nodeConnection.origin, nodeConnection.destination, nodeConnection.cost);
            }
        }

        print($"peso total para llegar de nodo 0 a nodo {totalNodes-1} es de {nodeGraph.PesoCamino(nodeMap)}");
    }
    //################ #################
    //----------CLASS METHODS-----------
    //################ #################
    private void LoadNodeMap()
    {
        foreach (Node node in GetComponentsInChildren<Node>())
        {
            nodeMap.Add(node);
        }
    }
}
