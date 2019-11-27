using UnityEngine;

public class UnitGod : Unit
{
    public override void initialize(Vector2 gPos)
    {
        maxHealth = 10000;
        moveRange = 100;
        damage = 500;
        
        base.initialize(gPos);
    }
}