using System.Collections.Generic;
using UnityEngine;

public class PlayerTracer : MonoBehaviour
{
    public static Node lastNode;
    private Node currentNode;

    void Update()
    {
        Node newNode = Graph.GetClosestNode(transform.position);
        if (newNode != currentNode)
        {
            currentNode = newNode;
            lastNode = newNode;
        }
    }
}