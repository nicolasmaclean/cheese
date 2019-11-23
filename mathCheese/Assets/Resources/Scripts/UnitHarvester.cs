using UnityEngine;

public class UnitHarvester : Unit
{
    public override void instantiateUnit(Vector2 gPos, bool mouse)
    {
        base.instantiateUnit(gPos, mouse);
        moveRange = 1;
    }

    public void harvestTile()
    {
        
    }
}