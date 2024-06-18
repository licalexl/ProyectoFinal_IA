using System.Collections.Generic;
using UnityEngine;

public class Graph : MonoBehaviour
{
    public static List<Node> nodes = new List<Node>();// Encontrar todos los componentes NodeComponent en la escena

    void Start()
    {
        nodes.Clear();
        NodeComponent[] nodeComponents = FindObjectsOfType<NodeComponent>();

        foreach (var nodeComponent in nodeComponents)
        {
            Node node = new Node(nodeComponent.transform.position);  // Crear un nodo por cada componente
            nodes.Add(node);
        }

        //  edges basados en las conexiones en NodeComponent
        foreach (var nodeComponent in nodeComponents)
        {
            Node node = GetNodeByPosition(nodeComponent.transform.position);
            foreach (var connectedNodeComponent in nodeComponent.connectedNodes)
            {
                Node connectedNode = GetNodeByPosition(connectedNodeComponent.transform.position);
                if (connectedNode != null)
                {
                    float cost = nodeComponent.connectionCost;
                    node.edges.Add(new Edge(node, connectedNode, cost));
                }
            }
        }
    }

    public static Node GetClosestNode(Vector3 position)
    {
        Node closestNode = null;
        float minDistance = Mathf.Infinity;

        foreach (Node node in nodes)
        {
            float dist = Vector3.Distance(position, node.position);
            if (dist < minDistance)
            {
                minDistance = dist;
                closestNode = node;
            }
        }

        return closestNode;
    }

    private static Node GetNodeByPosition(Vector3 position)
    {
        foreach (Node node in nodes)
        {
            if (node.position == position)
            {
                return node;
            }
        }
        return null;
    }
}