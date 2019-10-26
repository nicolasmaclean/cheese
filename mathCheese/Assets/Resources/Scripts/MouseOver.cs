using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class MouseOver : MonoBehaviour
{
    public TileMapGenerator tileMapGen;

    Action onHover;
    Action notHover;
    Action onClick;
    Action notClick;

    void Start() 
    {
        
    }

    public void instantiate(Action onHov, Action notHov)
    {
        onHover = onHov;
        notHover = notHov;
    }

    public bool checkCollision()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;

        if(GetComponent<Collider>().Raycast( ray, out hitInfo, 1000 )) {
            return true;
        }
        return false;
    }

    void Update()
    {
        if(checkCollision()){
            onHover();
        } else {
            notHover();
        }
    }
}
