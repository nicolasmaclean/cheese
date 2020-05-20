using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ClickSystem : MonoBehaviour
{
    public static List<GameObject> clickHistory;
    public static RaycastHit hitInfo;
    public enum ClickState {none, hover, click};
    public GameObject infoPanelPublic;
    public GameObject messagePanelPublic;
    public GameObject messagePanelPublic2;
    public Text activeSelectionTextPublic;
    public Text healthHeaderPublic;

    static RectTransform infoPanel;
    static GameObject messagePanel;
    static GameObject messagePanel2;
    static Text activeSelectionText;
    static Text healthHeader;
    GameObject lastClicked;
    static ClickSystem clickSystemRef;

    void Start()
    {
        clickHistory = new List<GameObject>();
        clickSystemRef = this;

        infoPanel = infoPanelPublic.GetComponent<RectTransform>();
        messagePanel = messagePanelPublic;
        messagePanel2 = messagePanelPublic2;
        activeSelectionText = activeSelectionTextPublic;
        healthHeader = healthHeaderPublic;
    }

    //checks the the last 2 clicked items and moves the unit if applicable
    public static void checkClickMoveUnit()
    {
        if(clickHistory.Count >= 2) {
            GameObject cl0 = clickHistory[clickHistory.Count-2];
            GameObject cl1 = clickHistory[clickHistory.Count-1];
            if(cl0.GetComponent<Unit>() != null && cl1.GetComponent<Tile>() != null){
                if(cl0.transform.parent == TurnSystem.players[TurnSystem.currentPlayer].gameObject.transform) {
                    if(UIGameManager.assign) {
                        cl0.GetComponent<UnitHarvester>().assignedTile = cl1.GetComponent<Tile>();
                        UIGameManager.assign = false;
                    }
                    else if(!cl0.GetComponent<Unit>().canMove) {
                        if(!messagePanel2.activeInHierarchy) {
                            messagePanel2.SetActive(true);
                            clickSystemRef.disappearCoroutine(3);
                        }
                    }
                    else
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
                    if(!cl0.GetComponent<Unit>().canMove) {
                        if(!messagePanel2.activeInHierarchy) {
                            messagePanel2.SetActive(true);
                            clickSystemRef.disappearCoroutine(3);
                        }
                    }
                    else if(cl1.GetComponent<Unit>().takeDamage(cl0.GetComponent<Unit>().damage)){
                        cl0.GetComponent<Unit>().levelUp();
                        cl0.GetComponent<Unit>().move(tPos);
                    }
                    // cl0.GetComponent<Unit>().moves--;
                    cl0.GetComponent<Unit>().decreaseMoves(1);
                }
                if(cl0.GetComponent<Unit>() != null && cl1.GetComponent<TileColony>() != null && cl0.transform.parent != cl1.transform.parent && cl0.transform.parent == TurnSystem.players[TurnSystem.currentPlayer]){
                    Vector2 tPos = cl1.GetComponent<TileColony>().gridPosition;
                    if(!cl0.GetComponent<Unit>().canMove) {
                        if(!messagePanel2.activeInHierarchy) {
                            messagePanel2.SetActive(true);
                            clickSystemRef.disappearCoroutine(3);
                        }
                    }
                    else if(cl1.GetComponent<TileColony>().takeDamage(cl0.GetComponent<Unit>().damage)){
                        cl0.GetComponent<Unit>().levelUp();
                        cl0.GetComponent<Unit>().move(tPos);
                    }
                    // cl0.GetComponent<Unit>().moves--;
                    cl0.GetComponent<Unit>().decreaseMoves(1);
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
            Unit.unitPositions[(int)lastClicked.GetComponent<Tile>().gridPosition.y,(int)lastClicked.GetComponent<Tile>().gridPosition.x] = true;
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
            infoPanel.sizeDelta = new Vector2((float)390, infoPanel.sizeDelta.y);
            healthHeader.gameObject.SetActive(true);
            healthHeader.text = "Health: " + go.GetComponent<Unit>().health + "/" + go.GetComponent<Unit>().maxHealth;
        } else if(go.GetComponent<TileColony>() != null) {
            infoPanel.sizeDelta = new Vector2((float)390, infoPanel.sizeDelta.y);
            healthHeader.gameObject.SetActive(true);
            healthHeader.text = "Health: " + go.GetComponent<TileColony>().health + "/" + go.GetComponent<TileColony>().maxHealth;
        } else {
            infoPanel.sizeDelta = new Vector2((float)170, infoPanel.sizeDelta.y);
            healthHeader.gameObject.SetActive(false);
        }
    }

    bool checkCollision(Collider collideComponent)
    {
        return collideComponent != null &&  hitInfo.collider == collideComponent;
    }

    void Update()
    {
        if(!UIPauseManager.paused){
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Physics.Raycast(ray, out hitInfo, 200);

            if(UIPauseManager.paused || EventSystem.current.IsPointerOverGameObject()) return; //this bit makes ui block mouse

            Entity[] entities = FindObjectsOfType<Entity>();
            foreach(Entity entity in entities) {
                if(checkCollision(entity.getCollider())) {// && (entity.GetComponent<Unit>() == null || entity.GetComponent<Unit>().moves > 0)){
                    if(Input.GetMouseButtonDown(0)){ // clicked
                        entity.clicked(clickHistory);
                        ClickSystem.updateSelectionText(entity.gameObject); // move this into clicked

                    } else if(clickHistory.Count == 0 || entity.clickState == ClickSystem.ClickState.none) { // hover
                        entity.hovered(clickHistory); //hover doesn't work if a tile is selected
                        ClickSystem.updateSelectionText(entity.gameObject);
                    }
                } else if(clickHistory.Count == 0 || clickHistory.IndexOf(entity.gameObject) != clickHistory.Count-1) { // default
                        entity.inactive(clickHistory);
                }
            }
        }
    }
}