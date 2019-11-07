using UnityEngine;

public class Unit : MonoBehaviour
{
    public Vector2 gridPosition;

    Material unitMeshMaterial;
    Transform borderT;

    public void instantiateUnit(Vector2 gPos, bool mouse)
    {
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
        go.GetComponent<Renderer>().sharedMaterial = unitMeshMaterial;
        borderT.GetComponent<Renderer>().enabled = false;
    }

    void onClick(GameObject go)
    {
        go.GetComponent<Renderer>().material.color = Color.red;
        borderT.GetComponent<Renderer>().enabled = true;
    }

    public void move(Vector3 nPos)
    {
        gridPosition = nPos;
        gameObject.transform.position = new Vector3(gridPosition.x * TileMapGenerator.tileSize, 0, gridPosition.y * TileMapGenerator.tileSize);
    }
}