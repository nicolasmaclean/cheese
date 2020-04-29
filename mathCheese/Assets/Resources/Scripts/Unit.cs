using UnityEngine;

public class Unit : Entity
{
    public static bool[,] unitPositions;
    public int moveRange = 0;
    public int maxHealth = 1;
    public int health = 1;
    public int damage = 1;
    public int level = 1;
    public double levelMult = 1.1;
    public int moves = 1;
    public int teamMaterialIndex;
    public bool canMove = true;

    static Material borderDefaultMaterial;
    static Material borderNoStamMaterial;

    public override void initialize(Vector2 gPos)
    {
        if(borderDefaultMaterial == null) {
            borderDefaultMaterial = transform.Find("Border").GetComponent<Renderer>().material;
        }
        if(borderNoStamMaterial == null) {
            borderNoStamMaterial = Resources.Load("Materials/Player 1") as Material;
        }

        unitPositions[(int)gPos.y, (int)gPos.x] = true;
        health = maxHealth;

        base.initialize(gPos);
    }

    public virtual void initialize(Vector2 gPos, Material teamMaterial)
    {
        if(teamMaterialIndex > 0 && teamMaterialIndex < GetComponent<Renderer>().materials.GetLength(0) && teamMaterial != null) {
            Renderer renderer = GetComponent<Renderer>();
            Material[] mats = renderer.materials; 
            mats[teamMaterialIndex] = teamMaterial; 
            renderer.materials = mats;
        } else {
            Debug.Log("something wrong with team material assignment");
        }
        initialize(gPos);
    }

    public override void clickClickState()
    {
        base.clickClickState();
        if(gameObject.transform.parent == TurnSystem.players[TurnSystem.currentPlayer] && canMove){ // player check
            for(int z = -moveRange; z <= moveRange; z++){
                for(int x = -moveRange; x <= moveRange; x++){
                    if((int)gridPosition.y + z > -1 && (int)gridPosition.y + z < TileMapGenerator.tiles.GetLength(0) && (int)gridPosition.x + x > -1 && (int)gridPosition.x + x < TileMapGenerator.tiles.GetLength(1)){
                        TileMapGenerator.tiles[(int)gridPosition.y + z, (int)gridPosition.x + x].clickState = ClickSystem.ClickState.none;
                        TileMapGenerator.tiles[(int)gridPosition.y + z, (int)gridPosition.x + x].isInMoveRange = true;
                        TileMapGenerator.tiles[(int)gridPosition.y + z, (int)gridPosition.x + x].updated = false;
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

        } else if(clickHistory.Count > 0 && clickHistory.IndexOf(gameObject) == clickHistory.Count-1) { // double click to deselect
            clickState = ClickSystem.ClickState.none;

            if(gameObject.GetComponent<Unit>() != null)
                moveTilesReset();

        }

        updated = false;

        if(canMove)
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
            // moves--;
            decreaseMoves(1);
        }
    }

    public void moveTilesReset()
    {
        for(int z = -moveRange; z <= moveRange; z++){
            for(int x = -moveRange; x <= moveRange; x++){
                if((int)gridPosition.y + z > -1 && (int)gridPosition.y + z < TileMapGenerator.tiles.GetLength(0) && (int)gridPosition.x + x > -1 && (int)gridPosition.x + x < TileMapGenerator.tiles.GetLength(1)) {
                    TileMapGenerator.tiles[(int)gridPosition.y + z, (int)gridPosition.x + x].clickState = ClickSystem.ClickState.none;
                    TileMapGenerator.tiles[(int)gridPosition.y + z, (int)gridPosition.x + x].isInMoveRange = false;
                    TileMapGenerator.tiles[(int)gridPosition.y + z, (int)gridPosition.x + x].updated = false;
                }
            }
        }
    }

    public virtual void levelUp()
    {
        level++;
        maxHealth = (int) (maxHealth * levelMult);
        damage = (int) (damage * levelMult);
        health = maxHealth;

        if(level == 3)
            promoteToVeteran();
    }

    public virtual void promoteToVeteran()
    {
        Debug.Log("veteran has no changes");
    }

    public bool takeDamage(int damage)
    {
        health -= damage;
        return checkDeath();
    }

    public int heal(int healAmt)
    {
        health += healAmt;
        return health;
    }

    public void delete(bool block)
    {
        gameObject.transform.parent.GetComponent<Player>().removeUnit(gameObject);

        if(!block)
            unitPositions[(int)gridPosition.y, (int)gridPosition.x] = false;

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

    public void moveToRandomAdjTile()
    {
        int y = (int) gridPosition.y;
        int x = (int) gridPosition.x;
        System.Collections.Generic.List<Tile> adjTiles = TileMapGenerator.tiles[y, x].getAdjacentTiles();
        bool moved = false;

        while(!moved && adjTiles.Count > 0) {
            int r = Random.Range(0, adjTiles.Count);

            if(!Unit.unitPositions[(int)adjTiles[r].gridPosition.y, (int)adjTiles[r].gridPosition.x]) {
                moved = true;
                move(adjTiles[r].gridPosition);
            } else {
                adjTiles.RemoveAt(r);
            }
        }
    }

    public void decreaseMoves(int i)
    {
        moves -= i;
        if(moves <= 0) {
            canMove = false;
            updateCanMove();
        }
    }

    public void resetMoves()
    {
        moves = 1;
        if(!canMove) {
            canMove = true;
            updateCanMove();
        }
    }

    public void updateCanMove()
    {
        Renderer borderRenderer = transform.Find("Border").GetComponent<Renderer>();
        if(canMove) {
            borderRenderer.material = borderDefaultMaterial;
        } else {
            borderRenderer.material = borderNoStamMaterial;
        }
    }
}