using UnityEngine;

public class Unit : MonoBehaviour
{
    public Vector2 unitPosition;
    public Transform unitMeshPrefab;
    public Transform borderPrefab;

    Material unitMeshMaterial;

    private Transform unitMesh;
    private Transform borderMesh;
    
    void Start()
    {
        unitMeshMaterial = unitMeshPrefab.gameObject.GetComponent<Renderer>().sharedMaterial;
        instantiateMesh();
    }

    void onHover(GameObject go)
    {
        // go.GetComponent<Renderer>().material.color = Color.green;
        borderMesh.GetComponent<Renderer>().enabled = true;
    }

    void notHover(GameObject go)
    {
        go.GetComponent<Renderer>().sharedMaterial = unitMeshMaterial;
        borderMesh.GetComponent<Renderer>().enabled = false;
    }

    void onClick(GameObject go)
    {
        go.GetComponent<Renderer>().material.color = Color.red;
        borderMesh.GetComponent<Renderer>().enabled = true;
    }

    public void instantiateMesh()
    {
        Quaternion up = new Quaternion(-1,0,0,1);
        unitMesh = Instantiate(unitMeshPrefab, unitPosition, up);
        unitMesh.parent = gameObject.transform;
        unitMesh.gameObject.AddComponent<MouseOver>();
        unitMesh.gameObject.GetComponent<MouseOver>().instantiate(onHover, notHover, onClick, MouseOver.GameObjectType.Unit);

        borderMesh = Instantiate(borderPrefab, new Vector3(unitPosition.x, 2, unitPosition.y), up);
        borderMesh.parent = gameObject.transform;
        borderMesh.GetComponent<Renderer>().enabled = false;
    }

    public void move(Vector3 nPos)
    {
        unitMesh.position = nPos;
        borderMesh.position = new Vector3(nPos.x, nPos.y + 2, nPos.z);
    }
}