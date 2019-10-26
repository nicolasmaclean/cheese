using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public Vector2 unitPosition;
    public Transform unitMeshPrefab;

    private Transform unitMesh;
    void Start()
    {
        instantiateMesh();
    }

    void onHover(GameObject go)
    {
        go.GetComponent<Renderer>().material.color = Color.green;
    }

    void notHover(GameObject go)
    {
        go.GetComponent<Renderer>().material.color = Color.blue;
    }
    public void instantiateMesh()
    {
        Quaternion up = new Quaternion(-1,0,0,1);
        unitMesh = Instantiate(unitMeshPrefab, unitPosition, up);
        unitMesh.parent = gameObject.transform;
        unitMesh.gameObject.AddComponent<MouseOver>();
        unitMesh.gameObject.GetComponent<MouseOver>().instantiate(onHover, notHover);
    }

    public void move()
    {
        unitMesh.position = new Vector3(unitPosition.x, unitMesh.position.y, unitPosition.y);
    }

    void Update()
    {
        if(unitPosition != new Vector2(unitMesh.position.x, unitMesh.position.z)){
            move();
        }
    }
}
