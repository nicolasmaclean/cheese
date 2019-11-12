using UnityEngine;

public class Tile : MonoBehaviour
{
    public Vector2 gridPosition;
    public Material groundMat;
    public ClickSystem.ClickState clickState = ClickSystem.ClickState.none;
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
            groundT.GetComponent<MouseOver>().instantiate(MouseOver.GameObjectType.Tile);
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
            inMoveRangeClickState();
    }

    public void noClickState()
    {
        groundT.GetComponent<Renderer>().sharedMaterial = groundMat;
        tileBorderRenderer.enabled = false;
    }

    public void hoverClickState()
    {
        groundT.GetComponent<Renderer>().material.color = Color.green;
        tileBorderRenderer.enabled = true;
    }
    
    public void clickClickState()
    {
        groundT.GetComponent<Renderer>().material.color = Color.red;
        tileBorderRenderer.enabled = true;
    }

    public void inMoveRangeClickState()
    {
        groundT.GetComponent<Renderer>().material.color = Color.blue;
    }
}