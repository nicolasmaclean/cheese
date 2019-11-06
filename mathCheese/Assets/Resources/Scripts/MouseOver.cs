using System;
using UnityEngine;

public class MouseOver : MonoBehaviour
{
    public enum GameObjectType {Tile, Unit}
    public GameObjectType goType;

    Collider collideComponent;
    Action<GameObject> onHover;
    Action<GameObject> notHover;
    Action<GameObject> onClick;

    void Start() 
    {
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

    public void move(Vector3 nPos)
    {
        gameObject.transform.position = nPos;
        // move the unit border
    }

    void Update()
    {
        bool collision = checkCollision();
        if(collision && Input.GetMouseButtonDown(0) && onClick != null && !ClickSystem.clickHistory.Contains(gameObject)){
            onClick(gameObject);
            ClickSystem.clickHistory.Add(gameObject);
        } else if(collision && Input.GetMouseButtonDown(0) && onClick != null && ClickSystem.clickHistory.IndexOf(gameObject) == ClickSystem.clickHistory.Count-1) {
            ClickSystem.clickHistory.Remove(gameObject);
            notHover(gameObject);
        } else if(collision && onHover != null && ClickSystem.clickHistory.IndexOf(gameObject) != ClickSystem.clickHistory.Count-1) {
            onHover(gameObject);
        } else if(notHover != null && ClickSystem.clickHistory.IndexOf(gameObject) != ClickSystem.clickHistory.Count-1) {
            notHover(gameObject);
        }
    }
}