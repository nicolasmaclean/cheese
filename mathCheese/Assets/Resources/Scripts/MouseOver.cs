using System.Collections.Generic;
using UnityEngine;

public class MouseOver : MonoBehaviour
{
    Collider collideComponent;
    Entity entityScript;
    bool collision;
    List<GameObject> clickHistory;

    void Start() 
    {
        entityScript = gameObject.GetComponent<Entity>();
        collideComponent = entityScript.getCollider();
        clickHistory = ClickSystem.clickHistory;
    }

    bool checkCollision()
    {
        if(collideComponent != null &&  ClickSystem.hitInfo.collider == collideComponent) {
            return true;
        }
        return false;
    }

    void Update()
    {
        collision = checkCollision();
        if(entityScript != null && collision){
            if(Input.GetMouseButtonDown(0)){ // clicked
                entityScript.clicked(clickHistory);
                ClickSystem.updateSelectionText(gameObject); // move this into clicked

            } else if(clickHistory.Count == 0 || entityScript.clickState == ClickSystem.ClickState.none) { // hover
                entityScript.hovered(clickHistory); //hover doesn't work if a tile is selected
                ClickSystem.updateSelectionText(gameObject);
            }
        } else if(clickHistory.Count == 0 || clickHistory.IndexOf(gameObject) != clickHistory.Count-1) { // default
            entityScript.inactive(clickHistory);
        }
    }
}