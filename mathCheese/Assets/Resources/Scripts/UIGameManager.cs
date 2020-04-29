using UnityEngine;
using System.Collections.Generic;
public class UIGameManager : MonoBehaviour
{
    public GameObject canvasPublic;
    public TurnSystem turnSystem;
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
        canvas.transform.Find("UnitCreation").gameObject.SetActive(false);
        canvas.transform.Find("HarvesterAssignment").gameObject.SetActive(false);
    }

    public void buildAnt() 
    {
        Player currentPlayer = TurnSystem.players[TurnSystem.currentPlayer].GetComponent<Player>();
        bool placed = false;
        if(currentPlayer.larvae >= 5 || currentPlayer.unlimitedMoney)
        {
            Tile c = ClickSystem.clickHistory[ClickSystem.clickHistory.Count-1].GetComponent<Tile>();
            List<Tile> tiles = c.getAdjacentTiles();
            foreach (Tile t in tiles)
            {
                if(!Unit.isTileFilled(t.gridPosition) && !placed)
                {
                    currentPlayer.addUnit(Species.unitType[0],t.gridPosition,new Quaternion(-1,0,0,1));
                    if(!currentPlayer.unlimitedMoney) {
                        currentPlayer.larvae -= 5;
                    }
                    placed = true;
                }
            }
            turnSystem.updateText();
        }
    }

    public void buildSoldier() 
    {
        Player currentPlayer = TurnSystem.players[TurnSystem.currentPlayer].GetComponent<Player>();
        bool placed = false;
        if((currentPlayer.larvae >= 50 && currentPlayer.food >= 20 && currentPlayer.water >= 10) || currentPlayer.unlimitedMoney)
        {
            Tile c = ClickSystem.clickHistory[ClickSystem.clickHistory.Count-1].GetComponent<Tile>();
            List<Tile> tiles = c.getAdjacentTiles();
            foreach (Tile t in tiles)
            {
                if(!Unit.isTileFilled(t.gridPosition) && !placed)
                {
                    currentPlayer.addUnit(Species.unitType[2],t.gridPosition,new Quaternion(-1,0,0,1));
                    if(!currentPlayer.unlimitedMoney) {
                        currentPlayer.larvae -= 50;
                        currentPlayer.food -= 20;
                        currentPlayer.water -= 10;
                    }
                    placed = true;
                }
            }
            turnSystem.updateText();
        }
    }
    public void buildQueen() 
    {
        Player currentPlayer = TurnSystem.players[TurnSystem.currentPlayer].GetComponent<Player>();
        bool placed = false;
        if((currentPlayer.larvae >= 50 && currentPlayer.food >= 100 && currentPlayer.water >= 100 && currentPlayer.gold >= 20) || currentPlayer.unlimitedMoney)
        {
            Tile c = ClickSystem.clickHistory[ClickSystem.clickHistory.Count-1].GetComponent<Tile>();
            List<Tile> tiles = c.getAdjacentTiles();
            foreach (Tile t in tiles)
            {
                if(!Unit.isTileFilled(t.gridPosition) && !placed)
                {
                    currentPlayer.addUnit(Species.unitType[1],t.gridPosition,new Quaternion(-1,0,0,1));
                    if(!currentPlayer.unlimitedMoney) {
                        currentPlayer.larvae -= 50;
                        currentPlayer.food -= 100;
                        currentPlayer.water -= 100;
                        currentPlayer.gold -= 20;
                    }
                    placed = true;
                }
            }
            turnSystem.updateText();
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
