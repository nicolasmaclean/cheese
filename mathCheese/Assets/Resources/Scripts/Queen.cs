using UnityEngine;

public class Queen : Unit
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