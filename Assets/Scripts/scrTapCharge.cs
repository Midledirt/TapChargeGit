using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrTapCharge : MonoBehaviour
{
    //Stores the number of taps
    public int tapCount;

    //Checks if we are crouching
    public bool isCrouching;

    //Checks if we are charging (started tapping after crouching)
    public bool isCharging;

    public float jumpChargeTimer = 1f;

    [Header("Decide the tap-jump power")]
    [Tooltip("The higher the number, the more force is applied for each tap")]
    public int chargeMultiplier;

    [Tooltip("The animator thats attached to the oPLayer")]
    public Animator boxAnimator;

    private void Update()
    {
        isCrouching = GetComponent<scrDetectSwipe>().characterCrouch;
    }

    private void FixedUpdate()
    {
        if (isCrouching)
        {
            //Reset the jump ready bool
            boxAnimator.SetBool("JumpReady", false);
            //Store Taps
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                tapCount += (Input.touchCount * chargeMultiplier);
            }
        }

        //Activate the charging state
        if (tapCount > 0)
        {
            isCharging = true;
        }
        else if (tapCount <= 0)
        {
            isCharging = false;
        }
        if (isCrouching && isCharging)
        {
            boxAnimator.SetBool("ChargedJump", true);
            //Ready the jump
            StartCoroutine(initiateJump());
        }
        else if(!isCrouching)
        {
            boxAnimator.SetBool("ChargedJump", false);
        }

    }
    IEnumerator initiateJump()
    {
        yield return new WaitForSeconds(jumpChargeTimer);
        executeJump();
        yield return new WaitForSeconds(0.5f);
        tapCount = 0;
    }
    public void executeJump()
    {
        //Initiate the jump
        GetComponent<scrJump>().isJumping = true;

        //Animate the jump
        boxAnimator.SetBool("JumpReady", true);
    }
}
