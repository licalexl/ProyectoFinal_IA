using System.Collections.Generic;
using UnityEngine;

public class NodeComponent : MonoBehaviour
{
    public List<NodeComponent> connectedNodes = new List<NodeComponent>();
    public float connectionCost = 1f;

    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(transform.position, 0.1f);

        Gizmos.color = Color.red;
        foreach (var node in connectedNodes)
        {
            if (node != null)
            {
                Gizmos.DrawLine(transform.position, node.transform.position);
            }
        }
    }
}