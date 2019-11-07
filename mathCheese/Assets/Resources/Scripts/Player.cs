using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public List<Transform> units = new List<Transform>();
    public int larvae = 0;

    // void Start() {
    //     units = new List<Transform>();
    //     larvae = 0;    
    // }

    public void log(){Debug.Log("logging");}

    public void addUnit(Transform mesh, Vector2 pos, Quaternion up)
    {
        Transform tempT = Instantiate(mesh, new Vector3(pos.x * TileMapGenerator.tileSize, 0, pos.y * TileMapGenerator.tileSize), up);
        tempT.gameObject.AddComponent<Unit>();
        tempT.gameObject.GetComponent<Unit>().instantiateUnit(pos, true);
        units.Add(tempT);
    }

    public void EndTurn()
    {

    }
}
