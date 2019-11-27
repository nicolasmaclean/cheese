using System;
using UnityEngine;

public class MouseOver : MonoBehaviour
{
    Collider collideComponent;

    void Start() 
    {
        if(gameObject.GetComponent<Unit>() != null)
            collideComponent = gameObject.GetComponent<Unit>().getCollider();
        else if(gameObject.GetComponent<Tile>() != null)
            collideComponent = gameObject.GetComponent<Tile>().getCollider();
    }

    bool checkCollision()
    {
        if(collideComponent != null &&  ClickSystem.hitInfo.collider == collideComponent) {//collideComponent.Raycast( ray, out hitInfo, 1000 ) && hitInfo.collider.Equals(collideComponent)) {
            return true;
        }
        return false;
    }

    void Update()
    { //move as much of this to the click system so there is one big check instead of hundreds of small ones
    //maybe move chunks of if statements into external functions to imrove readibility
        bool collision = checkCollision();
        if(collision){
            if(Input.GetMouseButtonDown(0) && !ClickSystem.clickHistory.Contains(gameObject)){ //clicked
                if(gameObject.GetComponent<Tile>() != null && gameObject.GetComponent<Tile>().clickState != ClickSystem.ClickState.click){
                    gameObject.GetComponent<Tile>().clickState = ClickSystem.ClickState.click;
                    gameObject.GetComponent<Tile>().updated = false;
                } else if(gameObject.GetComponent<Unit>() != null && gameObject.GetComponent<Unit>().clickState != ClickSystem.ClickState.click){
                    if(ClickSystem.clickHistory[ClickSystem.clickHistory.Count-1] != null && ClickSystem.clickHistory[ClickSystem.clickHistory.Count-1].GetComponent<Unit>() != null && gameObject != ClickSystem.clickHistory[ClickSystem.clickHistory.Count-1])
                        ClickSystem.clickHistory[ClickSystem.clickHistory.Count-1].GetComponent<Unit>().moveTilesReset();
                    ClickSystem.clickHistory.Clear();
                    gameObject.GetComponent<Unit>().clickState = ClickSystem.ClickState.click;
                    gameObject.GetComponent<Unit>().updated = false;
                }
                ClickSystem.clickHistory.Add(gameObject);

            } else if(Input.GetMouseButtonDown(0) && ClickSystem.clickHistory.IndexOf(gameObject) == ClickSystem.clickHistory.Count-1) { //clicked again to deselect
                ClickSystem.clickHistory.Remove(gameObject);
                ClickSystem.clickHistory.Add(null);
                if(gameObject.GetComponent<Tile>() != null){
                    gameObject.GetComponent<Tile>().clickState = ClickSystem.ClickState.none;
                    gameObject.GetComponent<Tile>().updated = false;
                } else if(gameObject.GetComponent<Unit>() != null){
                    gameObject.GetComponent<Unit>().clickState = ClickSystem.ClickState.none;
                    gameObject.GetComponent<Unit>().updated = false;
                    gameObject.GetComponent<Unit>().moveTilesReset();
                }

            } else if(!ClickSystem.clickHistory.Contains(gameObject)) { //hover
                ClickSystem.clickHistory.Remove(gameObject);
                if(gameObject.GetComponent<Tile>() != null && gameObject.GetComponent<Tile>().clickState != ClickSystem.ClickState.hover){
                    gameObject.GetComponent<Tile>().clickState = ClickSystem.ClickState.hover;
                    gameObject.GetComponent<Tile>().updated = false;
                } else if(gameObject.GetComponent<Unit>() != null && gameObject.GetComponent<Unit>().clickState != ClickSystem.ClickState.hover){
                    gameObject.GetComponent<Unit>().clickState = ClickSystem.ClickState.hover;
                    gameObject.GetComponent<Unit>().updated = false;
                }
            }

        } else if(ClickSystem.clickHistory.IndexOf(gameObject) != ClickSystem.clickHistory.Count-1) { //default
            if(gameObject.GetComponent<Tile>() != null && gameObject.GetComponent<Tile>().clickState != ClickSystem.ClickState.none){
                gameObject.GetComponent<Tile>().clickState = ClickSystem.ClickState.none;
                gameObject.GetComponent<Tile>().updated = false;
            } else if(gameObject.GetComponent<Unit>() != null && gameObject.GetComponent<Unit>().clickState != ClickSystem.ClickState.none){
                gameObject.GetComponent<Unit>().clickState = ClickSystem.ClickState.none;
                gameObject.GetComponent<Unit>().updated = false;
            }
        }
    }
}