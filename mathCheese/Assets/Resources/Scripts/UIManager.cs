using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    
    public GameObject Main;
    public GameObject Options;

    public Slider UISlider;
    public Dropdown UIDropdown;

    public InputField name1;
    public  InputField name2;
    public  InputField name3;
    public  InputField name4;

    public InputField[] playerNames;
    public  GameObject Player1;
    public  GameObject Player2;
    public  GameObject Player3;
    public  GameObject Player4;

    public  float players = 1;
    public Text text;
    public  int size = 0;

    public void quitQame()
    {
        Application.Quit();
    }

    public void prepareGame()
    {
        TurnSystem.players = new List<Transform>();
        for(int i = 0; i < players; i++) {
            GameObject player = new GameObject();
            player.name = playerNames[i].text;
            player.AddComponent<Player>();
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
    }

    public void slide()
    {
        players = UISlider.value;
        text.text = "" + UISlider.value;
    }

    public void drop()
    {
        size = UIDropdown.value;
    }

    public void play2()
    {
        Options.SetActive(false);
        GameObject[] Players = {Player1, Player2, Player3, Player4};
        playerNames = new InputField[] {name1, name2, name3, name4};
        for(int i = 0; i < players; i++)
        {
            Players[i].SetActive(true);
        }
    }

    public static IEnumerator timedDisappear(GameObject go, int time)
    {
        yield return new WaitForSeconds(time);

        go.SetActive(false);
    }
}
