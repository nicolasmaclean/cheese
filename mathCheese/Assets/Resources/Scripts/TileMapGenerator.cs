using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileMapGenerator : MonoBehaviour
{
    public int mapWidth, mapHeight;
    public Transform[] tilePrefabs;
    // public Transform tileBorder;

    private Transform[,] tiles;
    private float tileSize;
    Renderer renderComponent;

    void Start()
    {
        tiles = new Transform[mapHeight, mapWidth];
        renderComponent = tilePrefabs[0].GetComponent<Renderer>();
        tileSize = renderComponent.bounds.size.x;
        buildMap();
    }

    public void onHover(GameObject go)
    {
        go.GetComponent<Renderer>().material.color = Color.green;
    }

    public void notHover(GameObject go)
    {
        go.GetComponent<Renderer>().material.color = Color.blue;
    }

    public void buildMap()
    {
        int z, x;
        Quaternion up = new Quaternion(-1,0,0,1);
        int[,] noiseMap = Noise.GenerateNoiseMap(mapWidth, mapHeight, tilePrefabs.Length);

        for(z = 0; z < mapHeight; z++){
            for(x = 0; x < mapWidth; x++){
                tiles[z, x] = Instantiate(tilePrefabs[noiseMap[z, x]], new Vector3(x*tileSize, 0, z*tileSize), up);
                tiles[z, x].parent = gameObject.transform;
                tiles[z, x].gameObject.AddComponent<MouseOver>();
                tiles[z, x].gameObject.GetComponent<MouseOver>().instantiate(onHover, notHover);
            }
        }
    }
}
