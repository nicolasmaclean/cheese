using UnityEngine;

public class UnitQueen : Unit
{
    public override void initialize(Vector2 gPos)
    {
        maxHealth = 75;
        moveRange = 3;
        damage = 20;
        
        base.initialize(gPos);
    }

    public void makeColony()
    {
        
    }
}