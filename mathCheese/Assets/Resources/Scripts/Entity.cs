using UnityEngine;

public class Entity : MonoBehaviour
{
    public Vector2 gridPosition;
    public ClickSystem.ClickState clickState;
    public bool updated;
    public Renderer borderRenderer; 

    public virtual void initialize(Vector2 gPos)
    {
        gridPosition = gPos;
        clickState = ClickSystem.ClickState.none;
        updated = false;
        borderRenderer = gameObject.transform.Find("Border").gameObject.GetComponent<Renderer>();
        borderRenderer.enabled = false;
    }

    public Collider getCollider()
    {
        return gameObject.GetComponent<Collider>();
    }

    public virtual void noClickState()
    {
        if(borderRenderer != null)
            borderRenderer.enabled = false;
        updated = true;    
    }

    public virtual void hoverClickState()
    {
        if(borderRenderer != null)
            borderRenderer.enabled = true;
        updated = true;
    }

    public virtual void clickClickState()
    {
        if(borderRenderer != null)
            borderRenderer.enabled = true;
        updated = true;
    }

    public virtual void Update() {
        if(!updated){
            if(clickState == ClickSystem.ClickState.none)
                noClickState();
            else if(clickState == ClickSystem.ClickState.hover)
                hoverClickState();
            else if(clickState == ClickSystem.ClickState.click)
                clickClickState();
        }    
    }
}
