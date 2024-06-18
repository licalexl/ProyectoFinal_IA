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
                dist[node] = Mathf.Infinity; // inicializar todas las distancias a infinito (excepto el nodo de inicio)
            unvisited.Add(node);
        }

        while (unvisited.Count > 0)
        {
            unvisited.Sort((n1, n2) => dist[n1].CompareTo(dist[n2]));
            Node current = unvisited[0];
            unvisited.Remove(current);

            if (current == targetNode) // si llegamos al nodo destino, salimos del bucle
           
            break;

            foreach (Edge edge in current.edges)
            {
                float alt = dist[current] + edge.cost;
                if (alt < dist[edge.to])
                {
                    dist[edge.to] = alt; // actualizar la distancia mínima conocida
                    prev[edge.to] = current; // actualizar el nodo previo
                }
            }
        }

        List<Node> path = new List<Node>();
        Node temp = targetNode;

        while (temp != null)
        {
            path.Add(temp); // construir el camino trazando desde el nodo destino hasta el nodo de inicio
            temp = prev[temp];
        }

        path.Reverse();// invertir el camino para obtener la secuencia correcta del nodo de inicio al nodo destino
        return path;
    }
}