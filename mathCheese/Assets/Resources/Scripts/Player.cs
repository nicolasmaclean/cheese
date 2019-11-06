using System.Collections.Generic;

public class Player
{
    public List<Unit> units;
    public int larvae;

    public Player()
    {
        units = new List<Unit>();
        larvae = 0;
    }

    public void addUnit(Unit u)
    {
        units.Add(u);
    }

    public void EndTurn()
    {

    }
}
