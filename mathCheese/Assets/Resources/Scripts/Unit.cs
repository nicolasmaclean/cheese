using UnityEngine;

public class Unit
{
    public Vector3 unitPosition;

    Material unitMeshMaterial;

    private Transform unitMesh;

    public Unit(Transform uMesh, Vector3 up)
    {
        Debug.Log("making unit");
        unitPosition = up;
        instantiateMesh(uMesh);
    }
    
    void onHover(GameObject go)
    {
        unitMesh.Find("Border").GetComponent<Renderer>().enabled = true;
    }

    void notHover(GameObject go)
    {
        go.GetComponent<Renderer>().sharedMaterial = unitMeshMaterial;
        unitMesh.Find("Border").GetComponent<Renderer>().enabled = false;
    }

    void onClick(GameObject go)
    {
        go.GetComponent<Renderer>().material.color = Color.red;
        unitMesh.Find("Border").GetComponent<Renderer>().enabled = true;
    }

    public void instantiateMesh(Transform uMesh)
    {
        Quaternion up = new Quaternion(-1,0,0,1);
        unitMeshMaterial = uMesh.gameObject.GetComponent<Renderer>().sharedMaterial;

        unitMesh = GameObject.Instantiate(uMesh, unitPosition, up);
        unitMesh.parent = unitMesh.gameObject.transform;
        unitMesh.gameObject.AddComponent<MouseOver>();
        unitMesh.gameObject.GetComponent<MouseOver>().instantiate(onHover, notHover, onClick, MouseOver.GameObjectType.Unit);

        unitMesh.Find("Border").GetComponent<Renderer>().enabled = false;
    }

    public void move(Vector3 nPos)
    {
        unitPosition = nPos;
        unitMesh.position = unitPosition;
    }
}