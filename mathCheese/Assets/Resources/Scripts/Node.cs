using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    public int x;
    public int y;

    public Vector3 Position;
    public Node parent;

    public double gCost;
    public double hCost;
    public double fCost;


    public Node(int xPos, int yPos) 
    {
        x = xPos;
        y = yPos;
    }
}
