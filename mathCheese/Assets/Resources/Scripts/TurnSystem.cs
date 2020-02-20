using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnSystem : MonoBehaviour
{
    public static List<Transform> players;
    public static int currentPlayer;
    public Text currentPlayerText;
    public Text currentLarvaeText;
    public Text currentFoodText;
    public Text currentWaterText;
    public Text currentGoldText;

    void createInitialColonies()
    {
        int tempWidth = TileMapGenerator.tiles.GetLength(1);
        int tempHeight = TileMapGenerator.tiles.GetLength(0);

        if(ClickSystem.clickHistory == null) {
            ClickSystem.clickHistory = new List<GameObject>();
        }

        for(int i = 0; i < TurnSystem.players.Count; i++) { // gives each player a colony to start
            int x = 0; int y = 0;
            switch(i) {
                case 0 : x = Random.Range(0, tempWidth/2); y = Random.Range(0, tempHeight/2); break;
                case 1 : x = Random.Range(tempWidth/2, tempWidth); y = Random.Range(tempHeight/2, tempHeight); break;
                case 2 : x = Random.Range(tempWidth/2, tempWidth); y = Random.Range(0, tempHeight/2); break;
                case 3 : x = Random.Range(0, tempWidth/2); y = Random.Range(tempHeight/2, tempHeight); break;
            }

            TileMapGenerator.createColony(y, x, i);
        }
    }

    void Start()
    {
        #if UNITY_EDITOR
        if(players == null) // initializes stuff if not done by main menu
            devStart();
        #endif

        currentPlayer = 0;
        createInitialColonies();
        players[currentPlayer].GetComponent<Player>().updateLarvae();
            
        updateText();

        Camera.main.GetComponent<CameraMovement>().moveToColony();
    }

    public void nextTurn()
    {
        currentPlayer++;
        if(currentPlayer >= players.Count)
            currentPlayer = 0;

        players[currentPlayer].GetComponent<Player>().updateLarvae();
        foreach(Transform h in players[currentPlayer].GetComponent<Player>().units) {
            if(h.GetComponent<UnitHarvester>())
                players[currentPlayer].GetComponent<Player>().updateResources(h.GetComponent<UnitHarvester>().harvest());
        }

        Camera.main.GetComponent<CameraMovement>().moveToColony();

        updateText();
        if(ClickSystem.clickHistory.Count > 0 && ClickSystem.clickHistory[ClickSystem.clickHistory.Count-1].GetComponent<Unit>())
            ClickSystem.clickHistory[ClickSystem.clickHistory.Count-1].GetComponent<Unit>().moveTilesReset();
    }

    public void updateText()
    {
        currentPlayerText.text = players[currentPlayer].name;
        currentLarvaeText.text = "" + players[currentPlayer].GetComponent<Player>().larvae;
        currentFoodText.text = "" + players[currentPlayer].GetComponent<Player>().food;
        currentWaterText.text = "" + players[currentPlayer].GetComponent<Player>().water;
        currentGoldText.text = "" + players[currentPlayer].GetComponent<Player>().gold;
    }

    public void reset()
    {
        currentPlayer = 0;
    }

    #if UNITY_EDITOR
    void devStart()
    {
        TurnSystem.players = new List<Transform>();
        GameObject player1 = new GameObject();
        player1.name = "dev1";
        player1.AddComponent<Player>();
        players.Add(player1.transform);
        DontDestroyOnLoad(player1);

        GameObject player2 = new GameObject();
        player2.name = "dev2";
        player2.AddComponent<Player>();
        players.Add(player2.transform);
        DontDestroyOnLoad(player2);
    }
    #endif
}
