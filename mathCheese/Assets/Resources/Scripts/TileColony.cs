public class TileColony : Tile
{
    public int queens = 1;

    public override void initialize(UnityEngine.Vector2 gPos)
    {
        entityName = "Colony Tile";

        base.initialize(gPos);
    }
}