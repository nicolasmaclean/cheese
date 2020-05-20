using UnityEngine;
public class TileColony : Tile
{
    public int queens = 1;
    public int level = 2;
    public int maxHealth = 100;
    public int health = 100;

    public override void initialize(UnityEngine.Vector2 gPos)
    {
        entityName = "Colony Tile";

        base.initialize(gPos);
    }

    public override void clicked(System.Collections.Generic.List<GameObject> clickHistory) 
    {
        base.clicked(clickHistory);
        ClickSystem.checkUnitAttack();
        if(this.transform.parent == TurnSystem.players[TurnSystem.currentPlayer])
            UIGameManager.openMenu("UnitCreation");
    }

    public bool takeDamage(int damage)
    {
        health -= damage;
        return checkDeath();
    }


    public void delete(bool block)
    {
        foreach(Transform p in TurnSystem.players)
        {
            if(p.GetComponent<Player>().colonies.Contains(this))
                p.GetComponent<Player>().colonies.Remove(this);
        }
        //TileMapGenerator.tiles = TileMapGenerator.createTile(gridPosition.x, gridPosition.y, up, TileMapGenerator.tile);
        base.delete();
    }



    public bool checkDeath()
    {
        if(health <= 0 ){
            delete(false);
            return true;
        }
        return false;
    }
}