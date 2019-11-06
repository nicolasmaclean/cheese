using System.Collections.Generic;
using UnityEngine;

public class TurnSystem : MonoBehaviour
{
    public static List<Player> players = new List<Player>();
    public static Player currentPlayer;

    void Start()
    {
        if(players.Count > 0)
            currentPlayer = players[0];
    }
}
