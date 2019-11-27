using UnityEngine;

public class Unit : Entity
{
    public int moveRange = 0;
    public int maxHealth = 1;
    public int health;
    public int damage = 1;
    public static bool[,] unitPositions;

    public override void initialize(Vector2 gPos)
    {
        unitPositions[(int)gPos.y, (int)gPos.x] = true;
        health = maxHealth;

        base.initialize(gPos);
    }

    public Collider getCollider()
    {
        return gameObject.GetComponent<Collider>();
    }

    public override void Update() {
        // checkDeath();
        base.Update();
    }

    public override void clickClickState()
    {
        base.clickClickState();
        if(gameObject.transform.parent == TurnSystem.players[TurnSystem.currentPlayer]){ // player check
            for(int z = -moveRange; z <= moveRange; z++){
                for(int x = -moveRange; x <= moveRange; x++){
                    if((int)gridPosition.y + z > -1 && (int)gridPosition.y + z < TileMapGenerator.tiles.GetLength(0) && (int)gridPosition.x + x > -1 && (int)gridPosition.x + x < TileMapGenerator.tiles.GetLength(1)){
                        TileMapGenerator.tiles[(int)gridPosition.y + z, (int)gridPosition.x + x].GetComponent<Tile>().clickState = ClickSystem.ClickState.none;
                        TileMapGenerator.tiles[(int)gridPosition.y + z, (int)gridPosition.x + x].GetComponent<Tile>().isInMoveRange = true;
                        TileMapGenerator.tiles[(int)gridPosition.y + z, (int)gridPosition.x + x].GetComponent<Tile>().updated = false;
                    }
                }
            }
        }
    }

    public static bool isTileFilled(Vector2 gPos)
    {
        if(unitPositions[(int)gPos.y, (int)gPos.x])
            return true;
        return false;
    }

    public void move(Vector2 nPos)
    {
        moveTilesReset();
        if(!unitPositions[(int)nPos.y, (int)nPos.x] && Mathf.Abs(nPos.x - gridPosition.x) <= moveRange && Mathf.Abs(nPos.y - gridPosition.y) <= moveRange){
            unitPositions[(int)gridPosition.y, (int)gridPosition.x] = false; // maybe add a message about move range if not in range
            gridPosition = nPos;
            unitPositions[(int)nPos.y, (int)nPos.x] = true;
            gameObject.transform.position = new Vector3(gridPosition.x * TileMapGenerator.tileSize, 0, gridPosition.y * TileMapGenerator.tileSize);
        }
    }

    public void moveTilesReset()
    {
        for(int z = -moveRange; z <= moveRange; z++){
            for(int x = -moveRange; x <= moveRange; x++){
                if((int)gridPosition.y + z > -1 && (int)gridPosition.y + z < TileMapGenerator.tiles.GetLength(0) && (int)gridPosition.x + x > -1 && (int)gridPosition.x + x < TileMapGenerator.tiles.GetLength(1)) {
                    TileMapGenerator.tiles[(int)gridPosition.y + z, (int)gridPosition.x + x].GetComponent<Tile>().clickState = ClickSystem.ClickState.none;
                    TileMapGenerator.tiles[(int)gridPosition.y + z, (int)gridPosition.x + x].GetComponent<Tile>().isInMoveRange = false;
                    TileMapGenerator.tiles[(int)gridPosition.y + z, (int)gridPosition.x + x].GetComponent<Tile>().updated = false;
                }
            }
        }
    }

    public int takeDamage(int damage)
    {
        health -= damage;
        return health;
    }

    public int heal(int healAmt)
    {
        health += healAmt;
        return health;
    }

    public void checkDeath()
    {
        if(health <= 0 ){
            gameObject.transform.parent.GetComponent<Player>().removeUnit(gameObject.transform);
            Destroy(gameObject);
        }
    }
}