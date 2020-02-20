using UnityEngine;

public class UIGameManager : MonoBehaviour
{
    public GameObject canvas;

    public void Update()
    {
        if(ClickSystem.clickHistory.Count > 0) {
            GameObject clicked = ClickSystem.clickHistory[ClickSystem.clickHistory.Count-1];
            if(clicked.GetComponent<TileColony>())
            {
                canvas.transform.GetChild(4).gameObject.SetActive(true);
            }
            else 
            {
                for(int i = 4; i < canvas.transform.childCount; i++)
                    canvas.transform.GetChild(i).gameObject.SetActive(false);
            }
        }
    }

    public void buildAnt() {
        if(TurnSystem.players[TurnSystem.currentPlayer].GetComponent<Player>().larvae > 5)
            //ant
            ;
    }
}
