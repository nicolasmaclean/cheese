using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ClickSystem : MonoBehaviour
{
    public static List<GameObject> clickHistory;
    public static RaycastHit hitInfo;
    public Transform tempUnitTransform;
    public enum ClickState {none, hover, click};
    public GameObject infoPanelPublic;
    public GameObject messagePanelPublic;
    public Text activeSelectionTextPublic;
    public Text healthHeaderPublic;
    public Text healthTextPublic;

    static RectTransform infoPanel;
    static GameObject messagePanel;
    static Text activeSelectionText;
    static Text healthHeader;
    static Text healthText;
    GameObject lastClicked;
    static ClickSystem clickSystemRef;

    void Start()
    {
        clickHistory = new List<GameObject>();
        clickSystemRef = this;

        infoPanel = infoPanelPublic.GetComponent<RectTransform>();
        messagePanel = messagePanelPublic;
        activeSelectionText = activeSelectionTextPublic;
        healthHeader = healthHeaderPublic;
        healthText = healthTextPublic;
    }

    //checks the the last 2 clicked items and moves the unit if applicable
    public static void checkClickMoveUnit()
    {
        if(clickHistory.Count >= 2) {
            GameObject cl0 = clickHistory[clickHistory.Count-2];
            GameObject cl1 = clickHistory[clickHistory.Count-1];
            if(cl0.GetComponent<Unit>() != null && cl1.GetComponent<Tile>() != null){
                if(cl0.transform.parent == TurnSystem.players[TurnSystem.currentPlayer].gameObject.transform) {
                    cl0.GetComponent<Unit>().move(cl1.GetComponent<Tile>().gridPosition);
                } else {
                    if(!messagePanel.activeInHierarchy) {
                        messagePanel.SetActive(true);
                        clickSystemRef.disappearCoroutine(3);
                    }
                }
            }
        }
    }

    void disappearCoroutine(int time)
    {
        StartCoroutine(UIManager.timedDisappear(messagePanel, time));
    }

    public static void checkUnitAttack()
    {
        if(clickHistory.Count >= 2){
            GameObject cl0 = clickHistory[clickHistory.Count-2];
            GameObject cl1 = clickHistory[clickHistory.Count-1];
            if(cl0 != null && cl1 != null){ // add another if the tile under an enemy unit is selected
                if(cl0.GetComponent<Unit>() != null && cl1.GetComponent<Unit>() != null && cl0.transform.parent != cl1.transform.parent && cl0.transform.parent == TurnSystem.players[TurnSystem.currentPlayer]){
                    Vector2 tPos = cl1.GetComponent<Unit>().gridPosition;
                    if(cl1.GetComponent<Unit>().takeDamage(cl0.GetComponent<Unit>().damage)){
                        cl0.GetComponent<Unit>().levelUp();
                        cl0.GetComponent<Unit>().move(tPos);
                    }
                }
            }
        }
    }

    public void addUnit()
    {
        if(clickHistory.Count > 0) {
            lastClicked = clickHistory[clickHistory.Count-1];
            if(lastClicked != null && lastClicked.GetComponent<Tile>())
            TurnSystem.players[TurnSystem.currentPlayer].gameObject.GetComponent<Player>().addUnit(Species.getRandomUnitTransform(), lastClicked.GetComponent<Tile>().gridPosition, new Quaternion(-1,0,0,1));
        }
    }

    public void buildColony()
    {
        if(clickHistory.Count > 0) {
            lastClicked = clickHistory[clickHistory.Count-1];
            if(lastClicked != null && lastClicked.GetComponent<UnitQueen>())
                lastClicked.GetComponent<UnitQueen>().makeColony();
        }
    }

    public static void updateSelectionText(GameObject go)
    {
        activeSelectionText.text = go.GetComponent<Entity>().entityName;
        if(go.GetComponent<Unit>() != null){
            infoPanel.sizeDelta = new Vector2(infoPanel.sizeDelta.x, (float)182);
            healthHeader.enabled = true;
            healthText.enabled = true;
            healthText.text = "" + go.GetComponent<Unit>().health;
        } else {
            infoPanel.sizeDelta = new Vector2(infoPanel.sizeDelta.x, (float)141);
            healthHeader.enabled = false;
            healthText.enabled = false;
        }
    }

    void Update()
    {
        if(!UIPauseManager.paused){
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Physics.Raycast(ray, out hitInfo, 200);
        }
    }
}