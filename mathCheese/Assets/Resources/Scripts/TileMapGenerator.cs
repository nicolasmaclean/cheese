using UnityEngine;

public class TileMapGenerator : MonoBehaviour
{
    public static Tile[,] tiles;
    public static int mapWidth = 25, mapHeight = 25;

    public Transform[] tilePrefabs;
    public float[] tileRates;
    public Transform tileColonyPrefab;
    public static float tileSize;
    public bool autoUpdate;

    void Start()
    {
        for(int i = gameObject.transform.childCount-1; i > -1; i--){
            DestroyImmediate(gameObject.transform.GetChild(i).gameObject);
        }
        buildMap();
    }

    static Tile createTile(int z, int x, Quaternion q, Transform t)
    {
        Tile tile = Instantiate(t, new Vector3(x*tileSize, 0, z*tileSize), q).GetComponent<Tile>();
        tile.initialize(new Vector2(x, z));
        return tile;
    }

    int getConstrainedRandom(float x)
    {
        int num = -1;

        for(int i = 0; i < tileRates.GetLength(0); i++)
            if(x < tileRates[i]) {
                num = i;
                break;
            }
        if(num == -1)
            Debug.Log("tile rates is empty");
        return num;
    }

    public void buildMap()
    {
        if(mapHeight > 0 && mapWidth > 0){
            tiles = new Tile[mapHeight, mapWidth];
            tileSize = tilePrefabs[0].transform.Find("Ground").GetComponent<Renderer>().bounds.size.x;
            Unit.unitPositions = new bool[mapHeight, mapWidth];

            Quaternion up = new Quaternion(0,0,0,1);
            float[,] noiseMap = Noise.GenerateNoiseMap(mapWidth, mapHeight, tilePrefabs.Length);
            
            for(int z = 0; z < mapHeight; z++) {
                for(int x = 0; x < mapWidth; x++) {
                    tiles[z, x] = createTile(z, x, up, tilePrefabs[getConstrainedRandom(noiseMap[z, x])]);
                    tiles[z, x].transform.parent = gameObject.transform;
                }
            }
        } else {
            Debug.Log("map size <= 0");
        }
    }

    public static bool createColony(int y, int x, int player)
    {
        if(tiles[y, x].GetComponent<TileColony>() == null) { // if its not already a colony
            if(tiles[y, x] != null)
                tiles[y, x].delete();
            
            tiles[y, x] = createTile(y, x, new Quaternion(0, 0, 0, 1), Resources.Load<Transform>("Meshes/tileColonyPrefab"));
            TurnSystem.players[player].GetComponent<Player>().colonies.Add(tiles[y, x].GetComponent<TileColony>());
            Unit.unitPositions[y, x] = true;
            return true;
        }
        return false;
    }
}