using UnityEngine;
using System.Collections.Generic;
public class UIGameManager : MonoBehaviour
{
    public GameObject canvasPublic;
    static GameObject canvas;

    void OnEnable() 
    {
        Debug.Log("hello");
        canvas = canvasPublic;
    }
    public static void openMenu() 
    {
        canvas.transform.Find("Colony").gameObject.SetActive(true);
    }

    public static void closeMenu()
    {
        canvas.transform.Find("Colony").gameObject.SetActive(false);
    }

    public void buildAnt() 
    {
        if(TurnSystem.players[TurnSystem.currentPlayer].GetComponent<Player>().larvae > 5)
        {
            Tile c = ClickSystem.clickHistory[ClickSystem.clickHistory.Count-1].GetComponent<Tile>();
            List<Tile> tiles = c.getAdjacentTiles();
            foreach (Tile t in tiles)
            {
                if(!Unit.isTileFilled(t.gridPosition))
                {
                    TurnSystem.players[TurnSystem.currentPlayer].GetComponent<Player>().addUnit(Species.unitType[0],t.gridPosition,new Quaternion(-1,0,0,1));
                    TurnSystem.players[TurnSystem.currentPlayer].GetComponent<Player>().larvae -= 5;
                }
            }
        }
    }

    public void buildSoldier() 
    {
        if(TurnSystem.players[TurnSystem.currentPlayer].GetComponent<Player>().larvae > 50 && TurnSystem.players[TurnSystem.currentPlayer].GetComponent<Player>().food > 20 && TurnSystem.players[TurnSystem.currentPlayer].GetComponent<Player>().water > 10)
        {
            Tile c = ClickSystem.clickHistory[ClickSystem.clickHistory.Count-1].GetComponent<Tile>();
            List<Tile> tiles = c.getAdjacentTiles();
            foreach (Tile t in tiles)
            {
                if(!Unit.isTileFilled(t.gridPosition))
                {
                    TurnSystem.players[TurnSystem.currentPlayer].GetComponent<Player>().addUnit(Species.unitType[1],t.gridPosition,new Quaternion(-1,0,0,1));
                    TurnSystem.players[TurnSystem.currentPlayer].GetComponent<Player>().larvae -= 50;
                    TurnSystem.players[TurnSystem.currentPlayer].GetComponent<Player>().food -= 20;
                    TurnSystem.players[TurnSystem.currentPlayer].GetComponent<Player>().water -= 10;
                }
            }
        }
    }

}
