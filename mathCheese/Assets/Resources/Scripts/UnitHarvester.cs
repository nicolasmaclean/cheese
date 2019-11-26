using UnityEngine;

public class UnitHarvester : Unit
{
    public override void instantiateUnit(Vector2 gPos)
    {
        maxHealth = 100;
        moveRange = 1;
        damage = 40;
        base.instantiateUnit(gPos);
    }

    public void harvestTile()
    {
        
    }
}