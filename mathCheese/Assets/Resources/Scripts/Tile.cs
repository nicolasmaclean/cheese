using UnityEngine;

public class Tile
{
    public Transform tileMesh;
    public Vector3 position;
    public Material groundMat;
    Renderer tileBorderRenderer;

    public Tile(Transform tileM, Vector3 pos, Quaternion rot, Transform parent, bool mouse)
    {
        //position of tile
        position = pos;

        //makes the tile mesh and sets them up
        tileMesh = GameObject.Instantiate(tileM, position, rot);
        tileMesh.parent = parent;
        groundMat = tileMesh.Find("Ground").GetComponent<Renderer>().sharedMaterial;

        tileBorderRenderer = tileMesh.Find("Border").gameObject.GetComponent<Renderer>();
        tileBorderRenderer.enabled = false;

        //adds mouse over if specified
        if(mouse){
            tileMesh.Find("Ground").gameObject.AddComponent<MouseOver>();
            tileMesh.Find("Ground").GetComponent<MouseOver>().instantiate(onHover, notHover, onClick, MouseOver.GameObjectType.Tile);
        }
    }

    public void onHover(GameObject go)
    {
            tileMesh.Find("Ground").GetComponent<Renderer>().material.color = Color.green;
            tileBorderRenderer.enabled = true;
    }

    public void notHover(GameObject go)
    {
            tileMesh.Find("Ground").GetComponent<Renderer>().sharedMaterial = groundMat;
            tileBorderRenderer.enabled = false;
    }
    
    public void onClick(GameObject go)
    {
        tileMesh.Find("Ground").GetComponent<Renderer>().material.color = Color.red;
        tileBorderRenderer.enabled = true;
    }
}