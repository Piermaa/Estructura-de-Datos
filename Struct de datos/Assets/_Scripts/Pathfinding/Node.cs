using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Poner el numero del nodo en Node Number = Ej si es el nodo 4 ponerle 4. (Asi xq No se puede acceder a las propiedades del go :(
//En Node Connections crear la cantidad de conexiones que tenga el nodo. Si el nodo 4 esta conectado al 3 y el 5, hacer 2 
//entradas y tirar el objeto del nodo 3 y 5 en el campo de Destination Node. El resto de la info se llena sola a partir de eso.

public class Node : MonoBehaviour, ISerializationCallbackReceiver
{
    //----PUBLIC PROPERTIES--------
    public int nodeNumber = 0;

    [Serializable]
    public struct NodeConnection
    {
        public int origin;
        public int destination;
        public int cost;
        public Node destinationNode;
    }

    public List<NodeConnection> nodeConnectionData = new List<NodeConnection>();

    //################ #################
    //----------UNITY EV FUNC-----------
    //################ #################

    private void Awake()
    {
        RetrieveNodeData();
    }
    public void OnBeforeSerialize()
    {
        RetrieveNodeData();
    }

    public void OnAfterDeserialize()
    {
        RetrieveNodeData();
    }

    //################ #################
    //----------CLASS METHODS-----------
    //################ #################

    private void RetrieveNodeData()
    {
        connectedNodesList.Clear();

        for (int i = 0; i < nodeConnectionData.Count; i++)
        {
            if (nodeConnectionData[i].destinationNode != null)
            {
                connectedNodesList.Add(nodeConnectionData[i].destinationNode);

                NodeConnection connectionData = new NodeConnection();
                connectionData.origin = nodeNumber;
                connectionData.destinationNode = nodeConnectionData[i].destinationNode;
                connectionData.destination = nodeConnectionData[i].destinationNode.nodeNumber;
                //No tiene sentido que sea otra cosa que 1 para laberinto
                connectionData.cost = 1;

                nodeConnectionData[i] = connectionData;
            }
            else print("Destination Node field is empty wacho. Remember to fill in Node Number to this node's number too.");
        }
    }


    //################ #################
    //------------DEBUGGING-----------
    //################ #################

    private List<Node> connectedNodesList = new List<Node>();

    private bool Contains(Node node)
    {
        return connectedNodesList.Contains(node);
    }
    private void OnDrawGizmos()
    {
        if (connectedNodesList.Count > 0)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, 0.5f);
            foreach (var node in connectedNodesList)
            {
                Gizmos.color = Color.blue; 
                if (node.Contains(this))
                {
                    Gizmos.color = Color.green;
                }
                Gizmos.DrawLine(transform.position, node.transform.position);
            }
        }
    }
}
