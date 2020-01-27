using UnityEngine;

public class Entity : MonoBehaviour
{
    public Vector2 gridPosition;
    public ClickSystem.ClickState clickState;
    public bool updated;
    public Renderer borderRenderer; 
    public string entityName = "Entity";

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

    public virtual void clicked(System.Collections.Generic.List<GameObject> clickHistory)
    {
        if(clickHistory.Count == 0 || clickHistory.IndexOf(gameObject) != clickHistory.Count-1){
            clickState = ClickSystem.ClickState.click;
            clickHistory.Add(gameObject);

        } else if(clickHistory.IndexOf(gameObject) == clickHistory.Count-1) {
            clickState = ClickSystem.ClickState.none;
        }

        updated = false;
    }

    public virtual void hovered(System.Collections.Generic.List<GameObject> clickHistory)
    {
        if(clickState != ClickSystem.ClickState.hover){
            clickState = ClickSystem.ClickState.hover;
            updated = false;
        }
    }

    public virtual void inactive(System.Collections.Generic.List<GameObject> clickHistory)
    {
        if(clickState != ClickSystem.ClickState.none){
            clickState = ClickSystem.ClickState.none;
            updated = false;
        }
    }

    public virtual void Update() {
        if(!UIPauseManager.paused && !updated){
            if(clickState == ClickSystem.ClickState.none)
                noClickState();
            else if(clickState == ClickSystem.ClickState.hover)
                hoverClickState();
            else if(clickState == ClickSystem.ClickState.click)
                clickClickState();
        }    
    }

    public virtual void delete()
    {
        ClickSystem.clickHistory.RemoveAll(x => x == gameObject);

        Destroy(gameObject);
    }
}
