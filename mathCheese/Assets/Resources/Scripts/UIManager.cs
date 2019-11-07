using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    
    public void quitQame() {
        Application.Quit();
    }

    public void prepareGame() {
        TurnSystem.players = new List<Transform>();
        GameObject player = new GameObject();
        player.name = "playerEmpty";
        player.AddComponent<Player>();
        TurnSystem.players.Add(player.transform);
        DontDestroyOnLoad(player);
    }

    public void playGame() {
        SceneManager.LoadScene("Game");
        prepareGame();
    }
}
