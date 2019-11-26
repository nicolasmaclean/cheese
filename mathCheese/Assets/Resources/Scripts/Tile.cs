using UnityEngine;

public class Tile : MonoBehaviour
{
    public Vector2 gridPosition;
    public Material groundMat;
    public ClickSystem.ClickState clickState = ClickSystem.ClickState.none;
    public bool updated = false;
    public bool isInMoveRange = false;
    Renderer tileBorderRenderer;
    Transform groundT;

    public void instantiateTile(Vector2 gPos)
    {
        //position of tile
        gridPosition = gPos;

        groundT = gameObject.transform.Find("Ground");
        groundMat = groundT.GetComponent<Renderer>().sharedMaterial;
        tileBorderRenderer = gameObject.transform.Find("Border").gameObject.GetComponent<Renderer>();
        
        tileBorderRenderer.enabled = false;

    }

    public Collider getCollider()
    {
        return gameObject.GetComponent<Collider>();
    }

    void Update() {
        if(!updated){
            if(clickState == ClickSystem.ClickState.none)
                noClickState();
            else if(clickState == ClickSystem.ClickState.hover)
                hoverClickState();
            else if(clickState == ClickSystem.ClickState.click)
                clickClickState();
            if(isInMoveRange)
                inMoveRange();
        }
    }

    public void noClickState()
    {
        groundT.GetComponent<Renderer>().sharedMaterial = groundMat;
        tileBorderRenderer.enabled = false;
        updated = true;
    }

    public void hoverClickState()
    {
        groundT.GetComponent<Renderer>().material.color = Color.green;
        tileBorderRenderer.enabled = true;
        updated = true;
    }
    
    public void clickClickState()
    {
        groundT.GetComponent<Renderer>().material.color = Color.red;
        tileBorderRenderer.enabled = true;
        updated = true;
    }

    public void inMoveRange()
    {
        groundT.GetComponent<Renderer>().material.color = Color.blue;
        updated = true;
    }
}