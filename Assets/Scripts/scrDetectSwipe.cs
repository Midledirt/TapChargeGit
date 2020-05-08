using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrDetectSwipe : MonoBehaviour
{
    //Problem: SwipeUpp virker ikke, mens swipeDown funker. Har ikke peiling på hvorfor... 

    //These are coordinates used to store the positions of the touch
    private Vector3 initialTouchPos;
    private Vector3 lastTouchPos;

    //These bools turns on for a set duration after a downward or upward swipe
    public bool swipeUp;
    public bool swipeDown;


    [SerializeField]
    [Range(100f, 300f)]
    private float minDistanceForSwipe = 100f;

    private void Update()
    {
        //Lets check for screen touches
        foreach (Touch theFirstTouch in Input.touches)
        {
            //Store the first touch whenever 1 or more fingers hit the screen
            //Touch theFirstTouch = Input.GetTouch(0);
            //print("Touching the screen");

            if (theFirstTouch.phase == TouchPhase.Began)
            {
                //Store the ORIGINAL coordinates of the first touch
                initialTouchPos = theFirstTouch.position;
                //The following line of code is not something I haad originally, but this will reset the finger up position whenever you touch the device again. Hmmmm
                lastTouchPos = theFirstTouch.position;
                //print(theFirstTouch.position);
            }

            if (theFirstTouch.phase == TouchPhase.Ended)
            {
                //Store the LAST coordinates of the first touch
                lastTouchPos = theFirstTouch.position;
                //print(theFirstTouch.position);
                swipe();
            }
        }
        
    }
    //Cheks the distance between initial and last touch pos and stores it in a float
    float swipeDistance()
    {
        return Mathf.Abs(lastTouchPos.y - initialTouchPos.y);
    }
    //checks if the swipeDistance exceeds the swipeDistanceMet
    bool swipeDistanceMet()
    {
        return swipeDistance() > minDistanceForSwipe;
    }
    private void swipe()
    {
        if (swipeDistanceMet())
        {
            //print("theswipewaslongenough");
            if (lastTouchPos.y > initialTouchPos.y)
            {
                swipeUp = true;
                StartCoroutine(onSwipe());
            }
            else if (lastTouchPos.y < initialTouchPos.y)
            {
                swipeDown = true;
                StartCoroutine(onSwipe());
            }
        }
    }

    IEnumerator onSwipe()
    {
        //This IEnumerator simply sets swipe down to false after a timer.
        yield return new WaitForSeconds(0.2f);
        swipeUp = false;
        swipeDown = false;

        //yield return null;
    }
}
