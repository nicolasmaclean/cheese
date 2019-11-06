using UnityEngine;

public class Unit
{
    public Vector2 gridPosition;

    Material unitMeshMaterial;

    private Transform unitMesh;

    public Unit(Transform uMesh, Vector2 gp)
    {
        Debug.Log("making unit");
        gridPosition = gp;
        instantiateMesh(uMesh);
    }
    
    void onHover(GameObject go)
    {
        unitMesh.Find("Border").GetComponent<Renderer>().enabled = true;
    }

    void notHover(GameObject go)
    {
        go.GetComponent<Renderer>().sharedMaterial = unitMeshMaterial;
        unitMesh.Find("Border").GetComponent<Renderer>().enabled = false;
    }

    void onClick(GameObject go)
    {
        go.GetComponent<Renderer>().material.color = Color.red;
        unitMesh.Find("Border").GetComponent<Renderer>().enabled = true;
    }

    public void instantiateMesh(Transform uMesh)
    {
        Quaternion up = new Quaternion(-1,0,0,1);
        unitMeshMaterial = uMesh.gameObject.GetComponent<Renderer>().sharedMaterial;

        unitMesh = GameObject.Instantiate(uMesh, gridPosition * TileMapGenerator.tileSize, up);
        unitMesh.parent = unitMesh.gameObject.transform;
        unitMesh.gameObject.AddComponent<MouseOver>();
        unitMesh.gameObject.GetComponent<MouseOver>().instantiate(onHover, notHover, onClick, MouseOver.GameObjectType.Unit);

        unitMesh.Find("Border").GetComponent<Renderer>().enabled = false;
    }

    public void move(Vector2 nPos)
    {
        gridPosition = nPos;
        unitMesh.position = nPos * TileMapGenerator.tileSize;
    }
}