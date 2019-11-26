using UnityEngine;

public class UnitGod : Unit
{
    public override void instantiateUnit(Vector2 gPos)
    {
        maxHealth = 10000;
        moveRange = 100;
        damage = 500;
        base.instantiateUnit(gPos);
    }
}