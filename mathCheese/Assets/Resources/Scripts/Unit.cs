using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public Vector2 gridPosition;
    public int moveRange = 2;
    public ClickSystem.ClickState clickState;
    public bool updated = false;

    Material unitMeshMaterial;
    Transform borderT;
    private static bool[,] unitPositions = new bool[TileMapGenerator.mapWidth, TileMapGenerator.mapHeight];

    public static bool isTileFilled(Vector2 gPos)
    {
        if(unitPositions[(int)gPos.x, (int)gPos.y])
            return true;
        return false;
    }

    public void instantiateUnit(Vector2 gPos, bool mouse)
    {
        unitPositions[(int)gPos.x, (int)gPos.y] = true;
        gridPosition = gPos;
        borderT = gameObject.transform.Find("Border");
        unitMeshMaterial = gameObject.GetComponent<Renderer>().sharedMaterial;

        borderT.GetComponent<Renderer>().enabled = false;

        if(mouse){
            gameObject.AddComponent<MouseOver>();
            gameObject.GetComponent<MouseOver>().instantiate(MouseOver.GameObjectType.Unit);
        }
    }

    void Update() {
        if(!updated){
            if(clickState == ClickSystem.ClickState.none)
                noClickState();
            else if(clickState == ClickSystem.ClickState.hover)
                hoverClickState();
            else if(clickState == ClickSystem.ClickState.click)
                clickClickState();
            else if(clickState == ClickSystem.ClickState.inMoveRange)
                clickState = ClickSystem.ClickState.none; //units should never be in this state, this state is only for tiles
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
                    TileMapGenerator.tiles[(int)gridPosition.y + z, (int)gridPosition.x + x].GetComponent<Tile>().inMoveRange = true;
                    TileMapGenerator.tiles[(int)gridPosition.y + z, (int)gridPosition.x + x].GetComponent<Tile>().updated = false;
                }
            }
        }
        updated = true;
    }

    public void move(Vector2 nPos)
    {
        moveTilesReset(nPos);
        if(!unitPositions[(int)nPos.x, (int)nPos.y] && Mathf.Abs(nPos.x - gridPosition.x) <= moveRange && Mathf.Abs(nPos.y - gridPosition.y) <= moveRange){
            unitPositions[(int)gridPosition.x, (int)gridPosition.y] = false; // maybe add a message about move range if not in range
            gridPosition = nPos;
            unitPositions[(int)nPos.x, (int)nPos.y] = true;
            gameObject.transform.position = new Vector3(gridPosition.x * TileMapGenerator.tileSize, 0, gridPosition.y * TileMapGenerator.tileSize);
        }
    }

    public void moveTilesReset(Vector2 nPos)
    {
        for(int z = -moveRange; z <= moveRange; z++){
            for(int x = -moveRange; x <= moveRange; x++){
                if((int)gridPosition.y + z > -1 && (int)gridPosition.y + z < TileMapGenerator.tiles.GetLength(0) && (int)gridPosition.x + x > -1 && (int)gridPosition.x + x < TileMapGenerator.tiles.GetLength(1)) {
                    TileMapGenerator.tiles[(int)gridPosition.y + z, (int)gridPosition.x + x].GetComponent<Tile>().clickState = ClickSystem.ClickState.none;
                    TileMapGenerator.tiles[(int)gridPosition.y + z, (int)gridPosition.x + x].GetComponent<Tile>().inMoveRange = false; //if the unit is on the edge this gives an out of bounds error
                    TileMapGenerator.tiles[(int)gridPosition.y + z, (int)gridPosition.x + x].GetComponent<Tile>().updated = false;
                }
            }
        }
    }
}