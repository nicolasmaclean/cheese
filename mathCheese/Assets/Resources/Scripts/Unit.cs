using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public Vector2 gridPosition;
    public int moveRange = 1;
    public ClickSystem.ClickState clickState;

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
        if(clickState == ClickSystem.ClickState.none)
            noClickState();
        if(clickState == ClickSystem.ClickState.hover)
            hoverClickState();
        if(clickState == ClickSystem.ClickState.click)
            clickClickState();
        if(clickState == ClickSystem.ClickState.inMoveRange)
            clickState = ClickSystem.ClickState.none; //units should never be in this state  
    }
    
    void hoverClickState()
    {
        borderT.GetComponent<Renderer>().enabled = true;
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
                if((int)gridPosition.y + z > -1 && (int)gridPosition.y + z < TileMapGenerator.tiles.GetLength(0) && (int)gridPosition.x > -1 && (int)gridPosition.x < TileMapGenerator.tiles.GetLength(1))
                TileMapGenerator.tiles[(int)gridPosition.y + z, (int)gridPosition.x + x].GetComponent<Tile>().clickState = ClickSystem.ClickState.inMoveRange;
            }
        }
    }

    public void move(Vector2 nPos)
    {
        if(!unitPositions[(int)nPos.x, (int)nPos.y] && Mathf.Abs(nPos.x - gridPosition.x) <= moveRange && Mathf.Abs(nPos.y - gridPosition.y) <= moveRange){
            unitPositions[(int)gridPosition.x, (int)gridPosition.y] = false; // maybe add a message about move range if not in range
            gridPosition = nPos;
            unitPositions[(int)nPos.x, (int)nPos.y] = true;
            gameObject.transform.position = new Vector3(gridPosition.x * TileMapGenerator.tileSize, 0, gridPosition.y * TileMapGenerator.tileSize);

            for(int z = 0; z < moveRange*2+1; z++){
                for(int x = 0; x < moveRange*2+1; x++){
                    TileMapGenerator.tiles[z, x].GetComponent<Tile>().clickState = ClickSystem.ClickState.none;
                }
            }
        }
        
    }
}