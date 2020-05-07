using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrChargeJump : MonoBehaviour
{
    private void Update()
    {
        //Lets check for screen touches
        if (Input.touchCount > 0)
        {
            print("Touching the screen");
            //Store the first touch whenever 1 or more fingers hit the screen
            Touch theTouch = Input.GetTouch(0);
        }
    }
}
