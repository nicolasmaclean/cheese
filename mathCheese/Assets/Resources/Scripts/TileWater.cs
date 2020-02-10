public class TileWater : Tile
{
    public override void initialize(UnityEngine.Vector2 gPos)
    {
        entityName = "Water Tile";

        base.initialize(gPos);
        resources = 3;
    }
}