using UnityEngine;

public class Tile : Entity
{
    public bool isInMoveRange = false;
    public Material defaultMaterial;

    Transform groundT;

    public override void initialize(Vector2 gPos)
    {
        groundT = gameObject.transform.Find("Ground");
        defaultMaterial = groundT.GetComponent<Renderer>().sharedMaterial;

        base.initialize(gPos);
    }

    public override void Update() {
        if(!UIPauseManager.paused && !updated){
            base.Update();
            if(isInMoveRange)
                inMoveRange();
        }
    }

    public void reset()
    {
        clickState = ClickSystem.ClickState.none;
        updated = false;
        isInMoveRange = false;
    }

    public override void noClickState()
    {
        groundT.GetComponent<Renderer>().sharedMaterial = defaultMaterial;
        base.noClickState();
    }

    public override void hoverClickState()
    {
        groundT.GetComponent<Renderer>().material.color = Color.green;
        base.hoverClickState();
    }
    
    public override void clickClickState()
    {
        groundT.GetComponent<Renderer>().material.color = Color.red;
        base.clickClickState();
    }

    public void inMoveRange()
    {
        groundT.GetComponent<Renderer>().material.color = Color.blue;
        updated = true;
    }

    public override void clicked(System.Collections.Generic.List<GameObject> clickHistory)
    {
        base.clicked(clickHistory);
        ClickSystem.checkClickMoveUnit();
    }
}