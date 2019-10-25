using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseOver : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay( Input.mousePosition);
        RaycastHit hitInfo;

        if(GetComponent<Collider>().Raycast( ray, out hitInfo, 1000 )) 
            GetComponent<Renderer>().material.color = Color.green;
        else
            GetComponent<Renderer>().material.color = Color.blue;
    }
}
