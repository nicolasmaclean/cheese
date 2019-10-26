using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class MouseOver : MonoBehaviour
{
    Collider collideComponent;
    Action<GameObject> onHover;
    Action<GameObject> notHover;
    Action<GameObject> onClick;
    Action<GameObject> notClick;

    void Start() 
    {
        collideComponent = GetComponent<Collider>();
    }

    public void instantiate(Action<GameObject> onHov, Action<GameObject> notHov)
    {
        onHover = onHov;
        notHover = notHov;
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
        if(checkCollision() && onHover != null) {
            onHover(gameObject);
        } else if(notHover != null) {
            notHover(gameObject);
        }
    }
}
