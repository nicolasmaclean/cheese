using UnityEngine;

public class UnitHarvester : Unit
{
    public override void initialize(Vector2 gPos)
    {
        maxHealth = 100;
        moveRange = 1;
        damage = 40;
        entityName = "Harvester Ant";
        
        base.initialize(gPos);
    }

    public float[] harvest()
    {
        float[] resources = new float[3];

        Tile temp = TileMapGenerator.tiles[(int)gridPosition.y, (int)gridPosition.x];
        if(temp) {
            resources[0] = temp.food;
            resources[1] = temp.water;
            resources[2] = temp.gold;
        }
        
        return resources;
    }
}