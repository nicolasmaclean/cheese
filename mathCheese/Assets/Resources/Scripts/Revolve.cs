using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Revolve : MonoBehaviour
{
    public float revoluionSpeed, angle;

    void FixedUpdate()
    {
        transform.rotation = Quaternion.Euler(0, angle, 0);
        angle += revoluionSpeed;
        if(angle > 360)
            angle = 0;
    }
}
