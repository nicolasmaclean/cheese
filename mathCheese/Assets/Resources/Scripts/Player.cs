using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public List<Transform> units = new List<Transform>();
    public int larvae = 0;

    public void updateLarvae()
    {
        foreach(Transform unit in units) {
            if(unit.GetComponent<UnitHarvester>() != null) {
                larvae += unit.GetComponent<UnitHarvester>().harvestTile();
            }
        }
    }

    public void addUnit(Transform mesh, Vector2 pos, Quaternion up)
    {
        if(!Unit.isTileFilled(pos)){
            Transform tempT = Instantiate(mesh, new Vector3(pos.x * TileMapGenerator.tileSize, 0, pos.y * TileMapGenerator.tileSize), up);
            tempT.gameObject.GetComponent<Unit>().initialize(pos);
            tempT.parent = gameObject.transform;
            units.Add(tempT);
        }
    }

    public void removeUnit(GameObject go)
    {
        units.Remove(go.transform);
        Destroy(go);
    }

    public void reset()
    {
        larvae = 0;
        foreach(Transform unit in units){
            removeUnit(unit.gameObject);
        }
    }
}
