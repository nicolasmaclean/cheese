using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseOver : MonoBehaviour
{

    public Transform borderPrefab;
    private Transform border;

    void Start() {
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
