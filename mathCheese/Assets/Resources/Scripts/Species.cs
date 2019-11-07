using System.Collections.Generic;
using UnityEngine;

public class Species : MonoBehaviour
{
    public static List<Transform> unitType = new List<Transform>(); //need to assign transfrom to here from unity editor
    public List<Transform> unitTypeTemp = new List<Transform>(); 

    void Start() {
        foreach (Transform t in unitTypeTemp){
            unitType.Add(t);
        }    
    }

    public static Transform getRandomUnitTransform()
    {

        if(unitType.Count > 0)
            return unitType[Random.Range(0, unitType.Count)];
        Debug.Log("list of species is empty");
        return null;
    }
}