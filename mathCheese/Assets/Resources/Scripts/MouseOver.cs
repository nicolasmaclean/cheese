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
    { //move as much of this to the click system so there is one big check instead of hundreds of small ones
    //maybe move chunks of if statements into external functions to imrove readibility
        collision = checkCollision();
        if(entityScript != null && clickHistory != null && collision){
            if(Input.GetMouseButtonDown(0)){ // clicked
                entityScript.clicked(clickHistory);

            }else if(!clickHistory.Contains(gameObject)) { // hover
                if(entityScript.clickState != ClickSystem.ClickState.hover){
                    entityScript.clickState = ClickSystem.ClickState.hover;
                    entityScript.updated = false;
                }
            }
        } else if(clickHistory.Count == 0 || (clickHistory.Count > 0 && clickHistory.IndexOf(gameObject) != clickHistory.Count-1)) { // default
            if(entityScript.clickState != ClickSystem.ClickState.none){
                entityScript.clickState = ClickSystem.ClickState.none;
                entityScript.updated = false;
            }
        }
    }
}