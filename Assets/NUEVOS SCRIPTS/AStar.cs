using System.Collections.Generic;
using UnityEngine;

public class AStar
{
    private static float Heuristic(Node a, Node b)
    {
        return Vector3.Distance(a.position, b.position);
    }

    public static List<Node> FindPath(Node startNode, Node targetNode)
    {
        Dictionary<Node, float> gScore = new Dictionary<Node, float>();
        Dictionary<Node, float> fScore = new Dictionary<Node, float>();
        Dictionary<Node, Node> cameFrom = new Dictionary<Node, Node>();
        List<Node> openSet = new List<Node>();

        gScore[startNode] = 0;
        fScore[startNode] = Heuristic(startNode, targetNode);
        openSet.Add(startNode);

        while (openSet.Count > 0)
        {
            openSet.Sort((n1, n2) => fScore[n1].CompareTo(fScore[n2]));
            Node current = openSet[0];

            if (current == targetNode)
                return ReconstructPath(cameFrom, current);

            openSet.Remove(current);

            foreach (Edge edge in current.edges)
            {
                Node neighbor = edge.to;
                float tentativeGScore = gScore[current] + edge.cost;

                if (!gScore.ContainsKey(neighbor) || tentativeGScore < gScore[neighbor])
                {
                    cameFrom[neighbor] = current;
                    gScore[neighbor] = tentativeGScore;
                    fScore[neighbor] = gScore[neighbor] + Heuristic(neighbor, targetNode);

                    if (!openSet.Contains(neighbor))
                        openSet.Add(neighbor);
                }
            }
        }
        return null;
    }

    private static List<Node> ReconstructPath(Dictionary<Node, Node> cameFrom, Node current)
    {
        List<Node> totalPath = new List<Node> { current };
        while (cameFrom.ContainsKey(current))
        {
            current = cameFrom[current];
            totalPath.Add(current);
        }
       
        totalPath.Reverse();
        return totalPath;
    }
}