using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIPauseManager : MonoBehaviour
{
    public GameObject canvas;
    public static bool paused;

    void Start()
    {
        canvas.SetActive(false);
        paused = false;    
    }

    void Update()
    {
        if(Input.GetKeyDown("escape")){
            canvas.SetActive(!canvas.activeInHierarchy);
            paused = !paused;
        }
    }

    public void continueGame()
    {
        canvas.SetActive(false);
        paused = false;
    }

    public void openOptions()
    {
        Debug.Log("nope");
    }

    public void restart()
    {
        // List<Transform> players = TurnSystem.players;
        // foreach(Transform player in players){
        //     player.gameObject.GetComponent<Player>().reset();
        // }

        // ClickSystem.clickHistory = new List<GameObject>();
        // Unit.unitPositions = new bool[TileMapGenerator.mapHeight, TileMapGenerator.mapWidth];

        // //update all tiles
        // Transform[,] tiles = TileMapGenerator.tiles;
        // for(int i = 0; i < tiles.GetLength(0); i++){
        //     for(int j = 0; j < tiles.GetLength(1); j++){
        //         tiles[i, j].GetComponent<Tile>().reset();
        //     }
        // }
        
        continueGame();
    }

    public void exitToMenu()
    {

    }

    public void exitToDesktop()
    {
        Application.Quit();
    }
}
