using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    
    public GameObject Main;
    public GameObject Options;

    public void quitQame() {
        Application.Quit();
    }

    public void startGame() {
        SceneManager.LoadScene("Game");
    }

    public void play() {
        Main.gameObject.SetActive(false);
        Options.gameObject.SetActive(true);
    }
}
