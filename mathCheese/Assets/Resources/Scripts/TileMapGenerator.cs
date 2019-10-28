using UnityEngine;

public class TileMapGenerator : MonoBehaviour
{
    public int mapWidth, mapHeight;
    public Transform[] tilePrefabs;
    public Transform tileBorderPrefab;
    public static float tileSize;

    private Tile[,] tiles;

    void Start()
    {
        tiles = new Tile[mapHeight, mapWidth];
        tileSize = tilePrefabs[0].transform.Find("Ground").GetComponent<Renderer>().bounds.size.x;

        buildMap();
    }

    public void buildMap()
    {
        Quaternion up = new Quaternion(0,0,0,1);
        int[,] noiseMap = Noise.GenerateNoiseMap(mapWidth, mapHeight, tilePrefabs.Length);
        
        for(int z = 0; z < mapHeight; z++){
            for(int x = 0; x < mapWidth; x++){
                tiles[z, x] = new Tile(tilePrefabs[noiseMap[z, x]], tileBorderPrefab, new Vector3(x*tileSize, 0, z*tileSize), up, gameObject.transform, true);
            }
        }

    }

    public void clickSystem()
    {
        if(MouseOver.lastClicked.Count >= 2) {
            GameObject cl0 = MouseOver.lastClicked[MouseOver.lastClicked.Count-2];
            GameObject cl1 = MouseOver.lastClicked[MouseOver.lastClicked.Count-1];
            if(cl0 != null && cl1 != null) {
                if(cl0.GetComponent<MouseOver>().goType == MouseOver.GameObjectType.Unit && cl1.GetComponent<MouseOver>().goType == MouseOver.GameObjectType.Tile){
                    cl0.transform.parent.GetComponent<Unit>().move(cl1.transform.position);
                    MouseOver.lastClicked = new System.Collections.Generic.List<GameObject>();
                    MouseOver.lastClicked.Add(null);
                }
            }
        }
    }

    void Update() 
    {
        clickSystem();
    }
}