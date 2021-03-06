﻿using System.Collections.Generic;
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
    public GameObject mapSize;
    public GameObject playerAmt;
    public GameObject playerCanvas;
    public Material[] teamMaterials;

    private float players = 2;
    private int size = 0;
    private bool pressed = false;

    public void quitQame()
    {
        Application.Quit();
    }

    public void prepareGame()
    {
        TurnSystem.players = new List<Transform>();

        pickRandomNames();

        playerAmtSet(0);
        mapSizeSet(0);
    }

    public void pickRandomNames()
    {
        string path = "names";
        TextAsset namesText = Resources.Load<TextAsset>(path);
        string[] namesArr = namesText.text.Split('*');
        List<string> names = new List<string>(namesArr);
        for(int i = 0; i < players; i++) {
            GameObject player = new GameObject();
            int r = Random.Range(0, names.Count);
            player.name = names[r]; // make a player prefab that this can instantiate from\
            names.RemoveAt(r);
            player.AddComponent<Player>();
            if(i < teamMaterials.GetLength(0)) {
                player.GetComponent<Player>().teamMaterial = teamMaterials[i];
            } else {
                Debug.Log("get some juicy team materials in teh UI Manager!");
            }
            TurnSystem.players.Add(player.transform);
            DontDestroyOnLoad(player);
        }
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
        if(i == 1)
            players++;
        if(i == -1)
            players--;

        if(players > 4)
            players = 4;
        else if (players < 2)
            players = 2;
            
        playerAmt.GetComponent<Text>().text = "" + players;
    }

    public void mapSizeSet(int i)
    {
        if(i == 1)
            size++;
        if(i == -1)
            size--;

        if(size > 2)
            size = 2;
        else if (size < 0)
            size = 0;
        
        switch(size) {
            case 0 : mapSize.GetComponent<Text>().text = "small"; break;
            case 1 : mapSize.GetComponent<Text>().text = "medium"; break;
            case 2 : mapSize.GetComponent<Text>().text = "large"; break;
        }
    }

    void OnGUI() {
        Event e = Event.current;
        GameObject go = EventSystem.current.currentSelectedGameObject;

        if(go != null && playerCanvas.activeInHierarchy) {
            if(!pressed && Input.GetAxisRaw("Horizontal") == -1) {
                if(go.Equals(playerAmt)) {
                    playerAmtSet(-1);
                } else if(go.Equals(mapSize)) {
                    mapSizeSet(-1);
                }
                pressed = true;
            } else if (!pressed && Input.GetAxisRaw("Horizontal") == 1) {
                if(go.Equals(playerAmt)) {
                    playerAmtSet(1);
                } else if(go.Equals(mapSize)) {
                    mapSizeSet(1);
                }
                pressed = true;
            }
            if(Input.GetAxis("Horizontal") == 0)
                pressed = false;
        }
    }

    public static IEnumerator timedDisappear(GameObject go, int time)
    {
        yield return new WaitForSeconds(time);

        go.SetActive(false);
    }
}
