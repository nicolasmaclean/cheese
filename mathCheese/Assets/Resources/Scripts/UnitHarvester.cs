using UnityEngine;

public class UnitHarvester : Unit
{
    public override void instantiateUnit(Vector2 gPos, bool mouse)
    {
        maxHealth = 100;
        moveRange = 1;
        damage = 40;
        base.instantiateUnit(gPos, mouse);
    }

    public void harvestTile()
    {
        
    }
}