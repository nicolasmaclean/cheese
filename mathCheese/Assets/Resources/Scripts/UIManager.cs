using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    
    public void quitQame() {
        Application.Quit();
    }

    public void playGame() {
        SceneManager.LoadScene("Game");
    }
}
