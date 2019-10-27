using UnityEngine;

public class TileMapGenerator : MonoBehaviour
{
    public int mapWidth, mapHeight;
    public Transform[] tilePrefabs;
    public Transform tileBorderPrefab;

    private Tile[,] tiles;
    private float tileSize;

    void Start()
    {
        tiles = new Tile[mapHeight, mapWidth];
        tileSize = tilePrefabs[0].GetComponent<Renderer>().bounds.size.x;

        buildMap();
    }

    public void buildMap()
    {
        Quaternion up = new Quaternion(-1,0,0,1);
        int[,] noiseMap = Noise.GenerateNoiseMap(mapWidth, mapHeight, tilePrefabs.Length);
        
        for(int z = 0; z < mapHeight; z++){
            for(int x = 0; x < mapWidth; x++){
                tiles[z, x] = new Tile(tilePrefabs[noiseMap[z, x]], tileBorderPrefab, new Vector3(x*tileSize, 0, z*tileSize), up, gameObject.transform, true);
            }
        }

    }
}