using UnityEngine;

public class UnitGod : Unit
{
    public override void instantiateUnit(Vector2 gPos, bool mouse)
    {
        base.instantiateUnit(gPos, mouse);
        moveRange = 69;
    }
}