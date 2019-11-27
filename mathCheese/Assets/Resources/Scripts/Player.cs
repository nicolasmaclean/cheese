﻿using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public List<Transform> units = new List<Transform>();
    public int larvae = 0;

    public void addUnit(Transform mesh, Vector2 pos, Quaternion up)
    {
        if(!Unit.isTileFilled(pos)){
            Transform tempT = Instantiate(mesh, new Vector3(pos.x * TileMapGenerator.tileSize, 0, pos.y * TileMapGenerator.tileSize), up);
            tempT.gameObject.GetComponent<Unit>().instantiateUnit(pos);
            tempT.parent = gameObject.transform;
            units.Add(tempT);
        }
    }

    public void removeUnit(Transform transfrom)
    {
        units.Remove(transform);
    }

    public void EndTurn()
    {

    }
}
