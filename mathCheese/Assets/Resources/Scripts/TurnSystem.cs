using System.Collections.Generic;
using UnityEngine;

public class TurnSystem : MonoBehaviour
{
    public static List<Transform> players;
    public static int currentPlayer;

    void Start()
    {
        #if UNITY_EDITOR
        if(players == null) // initializes stuff if not done by main menu
            devStart();
        #endif
        if(players.Count > 0)
            currentPlayer = 0;
    }

    #if UNITY_EDITOR
    void devStart()
    {
        TurnSystem.players = new List<Transform>();
        GameObject player = new GameObject();
        player.name = "dev";
        player.AddComponent<Player>();
        players.Add(player.transform);
        DontDestroyOnLoad(player);
    }
    #endif
}
