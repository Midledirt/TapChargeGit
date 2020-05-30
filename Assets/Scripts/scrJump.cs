using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrJump : MonoBehaviour
{
    [Tooltip("Slot the player into this slot")]
    public Transform Player;

    private float jumpheight;
    [Header("Set default Jump Height")]
    [SerializeField]
    [Range(0.1f, 1f)]
    private float defaultJumpheight = 0.1f;
    public bool isJumping = false;

    public bool Grounded;

    [Tooltip("The animator thats attached to the oPLayer")]
    public Animator boxAnimator;

    private void FixedUpdate()
    {
        //Calculate jump: Jumpheight + tapCount.
        jumpheight = (defaultJumpheight + GetComponent<scrTapCharge>().tapCount) * Time.deltaTime;
        if (isJumping == true)
        {
            //Execute the jump
            StartCoroutine(executeJump());
        }
    }

    //Ground check and animation
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ground"))
        {
            Grounded = true;
            boxAnimator.SetBool("HitGround", true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Ground"))
        {
            Grounded = false;
            boxAnimator.SetBool("HitGround", false);
        }
    }

    public void Jump()
    {
        //Very simple jump function
        Player.Translate(0, jumpheight, 0);
    }

    IEnumerator executeJump()
    {
        Jump();
        yield return new WaitForSeconds(0.2f);
        isJumping = false;
    }

}
