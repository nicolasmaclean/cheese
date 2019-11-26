using UnityEngine;

public class UnitQueen : Unit
{
    public override void instantiateUnit(Vector2 gPos, bool mouse)
    {
        maxHealth = 75;
        moveRange = 3;
        damage = 20;
        base.instantiateUnit(gPos, mouse);
    }

    public void makeColony()
    {
        
    }
}