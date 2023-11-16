using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeMap : MonoBehaviour
{
    public GraphAM LevelMapGraph => nodeGraph;

    //----PRIVATE VARS---------
    private List<Node> nodeMap = new List<Node>();
    private GraphAM nodeGraph;

    [SerializeField] private bool turnOffDebugToolsOnPlay = false;

    //################ #################
    //----------UNITY EV FUNC-----------
    //################ #################
    private void Start()
    {
        LoadNodeMap();
        UseNodeMapToInitNodeGraph(nodeMap.Count);

        if (turnOffDebugToolsOnPlay == true)
        {
            TurnOffDebugTools();
        }
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
    private void UseNodeMapToInitNodeGraph(int totalNodes)
    {
        nodeGraph = new GraphAM();
        nodeGraph.InicializarGrafo();

        for (int i = 0; i < totalNodes; i++)
        {
            nodeGraph.AgregarVertice(i, nodeMap[i]);
        }

        foreach (Node node in nodeMap)
        {
            foreach (Node.NodeConnection nodeConnection in node.nodeConnectionData)
            {
                nodeGraph.AgregarArista(nodeConnection.origin, nodeConnection.destination, nodeConnection.cost);
            }
        }
    }

    [ContextMenu("/RefreshAllNodeInfo")]
    private void RefreshAllNodesInfo()
    {
        LoadNodeMap();
        foreach (Node node in nodeMap)
        {
            node.RefreshNodeData();
        }
    }

    private void TurnOffDebugTools()
    {
        foreach (Node node in nodeMap)
        {
            node.transform.GetChild(0).gameObject.SetActive(false);
        }
    }
}
