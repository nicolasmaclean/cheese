using UnityEngine;

public class Unit : Entity
{
    public static bool[,] unitPositions;
    public int moveRange = 0;
    public int maxHealth = 1;
    public int health;
    public int damage = 1;
    public bool veteran;

    public override void initialize(Vector2 gPos)
    {
        unitPositions[(int)gPos.y, (int)gPos.x] = true;
        health = maxHealth;
        veteran = false;

        base.initialize(gPos);
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

    public override void clicked(System.Collections.Generic.List<GameObject> clickHistory)
    {
        if(clickHistory.Count == 0 || clickHistory.IndexOf(gameObject) != clickHistory.Count-1){
            if(clickHistory.Count > 0 && clickHistory[clickHistory.Count-1].GetComponent<Unit>() != null) // resets move tiles when switching selection between units
                clickHistory[clickHistory.Count-1].GetComponent<Unit>().moveTilesReset();

            clickState = ClickSystem.ClickState.click;

            clickHistory.Add(gameObject);

        } else if(clickHistory.Count > 0 && clickHistory.IndexOf(gameObject) == clickHistory.Count-1) {
            clickState = ClickSystem.ClickState.none;

            if(gameObject.GetComponent<Unit>() != null)
                moveTilesReset();

        }

        updated = false;

        ClickSystem.checkUnitAttack();
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
            unitPositions[(int)gridPosition.y, (int)gridPosition.x] = false;
            unitPositions[(int)nPos.y, (int)nPos.x] = true;
            gridPosition = nPos;
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

    public virtual void promoteToVeteran()
    {
        veteran = true;
        maxHealth = (int) (maxHealth * 1.5);
        damage = (int) (damage * 1.5);
        health = maxHealth;
    }

    public bool takeDamage(int damage)
    {
        health -= damage;
        return checkDeath();;
    }

    public int heal(int healAmt)
    {
        health += healAmt;
        return health;
    }

    public bool checkDeath()
    {
        if(health <= 0 ){
            gameObject.transform.parent.GetComponent<Player>().removeUnit(gameObject);
            ClickSystem.clickHistory.RemoveAll(x => x == gameObject);
            unitPositions[(int)gridPosition.y, (int)gridPosition.x] = false;
            return true;
        }
        return false;
    }
}