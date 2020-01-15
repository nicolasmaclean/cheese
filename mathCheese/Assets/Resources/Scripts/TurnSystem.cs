using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnSystem : MonoBehaviour
{
    public static List<Transform> players;
    public static int currentPlayer;
    public Text currentPlayerText;
    public Text currentLarvaeText;

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

        if(players.Count > 0)
            currentPlayer = 0;
            
        updateText();

        createInitialColonies();
    }

    public void nextTurn()
    {
        players[currentPlayer].GetComponent<Player>().updateLarvae();
        currentPlayer++;

        if(currentPlayer >= players.Count)
            currentPlayer = 0;
        updateText();
        Camera.main.GetComponent<CameraMovement>().moveToColony();
        if(ClickSystem.clickHistory[ClickSystem.clickHistory.Count-1].GetComponent<Unit>())
            ClickSystem.clickHistory[ClickSystem.clickHistory.Count-1].GetComponent<Unit>().moveTilesReset();
    }

    public void updateText()
    {
        currentPlayerText.text = players[currentPlayer].name;
        currentLarvaeText.text = "" + players[currentPlayer].GetComponent<Player>().larvae;
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
