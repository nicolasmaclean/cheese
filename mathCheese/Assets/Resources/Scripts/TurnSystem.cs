using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnSystem : MonoBehaviour
{
    public static List<Transform> players;
    public static int currentPlayer;
    public Text currentPlayerText;
    public Text currentLarvaeText;

    void Start()
    {
        #if UNITY_EDITOR
        if(players == null) // initializes stuff if not done by main menu
            devStart();
        #endif
        if(players.Count > 0)
            currentPlayer = 0;
        updateText();
    }

    public void nextTurn()
    {
        players[currentPlayer].GetComponent<Player>().updateLarvae();
        currentPlayer++;
        if(currentPlayer >= players.Count)
            currentPlayer = 0;
        updateText();
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
