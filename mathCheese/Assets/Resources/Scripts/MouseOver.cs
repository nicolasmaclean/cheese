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
        if(entityScript != null && collision){

            if(Input.GetMouseButtonDown(0) && !clickHistory.Contains(gameObject)){ //clicked
                if(gameObject.GetComponent<Unit>() != null){
                    if(clickHistory.Count > 0 && clickHistory[clickHistory.Count-1] != null && clickHistory[clickHistory.Count-1].GetComponent<Unit>() != null && gameObject.GetComponent<Unit>() != null) // resets move tiles when switching selection between units
                        clickHistory[clickHistory.Count-1].GetComponent<Unit>().moveTilesReset();
                    clickHistory.Clear();
                }

                entityScript.clickState = ClickSystem.ClickState.click;
                entityScript.updated = false;

                clickHistory.Add(gameObject);

            } else if(Input.GetMouseButtonDown(0) && clickHistory.Count > 0 && clickHistory.IndexOf(gameObject) == clickHistory.Count-1) { //clicked again to deselect
                clickHistory.Remove(gameObject);
                clickHistory.Add(null);

                entityScript.clickState = ClickSystem.ClickState.none;
                entityScript.updated = false;

                if(gameObject.GetComponent<Unit>() != null)
                    gameObject.GetComponent<Unit>().moveTilesReset();

            } else if(!clickHistory.Contains(gameObject)) { //hover
                if(entityScript.clickState != ClickSystem.ClickState.hover){
                    // clickHistory.Remove(gameObject);

                    entityScript.clickState = ClickSystem.ClickState.hover;
                    entityScript.updated = false;
                }

            }

        } else if(clickHistory.Count == 0 || (clickHistory.Count > 0 && clickHistory.IndexOf(gameObject) != clickHistory.Count-1)) { //default
            if(entityScript.clickState != ClickSystem.ClickState.none){
                entityScript.clickState = ClickSystem.ClickState.none;
                entityScript.updated = false;
            }

        }
    }
}