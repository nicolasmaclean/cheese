using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIManager : MonoBehaviour
{
    public GameObject Main;
    public GameObject Options;
    public GameObject PlayButton1;
    public GameObject PlayButton2;
    public GameObject mapSize;
    public GameObject playerAmt;
    public InputField[] nameInput;
    public GameObject playerCanvas;

    private float players = 2;
    private int size = 0;

    public void quitQame()
    {
        Application.Quit();
    }

    public void prepareGame()
    {
        TurnSystem.players = new List<Transform>();
        for(int i = 0; i < players; i++) {
            GameObject player = new GameObject();
            player.name = nameInput[i].text; // make a player prefab that this can instantiate from
            player.AddComponent<Player>();
            TurnSystem.players.Add(player.transform);
            DontDestroyOnLoad(player);
        }
        playerAmtSet(2);
        mapSizeSet(0);
    }

    public void startGame()
    {
        SceneManager.LoadScene("Game");
        TileMapGenerator.mapHeight = (int)(Mathf.Pow(2,size) * 25);
        TileMapGenerator.mapWidth = (int)(Mathf.Pow(2,size) * 25);
        prepareGame();
    }

    public void play()
    {
        Main.gameObject.SetActive(false);
        Options.gameObject.SetActive(true);

        EventSystem.current.SetSelectedGameObject(PlayButton1);
    }

    public void playerAmtSet(int i)
    {
        players = i;
        if(players > 4)
            players = 4;
        else if (players < 2)
            players = 2;
            
        playerAmt.GetComponent<Text>().text = "" + players;
    }

    public void mapSizeSet(int i)
    {
        size = i;
        if(size > 3)
            size = 3;
        else if (size < 0)
            size = 0;

        switch(i) {
            case 0 : mapSize.GetComponent<Text>().text = "small"; break;
            case 1 : mapSize.GetComponent<Text>().text = "medium"; break;
            case 2 : mapSize.GetComponent<Text>().text = "large"; break;
        }
    }

    void OnGUI() {
        Event e = Event.current;
        GameObject go = EventSystem.current.currentSelectedGameObject;

        if(Event.current.isKey && go != null) {
            if(e.type == EventType.KeyDown && e.keyCode == KeyCode.LeftArrow) {
                if(go.Equals(playerAmt)) {
                    playerAmtSet((int)players-1);
                } else if(go.Equals(mapSize)) {
                    mapSizeSet(size-1);
                }
            } else if (e.type == EventType.KeyDown && e.keyCode == KeyCode.RightArrow) {
                if(go.Equals(playerAmt)) {
                    playerAmtSet((int)players+1);
                } else if(go.Equals(mapSize)) {
                    mapSizeSet(size+1);
                }
            }
        }
    }

    public void play2()
    {
        Options.SetActive(false);
        playerCanvas.SetActive(true); // for loop and set active each player/input field necessary
        for(int i = 0; i < players; i++) {
            nameInput[i].transform.parent.gameObject.SetActive(true);
        }

        EventSystem.current.SetSelectedGameObject(nameInput[0].gameObject); // also figure out to improve the event system navigation between input fields
    }

    public static IEnumerator timedDisappear(GameObject go, int time)
    {
        yield return new WaitForSeconds(time);

        go.SetActive(false);
    }
}
