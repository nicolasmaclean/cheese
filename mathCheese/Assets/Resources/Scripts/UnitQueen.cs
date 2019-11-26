using UnityEngine;

public class UnitQueen : Unit
{
    public override void instantiateUnit(Vector2 gPos)
    {
        maxHealth = 75;
        moveRange = 3;
        damage = 20;
        base.instantiateUnit(gPos);
    }

    public void makeColony()
    {
        
    }
}