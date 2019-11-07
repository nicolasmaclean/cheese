using UnityEngine;

public class Tile : MonoBehaviour
{
    public Vector2 gridPosition;
    public Material groundMat;
    Renderer tileBorderRenderer;
    Transform groundT;

    public void instantiateTile(Vector2 gPos, bool mouse)
    {
        //position of tile
        gridPosition = gPos;

        groundT = gameObject.transform.Find("Ground");
        groundMat = groundT.GetComponent<Renderer>().sharedMaterial;
        tileBorderRenderer = gameObject.transform.Find("Border").gameObject.GetComponent<Renderer>();
        
        tileBorderRenderer.enabled = false;

        //adds mouse over if specified
        if(mouse){
            groundT.gameObject.AddComponent<MouseOver>();
            groundT.GetComponent<MouseOver>().instantiate(onHover, notHover, onClick, MouseOver.GameObjectType.Tile);
        }
    }

    public void onHover(GameObject go)
    {
            groundT.GetComponent<Renderer>().material.color = Color.green;
            tileBorderRenderer.enabled = true;
    }

    public void notHover(GameObject go)
    {
            groundT.GetComponent<Renderer>().sharedMaterial = groundMat;
            tileBorderRenderer.enabled = false;
    }
    
    public void onClick(GameObject go)
    {
        groundT.GetComponent<Renderer>().material.color = Color.red;
        tileBorderRenderer.enabled = true;
    }
}