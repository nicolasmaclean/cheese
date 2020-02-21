using UnityEngine;
using System.Collections.Generic;

public class UnitHarvester : Unit
{
    public float harvestRange = 2;
    public override void initialize(Vector2 gPos)
    {
        maxHealth = 100;
        moveRange = 1;
        damage = 40;
        entityName = "Harvester Ant";
        
        base.initialize(gPos);
    }

    public float[] harvest(List<TileColony> colonies)
    {
        float[] resources = new float[3];

        Tile temp = TileMapGenerator.tiles[(int)gridPosition.y, (int)gridPosition.x];
        if(temp) {
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
}