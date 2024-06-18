using System.Collections.Generic;
using UnityEngine;

public class EnemyAStar : MonoBehaviour
{
    private List<Node> path;
    private int currentPathIndex;
    private Node lastKnownNode;

    public float speed = 1.0f;

    void Update()
    {
        if (PlayerTracer.lastNode != lastKnownNode)
        {
            lastKnownNode = PlayerTracer.lastNode;
            Node startNode = Graph.GetClosestNode(transform.position);
            path = AStar.FindPath(startNode, lastKnownNode);
            currentPathIndex = 0;
        }

        FollowPath();
    }

    void FollowPath()
    {
        if (path != null && path.Count > 0)
        {
            Vector3 target = path[currentPathIndex].position;
            transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);

            if (Vector3.Distance(transform.position, target) < 0.1f)
            {
                currentPathIndex++;
                if (currentPathIndex >= path.Count)
                {
                    currentPathIndex = 0;
                }
            }
        }
    }
}