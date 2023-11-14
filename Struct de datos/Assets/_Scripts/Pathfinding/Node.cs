using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

//Asegurarse que el nombre del nodo sea un numero. En NodeMap -> Click Derecho sobre el script -> RefreshAllNodes
//En Node Connections crear la cantidad de conexiones que tenga el nodo. Si el nodo 4 esta conectado al 3 y el 5, hacer 2 
//entradas y tirar el objeto del nodo 3 y 5 en el campo de Destination Node. El resto de la info se llena sola a partir de eso.

public class Node : MonoBehaviour, ISerializationCallbackReceiver
{
    //----PUBLIC PROPERTIES--------
    public int NodeNumber => nodeNumber;

    [Serializable]
    public struct NodeConnection
    {
        public int origin;
        public int destination;
        public int cost;
        public Node destinationNode;
    }

    public List<NodeConnection> nodeConnectionData = new List<NodeConnection>();

    //----PRIVATE VARS--------
    [SerializeField] private int nodeNumber = 0;
    private TextMeshPro nodeDebugScreenName;

    //################ #################
    //----------UNITY EV FUNC-----------
    //################ #################

    private void Awake()
    {
        RefreshNodeData();
    }
    public void OnBeforeSerialize()
    {
        UpdateNodeData();
    }

    public void OnAfterDeserialize()
    {
        UpdateNodeData();
    }

    //################ #################
    //----------CLASS METHODS-----------
    //################ #################

    private void UpdateNodeData()
    {
        connectedNodesList.Clear();

        for (int i = 0; i < nodeConnectionData.Count; i++)
        {
            if (nodeConnectionData[i].destinationNode != null)
            {
                connectedNodesList.Add(nodeConnectionData[i].destinationNode);

                NodeConnection connectionData = new NodeConnection();
                connectionData.origin = NodeNumber;
                connectionData.destinationNode = nodeConnectionData[i].destinationNode;
                connectionData.destination = nodeConnectionData[i].destinationNode.NodeNumber;
                //No tiene sentido que sea otra cosa que 1 para laberinto
                connectionData.cost = 1;

                nodeConnectionData[i] = connectionData;
            }
        }
    }
    private void ParseNameToNodeNumber()
    {
        if (int.TryParse(gameObject.name, out int number))
        {
            nodeNumber = number;
        }
        else Debug.LogWarning("Node name must be a number");

        nodeDebugScreenName = GetComponentInChildren<TextMeshPro>(true);
        nodeDebugScreenName.text = nodeNumber.ToString();
    }

    [ContextMenu("/RefreshNodeInfo")]
    public void RefreshNodeData()
    {
        ParseNameToNodeNumber();
        UpdateNodeData();
    }

    //################ #################
    //------------DEBUGGING-----------
    //################ #################

    private List<Node> connectedNodesList = new List<Node>();

    private void OnDrawGizmos()
    {
        if (connectedNodesList.Count > 0)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, 0.5f);
            foreach (var node in connectedNodesList)
            {
                Gizmos.color = Color.red; 
                if (node.Contains(this))
                {
                    Gizmos.color = Color.green;
                }
                Gizmos.DrawLine(transform.position, node.transform.position);
            }
        }
    }
    private bool Contains(Node node)
    {
        return connectedNodesList.Contains(node);
    }
}
