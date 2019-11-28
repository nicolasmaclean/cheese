using UnityEngine;

public class UnitSoldier : Unit
{
    public override void initialize(Vector2 gPos)
    {
        maxHealth = 150;
        moveRange = 2;
        damage = 50;
        
        base.initialize(gPos);
    }
}