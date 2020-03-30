using UnityEngine;
using System.Collections.Generic;
public class UIGameManager : MonoBehaviour
{
    public GameObject canvasPublic;
    static GameObject canvas;

    public static bool assign;

    void OnEnable() 
    {
        Debug.Log("hello");
        canvas = canvasPublic;
    }
    public static void openMenu(string type) 
    {
        canvas.transform.Find(type).gameObject.SetActive(true);
    }

    public static void closeMenu()
    {
        canvas.transform.Find("Colony").gameObject.SetActive(false);
        canvas.transform.Find("Harvester").gameObject.SetActive(false);
    }

    public void buildAnt() 
    {
        bool placed = false;
        if(TurnSystem.players[TurnSystem.currentPlayer].GetComponent<Player>().larvae > 5)
        {
            Tile c = ClickSystem.clickHistory[ClickSystem.clickHistory.Count-1].GetComponent<Tile>();
            List<Tile> tiles = c.getAdjacentTiles();
            foreach (Tile t in tiles)
            {
                if(!Unit.isTileFilled(t.gridPosition) && !placed)
                {
                    TurnSystem.players[TurnSystem.currentPlayer].GetComponent<Player>().addUnit(Species.unitType[0],t.gridPosition,new Quaternion(-1,0,0,1));
                    TurnSystem.players[TurnSystem.currentPlayer].GetComponent<Player>().larvae -= 5;
                    placed = true;
                }
            }
        }
    }

    public void buildSoldier() 
    {
        bool placed = false;
        if(TurnSystem.players[TurnSystem.currentPlayer].GetComponent<Player>().larvae > 50 && TurnSystem.players[TurnSystem.currentPlayer].GetComponent<Player>().food > 20 && TurnSystem.players[TurnSystem.currentPlayer].GetComponent<Player>().water > 10)
        {
            Tile c = ClickSystem.clickHistory[ClickSystem.clickHistory.Count-1].GetComponent<Tile>();
            List<Tile> tiles = c.getAdjacentTiles();
            foreach (Tile t in tiles)
            {
                if(!Unit.isTileFilled(t.gridPosition) && !placed)
                {
                    TurnSystem.players[TurnSystem.currentPlayer].GetComponent<Player>().addUnit(Species.unitType[1],t.gridPosition,new Quaternion(-1,0,0,1));
                    TurnSystem.players[TurnSystem.currentPlayer].GetComponent<Player>().larvae -= 50;
                    TurnSystem.players[TurnSystem.currentPlayer].GetComponent<Player>().food -= 20;
                    TurnSystem.players[TurnSystem.currentPlayer].GetComponent<Player>().water -= 10;
                    placed = true;
                }
            }
        }
    }
    public void buildQueen() 
    {
        bool placed = false;
        if(TurnSystem.players[TurnSystem.currentPlayer].GetComponent<Player>().larvae > 50 && TurnSystem.players[TurnSystem.currentPlayer].GetComponent<Player>().food > 100 && TurnSystem.players[TurnSystem.currentPlayer].GetComponent<Player>().water > 100 && TurnSystem.players[TurnSystem.currentPlayer].GetComponent<Player>().gold > 20)
        {
            Tile c = ClickSystem.clickHistory[ClickSystem.clickHistory.Count-1].GetComponent<Tile>();
            List<Tile> tiles = c.getAdjacentTiles();
            foreach (Tile t in tiles)
            {
                if(!Unit.isTileFilled(t.gridPosition) && !placed)
                {
                    TurnSystem.players[TurnSystem.currentPlayer].GetComponent<Player>().addUnit(Species.unitType[2],t.gridPosition,new Quaternion(-1,0,0,1));
                    TurnSystem.players[TurnSystem.currentPlayer].GetComponent<Player>().larvae -= 50;
                    TurnSystem.players[TurnSystem.currentPlayer].GetComponent<Player>().food -= 100;
                    TurnSystem.players[TurnSystem.currentPlayer].GetComponent<Player>().water -= 100;
                    TurnSystem.players[TurnSystem.currentPlayer].GetComponent<Player>().gold -= 20;
                    placed = true;
                }
            }
        }
    }

    public void assignAnt()
    {
        assign = true;
        ClickSystem.clickHistory[ClickSystem.clickHistory.Count-1].GetComponent<Unit>().moveTilesReset();
    }
    public void removeAssigned()
    {
        ClickSystem.clickHistory[ClickSystem.clickHistory.Count-1].GetComponent<UnitHarvester>().assignedTile = null;
    }
}
