using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIGameManager : MonoBehaviour
{
    public GameObject canvas;

    public void Update()
    {
        GameObject clicked = ClickSystem.clickHistory[ClickSystem.clickHistory.Count-1];
        if(clicked.GetComponent<TileColony>())
        {
            canvas.transform.GetChild(4).gameObject.SetActive(true);
        }
    }
}
