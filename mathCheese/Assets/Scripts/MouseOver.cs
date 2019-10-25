﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

private class MouseOver : MonoBehaviour
{

    public static Transform borderPrefab;

    void Start() {
        gameObject.GetComponent<MouseOver>().borderPrefab = Resources.Load("Meshes/tileBorder");
        border = Instantiate(borderPrefab, gameObject.transform.position, new Quaternion(-1,0,0,1));
    }
    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay( Input.mousePosition);
        RaycastHit hitInfo;

        if(GetComponent<Collider>().Raycast( ray, out hitInfo, 1000 )) 
            border.GetComponent<Renderer>().enabled = true;
        else
            border.GetComponent<Renderer>().enabled = false;
    }
}
