using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    
    public GameObject Main;
    public GameObject Options;

    public Slider UISlider;
    public Dropdown UIDropdown;

    public  float players = 1;
    public Text text;
    public  int size = 0;

    public void quitQame() {
        Application.Quit();
    }

    public void prepareGame() {
        TurnSystem.players = new List<Transform>();
        for(int i = 0; i < players; i++) {
            GameObject player = new GameObject();
            player.name = "playerEmpty";
            player.AddComponent<Player>();
            TurnSystem.players.Add(player.transform);
            DontDestroyOnLoad(player);
        }
    }

    public void startGame() {
        SceneManager.LoadScene("Game");
        TileMapGenerator.mapHeight = (int)(Mathf.Pow(2,size) * 25);
        TileMapGenerator.mapWidth = (int)(Mathf.Pow(2,size) * 25);
        prepareGame();
    }

    public void play() {
        Main.gameObject.SetActive(false);
        Options.gameObject.SetActive(true);
        
    }

    public void slide() {
        players = UISlider.value;
        text.text = ""+ UISlider.value;
    }

    public void drop() {
        size = UIDropdown.value;
    }
}
