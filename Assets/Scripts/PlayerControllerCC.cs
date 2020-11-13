using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerCC : MonoBehaviour
{
    //public values
    public float GravityMultiplier;
    public float MoveSpeed;
    public float JumpForce;
    public float MaxJumpHoldTime;

    //private values
    private float gravity;
    private float jumpHoldTime;
    private bool jumpOngoing;

    //private refs
    private CharacterController cc;


    private void Awake() {
        //get character controller
        cc = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        gravity = 9.81f * GravityMultiplier; //setting this here so we can change the multiplier mid-game if we want

        //get inputs
        float moveInputH = Input.GetAxis("Horizontal");
        bool jumpInput = (Input.GetAxis("Vertical") > 0f || Input.GetButton("Jump"));

        //real horizontal move
        float horizontalMove = moveInputH * MoveSpeed;


        //Jump: Basically jump can only be started when grounded, but jump can be continued as long as the button is held and we haven't reached max hold time.
        //      Releasing the jump button ends the jump, and again, a new one can only be started when grounded 

        //if jump input is released, jump is ended
        if(!jumpInput)
        {
            jumpOngoing = false;
            jumpHoldTime = 0f;
        }

        //if we are grounded, jumpInput is held down and jump isn't already ongoing: start jump
        if(cc.isGrounded && jumpInput && !jumpOngoing)
        {
            //start jump
            jumpOngoing = true;
        }

        //real vertical move - jump force applied if jump ongoing
        float verticalMove = (jumpOngoing ? JumpForce : 0f) - gravity;

        //update jump input hold time
        if(jumpOngoing && jumpHoldTime < MaxJumpHoldTime)
            jumpHoldTime += Time.deltaTime;
        else
        {
            jumpOngoing = false;
            jumpHoldTime = 0f;
        }


        //build final move vector
        Vector3 moveVector = new Vector3(0f, verticalMove, horizontalMove);

        //move character
        cc.Move(moveVector * Time.deltaTime);
    }
}
