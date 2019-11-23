using UnityEngine;

public class UnitQueen : Unit
{
    public override void instantiateUnit(Vector2 gPos, bool mouse)
    {
        base.instantiateUnit(gPos, mouse);
        moveRange = 3;
    }

    public void makeColony()
    {
        
    }
}