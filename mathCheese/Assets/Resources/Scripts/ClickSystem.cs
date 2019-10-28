using UnityEngine;

public class ClickSystem : MonoBehaviour
{
    //checks the the last 2 clicked items and moves the unit if applicable
    void clickMoveUnit()
    {
        if(MouseOver.lastClicked.Count >= 2) {
            GameObject cl0 = MouseOver.lastClicked[MouseOver.lastClicked.Count-2];
            GameObject cl1 = MouseOver.lastClicked[MouseOver.lastClicked.Count-1];
            if(cl0 != null && cl1 != null) {
                if(cl0.GetComponent<MouseOver>().goType == MouseOver.GameObjectType.Unit && cl1.GetComponent<MouseOver>().goType == MouseOver.GameObjectType.Tile){
                    cl0.transform.parent.GetComponent<Unit>().move(cl1.transform.position);
                    MouseOver.lastClicked = new System.Collections.Generic.List<GameObject>();
                    MouseOver.lastClicked.Add(null);
                }
            }
        }
    }

    void Update()
    {
        clickMoveUnit();
    }
}