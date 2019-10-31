using UnityEngine;

public class TileMapGenerator : MonoBehaviour
{
    public int mapWidth, mapHeight;
    public Transform[] tilePrefabs;
    public Transform tileBorderPrefab;
    public static float tileSizeX;
    public static float tileSizeZ;
    public bool autoUpdate;

    private Tile[,] tiles;

    void Start()
    {
        for(int i = gameObject.transform.childCount-1; i > -1; i--){
            DestroyImmediate(gameObject.transform.GetChild(i).gameObject);
        }
        buildMap();
    }

    public void buildMap()
    {
        if(mapHeight > 0 && mapWidth > 0){
            tiles = new Tile[mapHeight, mapWidth];
            tileSizeX = tilePrefabs[0].transform.Find("Ground").GetComponent<Renderer>().bounds.size.x;
            tileSizeZ = tilePrefabs[0].transform.Find("Ground").GetComponent<Renderer>().bounds.size.z;

            Quaternion up = new Quaternion(0,0,0,1);
            int[,] noiseMap = Noise.GenerateNoiseMap(mapWidth, mapHeight, tilePrefabs.Length);
            
            for(int z = 0; z < mapHeight; z++){
                for(int x = 0; x < mapWidth; x++){
                    tiles[z, x] = new Tile(tilePrefabs[noiseMap[z, x]], tileBorderPrefab, new Vector3(x*tileSizeX, 0, z*tileSizeZ), up, gameObject.transform, true);
                }
            }
        }

    }
}