public class TileRock : Tile
{
    public override void initialize(UnityEngine.Vector2 gPos)
    {
        entityName = "Rock Tile";

        base.initialize(gPos);
        resources = 2;
    }
}