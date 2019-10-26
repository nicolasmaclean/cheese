using UnityEngine;

public class TileMapGenerator : MonoBehaviour
{
    public int mapWidth, mapHeight;
    public Transform[] tilePrefabs;
    public Transform tileBorderPrefab;

    private Transform[,] tiles;
    private Transform tileHoverBorder;
    private Renderer tileHoverBorderRenderer;
    private Transform tileClickBorder;
    private Renderer tileClickBorderRenderer;
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
        tileHoverBorderRenderer.enabled = true; //this ain't working
        tileHoverBorder.position = go.transform.position;
    }

    public void notHover(GameObject go)
    {
        go.GetComponent<Renderer>().material.color = Color.blue;
        // tileHoverBorderRenderer.enabled = false;
    }

    public void buildMap()
    {
        int z, x;
        Quaternion up = new Quaternion(-1,0,0,1);
        int[,] noiseMap = Noise.GenerateNoiseMap(mapWidth, mapHeight, tilePrefabs.Length);

        //makes all the tiles
        for(z = 0; z < mapHeight; z++){
            for(x = 0; x < mapWidth; x++){
                //instantiates a random prefab
                tiles[z, x] = Instantiate(tilePrefabs[noiseMap[z, x]], new Vector3(x*tileSize, 0, z*tileSize), up);
                tiles[z, x].parent = gameObject.transform;
                //adds a mouse over compon
                tiles[z, x].gameObject.AddComponent<MouseOver>();
                tiles[z, x].gameObject.GetComponent<MouseOver>().instantiate(onHover, notHover);
            }
        }

        tileHoverBorder = Instantiate(tileBorderPrefab, new Vector3(0, 0, 0), up);
        tileHoverBorderRenderer = tileHoverBorder.gameObject.GetComponent<Renderer>();
        tileHoverBorderRenderer.enabled = false;
        
        tileClickBorder = Instantiate(tileBorderPrefab, new Vector3(0, 0, 0), up);
        tileClickBorderRenderer = tileClickBorder.gameObject.GetComponent<Renderer>();
        tileClickBorderRenderer.enabled = false;
    }
}
