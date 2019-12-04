using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class ClickSystem : MonoBehaviour
{
    public static List<GameObject> clickHistory;
    public static RaycastHit hitInfo;
    public Transform tempUnitTransform;
    public enum ClickState {none, hover, click};
    public GameObject UIPanelPublic;
    public Text activeSelectionTextPublic;
    public Text healthHeaderPublic;
    public Text healthTextPublic;

    static RectTransform UIPanel;
    static Text activeSelectionText;
    static Text healthHeader;
    static Text healthText;
    GameObject lastClicked;
    void Start()
    {
        clickHistory = new List<GameObject>();

        UIPanel = UIPanelPublic.GetComponent<RectTransform>();
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
                    Debug.Log("That's not your unit.");
                }
            }
        }
    }

    public static void checkUnitAttack()
    {
        if(clickHistory.Count >= 2){
            GameObject cl0 = clickHistory[clickHistory.Count-2];
            GameObject cl1 = clickHistory[clickHistory.Count-1];
            if(cl0 != null && cl1 != null){ // add another if the tile under an enemy unit is selected
                if(cl0.GetComponent<Unit>() != null && cl1.GetComponent<Unit>() != null && cl0.transform.parent != cl1.transform.parent && cl0.transform.parent == TurnSystem.players[TurnSystem.currentPlayer]){
                    cl1.GetComponent<Unit>().takeDamage(cl0.GetComponent<Unit>().damage);
                }
            }
        }
    }

    void addUnit()
    {
        if(clickHistory.Count > 0)
            lastClicked = clickHistory[clickHistory.Count-1];
        if(lastClicked != null && lastClicked.GetComponent<Tile>() && Input.GetKeyDown("p")){
            TurnSystem.players[TurnSystem.currentPlayer].gameObject.GetComponent<Player>().addUnit(Species.getRandomUnitTransform(), lastClicked.GetComponent<Tile>().gridPosition, new Quaternion(-1,0,0,1));
        }
    }

    public static void updateSelectionText(GameObject go)
    {
        activeSelectionText.text = go.GetComponent<Entity>().entityName;
        if(go.GetComponent<Unit>() != null){
            UIPanel.sizeDelta = new Vector2(UIPanel.sizeDelta.x, (float)144.2);
            healthHeader.enabled = true;
            healthText.enabled = true;
            healthText.text = "" + go.GetComponent<Unit>().health;
        } else {
            UIPanel.sizeDelta = new Vector2(UIPanel.sizeDelta.x, (float)100);
            healthHeader.enabled = false;
            healthText.enabled = false;
        }
    }

    void Update()
    {
        addUnit();

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        Physics.Raycast(ray, out hitInfo, 100);
    }
}