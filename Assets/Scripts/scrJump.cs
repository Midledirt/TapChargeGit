using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrJump : MonoBehaviour
{
    [Tooltip("Slot the player into this slot")]
    public Transform Player;

    public float jumpheight;
    private float defaultJumpheight = 5f;
    public bool isJumping = false;

    public bool Grounded;

    [Tooltip("The animator thats attached to the oPLayer")]
    public Animator boxAnimator;

    private void FixedUpdate()
    {
        jumpheight = (defaultJumpheight + GetComponent<scrTapCharge>().tapCount) * Time.deltaTime;
        if (isJumping == true)
        {
            StartCoroutine(executeJump());
        }
    }

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
        Player.Translate(0, jumpheight, 0);
    }

    IEnumerator executeJump()
    {
        Jump();
        yield return new WaitForSeconds(0.2f);
        isJumping = false;
    }

}
