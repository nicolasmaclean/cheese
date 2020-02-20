using UnityEngine;

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
        canvas.transform.Find("Colony").gameObject.SetActive(!canvas.transform.Find("Colony").gameObject.activeSelf);
    }

    public void buildAnt() 
    {
        if(TurnSystem.players[TurnSystem.currentPlayer].GetComponent<Player>().larvae > 5)
            //ant
            ;
    }
}
