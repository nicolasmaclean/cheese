using UnityEngine;
using System.Collections.Generic;

public class ClickSystem : MonoBehaviour
{
    public static List<GameObject> clickHistory;

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
                if(cl0.GetComponent<MouseOver>().goType == MouseOver.GameObjectType.Unit && cl1.GetComponent<MouseOver>().goType == MouseOver.GameObjectType.Tile){
                    cl0.transform.parent.GetComponent<Unit>().move(cl1.transform.position);
                    clickHistory = new System.Collections.Generic.List<GameObject>();
                    clickHistory.Add(null);
                }
            }
        }
    }

    void Update()
    {
        checkClickMoveUnit();
    }
}