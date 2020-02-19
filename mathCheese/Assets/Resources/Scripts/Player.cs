using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public List<Transform> units = new List<Transform>();
    public int larvae = 0, currentColony = 0, level = 2;
    public float gold = 0, water = 0, food = 0;
    public List<Tile> colonies = new List<Tile>();

    public void updateLarvae()
    {
        foreach(Tile unit in colonies) {
            larvae += level;
        }
    }

    public void updateResources(float[] resources)
    {
        food += resources[0];
        water += resources[1];
        gold += resources[2];
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

    public void cycleColony(int i)
    {
        currentColony += i;
        if(currentColony < 0)
            currentColony = colonies.Count-1;
        else if(currentColony >= colonies.Count)
            currentColony = 0;
    }

    public void removeUnit(GameObject go)
    {
        units.Remove(go.transform);
    }

    public void reset()
    {
        larvae = 0;
        foreach(Transform unit in units){
            removeUnit(unit.gameObject);
        }
    }
}
