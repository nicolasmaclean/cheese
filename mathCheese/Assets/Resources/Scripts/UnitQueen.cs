using UnityEngine;

public class UnitQueen : Unit
{
    public override void initialize(Vector2 gPos)
    {
        maxHealth = 75;
        moveRange = 3;
        damage = 20;
        entityName = "Queen Ant";
        
        base.initialize(gPos);
    }

    public void makeColony()
    {
        if(TileMapGenerator.createColony((int)gridPosition.y, (int)gridPosition.x, TurnSystem.currentPlayer)) { // makes sure that a colony is made before deleting the queen
            moveTilesReset();
            delete(true);
        }
    }
}