using UnityEngine;
public class TileColony : Tile
{
    public int queens = 1;
    public int level = 2;
    public int maxHealth = 100;
    public int health = 100;

    public Player owner;

    public override void initialize(UnityEngine.Vector2 gPos)
    {
        entityName = "Colony Tile";

        base.initialize(gPos);
    }

    public void setOwner() {
        foreach(Transform p in TurnSystem.players)
        {
            if(p.GetComponent<Player>().colonies.Contains(this))
                owner = p.GetComponent<Player>();
        }
    }
    public override void clicked(System.Collections.Generic.List<GameObject> clickHistory) 
    {
        base.clicked(clickHistory);
        ClickSystem.checkUnitAttack();
        if(owner.Equals(TurnSystem.players[TurnSystem.currentPlayer].GetComponent<Player>()))
            UIGameManager.openMenu("UnitCreation");
    }

    public bool takeDamage(int damage)
    {
        health -= damage;
        return checkDeath();
    }


    public void delete(bool block)
    {
        owner.colonies.Remove(this);
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