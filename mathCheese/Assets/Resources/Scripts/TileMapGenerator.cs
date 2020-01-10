using UnityEngine;

public class TileMapGenerator : MonoBehaviour
{
    public static int mapWidth = 25, mapHeight = 25;
    public Transform[] tilePrefabs;
    public Transform tileColonyPrefab;
    public static float tileSize;
    public bool autoUpdate;

    public static Transform[,] tiles;

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
            tiles = new Transform[mapHeight, mapWidth];
            tileSize = tilePrefabs[0].transform.Find("Ground").GetComponent<Renderer>().bounds.size.x;
            Unit.unitPositions = new bool[mapHeight, mapWidth];
            Unit.units = new Unit[mapHeight, mapWidth];

            Quaternion up = new Quaternion(0,0,0,1);
            int[,] noiseMap = Noise.GenerateNoiseMap(mapWidth, mapHeight, tilePrefabs.Length);
            
            for(int z = 0; z < mapHeight; z++) {
                for(int x = 0; x < mapWidth; x++) {
                    tiles[z, x] = Instantiate(tilePrefabs[noiseMap[z, x]], new Vector3(x*tileSize, 0, z*tileSize), up); //move this into the tile instantiation
                    tiles[z, x].GetComponent<Tile>().initialize(new Vector2(x, z));
                    tiles[z, x].parent = gameObject.transform;
                }
            }
        }
    }

    public static bool createColony(int y, int x, int player)
    {
        if(tiles[y, x].GetComponent<TileColony>() == null) { // if its not already a colony
            if(tiles[y, x] != null)
                tiles[y, x].GetComponent<Tile>().delete();
            
            tiles[y, x] = Instantiate(Resources.Load<Transform>("Meshes/tileColonyPrefab"), new Vector3(x*tileSize, 0, y*tileSize), new Quaternion(0,0,0,1));
            tiles[y, x].GetComponent<Tile>().initialize(new Vector2(x, y));
            TurnSystem.players[player].GetComponent<Player>().colonies.Add(tiles[y, x].GetComponent<Tile>());
            Unit.unitPositions[y, x] = true;
            return true;
        }
        return false;
    }
}