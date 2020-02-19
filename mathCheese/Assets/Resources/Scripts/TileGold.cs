public class TileGold : Tile
{
    public override void initialize(UnityEngine.Vector2 gPos)
    {
        entityName = "Grass Tile";

        base.initialize(gPos);
        gold = 5;
    }
}
