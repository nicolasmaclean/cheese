using System;
using UnityEngine;

public class MouseOver : MonoBehaviour
{
    public enum GameObjectType {Tile, Unit}
    public GameObjectType goType;

    Collider collideComponent;

    void Start() 
    {
        collideComponent = GetComponent<Collider>();
    }

    public void instantiate(GameObjectType goT)
    {
        goType = goT;
    }

    bool checkCollision()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;

        Physics.Raycast(ray, out hitInfo, 1000);

        if(collideComponent != null &&  hitInfo.collider == collideComponent) {//collideComponent.Raycast( ray, out hitInfo, 1000 ) && hitInfo.collider.Equals(collideComponent)) {
            return true;
        }
        return false;
    }

    public void move(Vector3 nPos)
    {
        gameObject.transform.position = nPos;
        // move the unit border
    }

    void Update() //bug if a tile is clicked, then the ant, the tile border is still enabled
    { //move as much of this to the click system so there is one big check instead of hundreds of small ones
        bool collision = checkCollision();
        if(collision && Input.GetMouseButtonDown(0) && !ClickSystem.clickHistory.Contains(gameObject)){ //clicked
            if(goType == GameObjectType.Tile){
                gameObject.transform.parent.gameObject.GetComponent<Tile>().clickState = ClickSystem.ClickState.click;
                gameObject.transform.parent.gameObject.GetComponent<Tile>().updated = false;
            } else if(goType == GameObjectType.Unit){
                ClickSystem.clickHistory.Clear();
                gameObject.GetComponent<Unit>().clickState = ClickSystem.ClickState.click;
                gameObject.GetComponent<Unit>().updated = false;
            }
            ClickSystem.clickHistory.Add(gameObject);

        } else if(collision && Input.GetMouseButtonDown(0) && ClickSystem.clickHistory.IndexOf(gameObject) == ClickSystem.clickHistory.Count-1) { //clicked again to deselect
            ClickSystem.clickHistory.Remove(gameObject);
            if(goType == GameObjectType.Tile){// && !gameObject.transform.parent.gameObject.GetComponent<Tile>().isInMoveRange){
                gameObject.transform.parent.gameObject.GetComponent<Tile>().clickState = ClickSystem.ClickState.none;
                gameObject.transform.parent.gameObject.GetComponent<Tile>().updated = false;
            } else if(goType == GameObjectType.Unit){
                gameObject.GetComponent<Unit>().clickState = ClickSystem.ClickState.none;
                gameObject.GetComponent<Unit>().updated = false;
            }

        } else if(collision && ClickSystem.clickHistory.IndexOf(gameObject) != ClickSystem.clickHistory.Count-1) { //hover
            ClickSystem.clickHistory.Remove(gameObject);
            if(goType == GameObjectType.Tile){
                gameObject.transform.parent.gameObject.GetComponent<Tile>().clickState = ClickSystem.ClickState.hover;
                gameObject.transform.parent.gameObject.GetComponent<Tile>().updated = false;
            } else if(goType == GameObjectType.Unit){
                gameObject.GetComponent<Unit>().clickState = ClickSystem.ClickState.hover;
                gameObject.GetComponent<Unit>().updated = false;
            }
                
        } else if(ClickSystem.clickHistory.IndexOf(gameObject) != ClickSystem.clickHistory.Count-1) { //default
            if(goType == GameObjectType.Tile){//  && !gameObject.transform.parent.gameObject.GetComponent<Tile>().isInMoveRange){
                gameObject.transform.parent.gameObject.GetComponent<Tile>().clickState = ClickSystem.ClickState.none;
                gameObject.transform.parent.gameObject.GetComponent<Tile>().updated = false;
            } else if(goType == GameObjectType.Unit){
                gameObject.GetComponent<Unit>().clickState = ClickSystem.ClickState.none;
                gameObject.GetComponent<Unit>().updated = false;
            }
        }
    }
}