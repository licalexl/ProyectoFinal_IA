using System.Collections.Generic;
using UnityEngine;

public class Node
{
    public Vector3 position;// posici�n del nodo en el mundo
    public List<Edge> edges = new List<Edge>(); // lista de conexiones (edges) del nodo

    public Node(Vector3 pos)
    {
        //  inicializa el nodo con una posici�n
        position = pos;
    }
}

public class Edge
{
    public Node from;// nodo de origen
    public Node to; // nodo de destino
    public float cost;


    //  creamos la conexi�n entre dos nodos y su costo
    public Edge(Node from, Node to, float cost)
    {
        this.from = from;
        this.to = to;
        this.cost = cost;
    }
}