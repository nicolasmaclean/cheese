using System;
using System.Collections.Generic;
using UnityEngine;

public class MouseOver : MonoBehaviour
{
    public static List<GameObject> lastClicked = new List<GameObject>();
    public enum GameObjectType {Tile, Unit}
    public GameObjectType goType;

    Collider collideComponent;
    Action<GameObject> onHover;
    Action<GameObject> notHover;
    Action<GameObject> onClick;
    Action<GameObject> notClick;

    void Start() 
    {
        if(lastClicked.Count == 0)
            lastClicked.Add(null);
        collideComponent = GetComponent<Collider>();
    }

    public void instantiate(Action<GameObject> onHov, Action<GameObject> notHov, Action<GameObject> onCl, GameObjectType goT)
    {
        onHover = onHov;
        notHover = notHov;
        onClick = onCl;
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

    void Update()
    {
        //clears lastClicked
        while(lastClicked.Count > 2){
            lastClicked.RemoveAt(0);
        }

        bool collision = checkCollision();
        if(collision && Input.GetMouseButtonDown(0) && onClick != null && !lastClicked.Contains(gameObject)){
            onClick(gameObject);
            lastClicked.Add(gameObject);
        } else if(collision && onHover != null && lastClicked.IndexOf(gameObject) != lastClicked.Count-1) {
            onHover(gameObject);
        } else if(notHover != null && lastClicked.IndexOf(gameObject) != lastClicked.Count-1) {
            notHover(gameObject);
        }
    }
}
