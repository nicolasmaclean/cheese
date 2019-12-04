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

    public void harvestTile()
    {
        
    }
}