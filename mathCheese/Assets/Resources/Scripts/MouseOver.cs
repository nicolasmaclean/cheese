using System;
using UnityEngine;

public class MouseOver : MonoBehaviour
{
    Collider collideComponent;

    void Start() 
    {
        collideComponent = GetComponent<Collider>();
    }

    bool checkCollision()
    {
        if(collideComponent != null &&  ClickSystem.hitInfo.collider == collideComponent) {//collideComponent.Raycast( ray, out hitInfo, 1000 ) && hitInfo.collider.Equals(collideComponent)) {
            return true;
        }
        return false;
    }

    void Update() //bug if a tile is clicked, then the ant, the tile border is still enabled
    { //move as much of this to the click system so there is one big check instead of hundreds of small ones
    //maybe move chunks of if statements to imrove readibility
        bool collision = checkCollision();
        if(collision){
            if(Input.GetMouseButtonDown(0) && !ClickSystem.clickHistory.Contains(gameObject)){ //clicked
                if(gameObject.name == "Ground"){
                    gameObject.transform.parent.gameObject.GetComponent<Tile>().clickState = ClickSystem.ClickState.click;
                    gameObject.transform.parent.gameObject.GetComponent<Tile>().updated = false;
                } else if(gameObject.GetComponent<Unit>() != null){
                    ClickSystem.clickHistory.Clear();
                    gameObject.GetComponent<Unit>().clickState = ClickSystem.ClickState.click;
                    gameObject.GetComponent<Unit>().updated = false;
                }
                ClickSystem.clickHistory.Add(gameObject);

            } else if(Input.GetMouseButtonDown(0) && ClickSystem.clickHistory.IndexOf(gameObject) == ClickSystem.clickHistory.Count-1) { //clicked again to deselect
                ClickSystem.clickHistory.Remove(gameObject);
                ClickSystem.clickHistory.Add(null);
                if(gameObject.name == "Ground"){
                    gameObject.transform.parent.gameObject.GetComponent<Tile>().clickState = ClickSystem.ClickState.none;
                    gameObject.transform.parent.gameObject.GetComponent<Tile>().updated = false;
                } else if(gameObject.GetComponent<Unit>() != null){
                    gameObject.GetComponent<Unit>().clickState = ClickSystem.ClickState.none;
                    gameObject.GetComponent<Unit>().updated = false;
                    gameObject.GetComponent<Unit>().moveTilesReset();
                }

            } else if(ClickSystem.clickHistory.IndexOf(gameObject) != ClickSystem.clickHistory.Count-1) { //hover
                ClickSystem.clickHistory.Remove(gameObject);
                if(gameObject.name == "Ground"){
                    gameObject.transform.parent.gameObject.GetComponent<Tile>().clickState = ClickSystem.ClickState.hover;
                    gameObject.transform.parent.gameObject.GetComponent<Tile>().updated = false;
                } else if(gameObject.GetComponent<Unit>() != null){
                    gameObject.GetComponent<Unit>().clickState = ClickSystem.ClickState.hover;
                    gameObject.GetComponent<Unit>().updated = false;
                }
            }

        } else if(ClickSystem.clickHistory.IndexOf(gameObject) != ClickSystem.clickHistory.Count-1) { //default
            if(gameObject.name == "Ground"){
                gameObject.transform.parent.gameObject.GetComponent<Tile>().clickState = ClickSystem.ClickState.none;
                gameObject.transform.parent.gameObject.GetComponent<Tile>().updated = false;
            } else if(gameObject.GetComponent<Unit>() != null){
                gameObject.GetComponent<Unit>().clickState = ClickSystem.ClickState.none;
                gameObject.GetComponent<Unit>().updated = false;
            }
        }
    }
}