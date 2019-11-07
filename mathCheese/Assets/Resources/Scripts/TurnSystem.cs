using System.Collections.Generic;
using UnityEngine;

public class TurnSystem : MonoBehaviour
{
    public static List<Transform> players;
    public static int currentPlayer;

    void Start()
    {
        if(players.Count > 0)
            currentPlayer = 0;
    }
}
