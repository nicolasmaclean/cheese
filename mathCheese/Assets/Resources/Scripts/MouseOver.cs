using System;
using UnityEngine;

public class MouseOver : MonoBehaviour
{
    public static GameObject lastClicked;

    Collider collideComponent;
    Action<GameObject> onHover;
    Action<GameObject> notHover;
    Action<GameObject> onClick;
    Action<GameObject> notClick;

    void Start() 
    {
        collideComponent = GetComponent<Collider>();
    }

    public void instantiate(Action<GameObject> onHov, Action<GameObject> notHov, Action<GameObject> onCl)
    {
        onHover = onHov;
        notHover = notHov;
        onClick = onCl;
    }

    bool checkCollision()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;

        if(collideComponent != null && collideComponent.Raycast( ray, out hitInfo, 1000 )) {
            return true;
        }
        return false;
    }

    void Update()
    {
        bool collision = checkCollision();
        if(collision && Input.GetMouseButtonDown(0) && onClick != null){
            onClick(gameObject);
            lastClicked = gameObject;
        } else if(collision && onHover != null && gameObject != lastClicked) {
            onHover(gameObject);
        } else if(notHover != null && gameObject != lastClicked) {
            notHover(gameObject);
        }
    }
}
