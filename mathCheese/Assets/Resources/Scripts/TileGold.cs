public class TileGold : Tile
{
    public override void initialize(UnityEngine.Vector2 gPos)
    {
        entityName = "Gold Tile";

        base.initialize(gPos);
        gold = 5;
    }
}
