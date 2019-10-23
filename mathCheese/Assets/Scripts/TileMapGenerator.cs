using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileMapGenerator : MonoBehaviour
{
    public int mapWidth, mapHeight;
    public Transform tilePrefab;

    private Transform[,] tiles;
    private float tileSize;
    Renderer renderComponent;
    void Start()
    {
        tiles = new Transform[mapHeight, mapWidth];
        renderComponent = tilePrefab.GetComponent<Renderer>();
        tileSize = renderComponent.bounds.size.x;
        buildMap();
    }

    public void buildMap()
    {
        int z, x;
        Quaternion up = new Quaternion(-1,0,0,1);
        for(z = 0; z < mapHeight; z++){
            for(x = 0; x < mapWidth; x++){
                tiles[z, x] = Instantiate(tilePrefab, new Vector3(x*tileSize, 0, z*tileSize), up);
            }
        }
    }
}
