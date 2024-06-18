using System.Collections.Generic;
using UnityEngine;

public class Dijkstra
{
    public static List<Node> FindPath(Node startNode, Node targetNode)
    {
        Dictionary<Node, float> dist = new Dictionary<Node, float>();  // distancia mínima conocida desde el nodo de inicio a cada nodo
        Dictionary<Node, Node> prev = new Dictionary<Node, Node>();
        List<Node> unvisited = new List<Node>();

        dist[startNode] = 0;
        prev[startNode] = null;

        foreach (var node in Graph.nodes)
        {
            if (node != startNode)
                dist[node] = Mathf.Infinity;
            unvisited.Add(node);
        }

        while (unvisited.Count > 0)
        {
            unvisited.Sort((n1, n2) => dist[n1].CompareTo(dist[n2]));
            Node current = unvisited[0];
            unvisited.Remove(current);

            if (current == targetNode)
                break;

            foreach (Edge edge in current.edges)
            {
                float alt = dist[current] + edge.cost;
                if (alt < dist[edge.to])
                {
                    dist[edge.to] = alt;
                    prev[edge.to] = current;
                }
            }
        }

        List<Node> path = new List<Node>();
        Node temp = targetNode;

        while (temp != null)
        {
            path.Add(temp);
            temp = prev[temp];
        }

        path.Reverse();
        return path;
    }
}