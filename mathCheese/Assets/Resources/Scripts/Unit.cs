using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public static bool[,] unitPositions = new bool[TileMapGenerator.mapWidth, TileMapGenerator.mapHeight];
    public Vector2 gridPosition;

    Material unitMeshMaterial;
    Transform borderT;

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
            gameObject.GetComponent<MouseOver>().instantiate(onHover, notHover, onClick, MouseOver.GameObjectType.Unit);
        }
    }
    
    void onHover(GameObject go)
    {
        borderT.GetComponent<Renderer>().enabled = true;
    }

    void notHover(GameObject go)
    {
        // go.GetComponent<Renderer>().sharedMaterial = unitMeshMaterial;
        borderT.GetComponent<Renderer>().enabled = false;
    }

    void onClick(GameObject go)
    {
        // go.GetComponent<Renderer>().material.color = Color.red;
        borderT.GetComponent<Renderer>().enabled = true;
    }

    public void move(Vector2 nPos)
    {
        if(!unitPositions[(int)nPos.x, (int)nPos.y]){
            unitPositions[(int)gridPosition.x, (int)gridPosition.y] = false;
            gridPosition = nPos;
            unitPositions[(int)nPos.x, (int)nPos.y] = true;
            gameObject.transform.position = new Vector3(gridPosition.x * TileMapGenerator.tileSize, 0, gridPosition.y * TileMapGenerator.tileSize);
        }
    }
}