public class TileGrass : Tile
{
    public override void initialize(UnityEngine.Vector2 gPos)
    {
        entityName = "Grass Tile";

        base.initialize(gPos);
        food = 2;
    }
}