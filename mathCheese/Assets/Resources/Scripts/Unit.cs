using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public Vector2 gridPosition;
    public int moveRange = 0;
    public int maxHealth = 1;
    public int health;
    public int damage = 1;
    public ClickSystem.ClickState clickState;
    public bool updated = false;
    public static bool[,] unitPositions;

    Material unitMeshMaterial;
    GameObject borderT;

    public static bool isTileFilled(Vector2 gPos)
    {
        if(unitPositions[(int)gPos.y, (int)gPos.x])
            return true;
        return false;
    }

    public virtual void instantiateUnit(Vector2 gPos)
    {
        unitPositions[(int)gPos.y, (int)gPos.x] = true;
        gridPosition = gPos;
        health = maxHealth;
        borderT = gameObject.transform.Find("Border").gameObject;
        unitMeshMaterial = gameObject.GetComponent<Renderer>().sharedMaterial;

        borderT.GetComponent<Renderer>().enabled = false;
    }

    public Collider getCollider()
    {
        return gameObject.GetComponent<Collider>();
    }

    void Update() {
        if(borderT != null && !updated){
            if(clickState == ClickSystem.ClickState.none)
                noClickState();
            else if(clickState == ClickSystem.ClickState.hover)
                hoverClickState();
            else if(clickState == ClickSystem.ClickState.click)
                clickClickState();
        }
    }
    
    void hoverClickState()
    {
        borderT.GetComponent<Renderer>().enabled = true;
        updated = true;
    }

    void noClickState()
    {
        // go.GetComponent<Renderer>().sharedMaterial = unitMeshMaterial;
        borderT.GetComponent<Renderer>().enabled = false;
    }

    void clickClickState()
    {
        // go.GetComponent<Renderer>().material.color = Color.red;
        borderT.GetComponent<Renderer>().enabled = true;
        for(int z = -moveRange; z <= moveRange; z++){
            for(int x = -moveRange; x <= moveRange; x++){
                if((int)gridPosition.y + z > -1 && (int)gridPosition.y + z < TileMapGenerator.tiles.GetLength(0) && (int)gridPosition.x + x > -1 && (int)gridPosition.x + x < TileMapGenerator.tiles.GetLength(1)){
                    TileMapGenerator.tiles[(int)gridPosition.y + z, (int)gridPosition.x + x].GetComponent<Tile>().clickState = ClickSystem.ClickState.none;
                    TileMapGenerator.tiles[(int)gridPosition.y + z, (int)gridPosition.x + x].GetComponent<Tile>().isInMoveRange = true;
                    TileMapGenerator.tiles[(int)gridPosition.y + z, (int)gridPosition.x + x].GetComponent<Tile>().updated = false;
                }
            }
        }
        updated = true;
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

    //deals damage and return new health
    public int takeDamage(int damage)
    {
        health -= damage;
        return health;
    }

    //increases health and return new health
    public int heal(int healAmt)
    {
        health += healAmt;
        return health;
    }
}