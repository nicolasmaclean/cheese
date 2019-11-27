using UnityEngine;
using System.Collections.Generic;

public class ClickSystem : MonoBehaviour
{
    public static List<GameObject> clickHistory;
    public Transform tempUnitTransform;
    public enum ClickState {none, hover, click};

    public static RaycastHit hitInfo;
    void Start()
    {
        clickHistory = new List<GameObject>();
        clickHistory.Add(null);
    }

    //checks the the last 2 clicked items and moves the unit if applicable
    void checkClickMoveUnit()
    {
        while(clickHistory.Count > 2){
            clickHistory.RemoveAt(0);
        }

        if(clickHistory.Count >= 2) {
            GameObject cl0 = clickHistory[clickHistory.Count-2];
            GameObject cl1 = clickHistory[clickHistory.Count-1];
            if(cl0 != null && cl1 != null) {
                if(cl0.GetComponent<Unit>() != null && cl1.GetComponent<Tile>() != null && cl0.transform.parent == TurnSystem.players[TurnSystem.currentPlayer]){
                    cl0.GetComponent<Unit>().move(cl1.GetComponent<Tile>().gridPosition);
                    clickHistory = new System.Collections.Generic.List<GameObject>();
                    clickHistory.Add(null);
                }
            }
        }
    }

    void checkUnitAttack()
    {
        if(clickHistory.Count >= 2){
            GameObject cl0 = clickHistory[clickHistory.Count-2];
            GameObject cl1 = clickHistory[clickHistory.Count-1];
            if(cl0 != null && cl1 != null){
                if(cl0.GetComponent<Unit>() != null && cl0.GetComponent<Unit>() != null && cl0.transform.parent != cl1.transform.parent && cl0.transform.parent == TurnSystem.players[TurnSystem.currentPlayer]){
                    cl1.GetComponent<Unit>().takeDamage(cl0.GetComponent<Unit>().damage);
                }
            }
        }
    }

    void addUnit()
    {
        GameObject lastClicked = clickHistory[clickHistory.Count-1];
        if(lastClicked != null && lastClicked.GetComponent<Tile>() && Input.GetKeyDown("p")){
            // lastClicked.gameObject.transform.parent.gameObject.GetComponent<Tile>().updated = false;
            TurnSystem.players[0].gameObject.GetComponent<Player>().addUnit(Species.getRandomUnitTransform(), lastClicked.GetComponent<Tile>().gridPosition, new Quaternion(-1,0,0,1));
        }
    }

    void Update()
    {
        addUnit();
        checkClickMoveUnit();
        checkUnitAttack();

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        Physics.Raycast(ray, out hitInfo, 100);
    }
}