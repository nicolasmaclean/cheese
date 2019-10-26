using UnityEngine;

public class TileMapGenerator : MonoBehaviour
{
    public int mapWidth, mapHeight;
    public Transform[] tilePrefabs;
    public Transform tileBorderPrefab;

    // Material[] dirt;
    Material dirt;

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

        // dirt = new Material[tilePrefabs.Length];
        // for(int x = 0; x < tilePrefabs.Length; x++){
        //     dirt[x] = tilePrefabs[x].gameObject.GetComponent<Renderer>().material;
        // }
        dirt = tilePrefabs[0].gameObject.GetComponent<Renderer>().sharedMaterial;

        buildMap();
    }

    public void onHover(GameObject go)
    {
        go.GetComponent<Renderer>().material.color = Color.green;
        tileHoverBorderRenderer.enabled = true;
        tileHoverBorder.position = go.transform.position;
    }

    public void notHover(GameObject go)
    {
        go.GetComponent<Renderer>().sharedMaterial = dirt;
        // tileHoverBorderRenderer.enabled = false;
    }

    public void onClick(GameObject go)
    {
        go.GetComponent<Renderer>().material.color = Color.red;
        tileClickBorderRenderer.enabled = true;
        tileClickBorder.position = go.transform.position;
    }

    public void buildMap()
    {
        int z, x;
        Quaternion up = new Quaternion(-1,0,0,1);
        int[,] noiseMap = Noise.GenerateNoiseMap(mapWidth, mapHeight, tilePrefabs.Length);

        //makes the tile borders
        tileHoverBorder = Instantiate(tileBorderPrefab, new Vector3(0, 0, 0), up);
        tileHoverBorderRenderer = tileHoverBorder.gameObject.GetComponent<Renderer>();
        tileHoverBorderRenderer.enabled = false;
        tileHoverBorder.parent = gameObject.transform;
        
        tileClickBorder = Instantiate(tileBorderPrefab, new Vector3(0, 0, 0), up);
        tileClickBorderRenderer = tileClickBorder.gameObject.GetComponent<Renderer>();
        tileClickBorderRenderer.enabled = false;
        tileClickBorder.parent = gameObject.transform;

        //makes all the tiles
        for(z = 0; z < mapHeight; z++){
            for(x = 0; x < mapWidth; x++){
                //instantiates a random prefab
                tiles[z, x] = Instantiate(tilePrefabs[noiseMap[z, x]], new Vector3(x*tileSize, 0, z*tileSize), up);
                tiles[z, x].parent = gameObject.transform;
                //adds a mouse over component
                tiles[z, x].gameObject.AddComponent<MouseOver>();
                tiles[z, x].gameObject.GetComponent<MouseOver>().instantiate(onHover, notHover, onClick);
            }
        }

    }
}