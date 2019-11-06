using System.Collections.Generic;
using UnityEngine;

public class TurnSystem : MonoBehaviour
{
    public static List<Player> players = new List<Player>();
    public static Player currentPlayer;
    public Transform tempUnit;

    void Start()
    {
        if(players.Count > 0)
            currentPlayer = players[0];
    }

    void Update()
    {
        if(Input.GetKeyDown("p")) //make the border on meshes apart of the prefab
            players[0].addUnit(new Unit(tempUnit, new Vector2(0, 0)));
    }
}
