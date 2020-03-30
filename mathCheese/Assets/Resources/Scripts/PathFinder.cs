using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinder : MonoBehaviour
{
    public static List<Node> path;

    public static Node[,] grid;
    public static void findPath(Node start, Node end)
    {
        grid = new Node[TileMapGenerator.mapWidth,TileMapGenerator.mapHeight];
        for(int i = 0; i < 25; i++)
        {
            for(int j = 0; j < 25; j++)
            {
                grid[i,j] = new Node(i,j);
            }
        }
        start = grid[start.x,start.y];
        end = grid[end.y,end.x];
        path = new List<Node>();
        Debug.Log("yeet");
        List<Node> openList = new List<Node>();
        HashSet<Node> closedList = new HashSet<Node>();

        openList.Add(start);

        while(openList.Count > 0)
        {
            Node current = openList[0];
            for(int i = 1; i < openList.Count; i++)
            {
                if(openList[i].fCost < current.fCost || openList[i].fCost == current.fCost && openList[i].hCost < current.hCost)
                {
                    current = openList[i];
                }
            }
            Debug.Log(current.x + " " + current.y);
            openList.Remove(current);
            closedList.Add(current);

            if(current.x == end.x && current.y == end.y)
            {
                path = getFinalPath(start, end);
                break;
            }
            List<Node> adjacent = new List<Node>();

            for(int i = current.x-1; i <= current.x+1; i++)
            {
                for(int j = current.y-1; j <= current.y+1; j++)
                {
                    if(i>0 && j > 0 && i < 25 && j < 25 && (i!=current.x || j!=current.y))
                        adjacent.Add(grid[i,j]);
                }
            }
            foreach(Node n in adjacent)
            {
                if(!Unit.unitPositions[n.x,n.y] && !closedList.Contains(n))
                {
                    double cost = current.gCost + getDistance(current, n);

                    if(cost < n.gCost || !openList.Contains(n))
                    {
                        n.gCost = cost;
                        n.hCost = getDistance(n,end);
                        n.parent = current;
                        if(!openList.Contains(n))
                        {
                            openList.Add(n);
                        }
                    }
                }
            }
        }
    }

    public static List<Node> getFinalPath(Node start, Node end)
    {
        List<Node> final = new List<Node>();
        Node current = end;

        while(current != start)
        {
            final.Add(current);
            current = current.parent;
        }

        return final;
    }

    public static double getDistance(Node start, Node end)
    {
        return Mathf.Sqrt(Mathf.Pow(Mathf.Abs(start.x-end.x),2) + Mathf.Pow(Mathf.Abs(start.y-end.y),2));
    }
}
