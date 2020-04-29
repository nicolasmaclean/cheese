using UnityEngine;
using System.Collections.Generic;

public class UnitHarvester : Unit
{
    public float harvestRange = 2;
    public Tile assignedTile;
    public Stack<Tile> path;

    public override void initialize(Vector2 gPos, Material teamMaterial)
    {
        maxHealth = 100;
        moveRange = 1;
        damage = 40;
        entityName = "Harvester Ant";
        teamMaterialIndex = 2;
        
        base.initialize(gPos, teamMaterial);
    }

    public float[] harvest(List<TileColony> colonies)
    {
        float[] resources = new float[3];

        Tile temp = TileMapGenerator.tiles[(int)gridPosition.y, (int)gridPosition.x];
        if(temp && temp == assignedTile) {
            resources[0] = temp.food;
            resources[1] = temp.water;
            resources[2] = temp.gold;
        }
        float dist = getDistFromNearestColony(colonies);
        for(int x = 0; x < resources.GetLength(0); x++)
            resources[x] = dist <= harvestRange ? resources[x] : Mathf.Round(resources[x] * (level*1f) * (-Mathf.Pow(1.2f, dist-harvestRange)+2));
        
        return resources;
    }

    public float getDistFromNearestColony(List<TileColony> colonies)
    {
        float i = Mathf.Infinity;
        float d;

        foreach(TileColony c in colonies){
            d = (c.gridPosition.y - gridPosition.y)*(c.gridPosition.y - gridPosition.y) + (c.gridPosition.x - gridPosition.x)*(c.gridPosition.x - gridPosition.x);
            if(d < i)
                i = d;
        }

        return (int)Mathf.Sqrt(i);
    }

    public override void clicked(System.Collections.Generic.List<GameObject> clickHistory) 
    {
        base.clicked(clickHistory);
        UIGameManager.openMenu("HarvesterAssignment");
    }

    public void getPath(Tile start, Tile end)
    {
        Node s = new Node((int)start.gridPosition.x, (int)start.gridPosition.y);
        Node e = new Node((int)end.gridPosition.x, (int)end.gridPosition.y);

        PathFinder.findPath(s, e);
        List<Node> nodePath = PathFinder.path;

        Debug.Log(nodePath.Count);

        List<Tile> tilePath = new List<Tile>();

        foreach(Node n in nodePath)
        {
            tilePath.Add(TileMapGenerator.tiles[n.x,n.y]);
        }

        path = new Stack<Tile>();
        foreach(Tile t in tilePath)
        {
            Debug.Log("bruh 2.0");
            path.Push(t);
        }
    }
}