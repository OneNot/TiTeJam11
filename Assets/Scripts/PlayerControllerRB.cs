using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerRB : MonoBehaviour
{
    //public values
    public float MoveSpeed, JumpForce, GravityScale, TerminalVelocity;
    public float GroundCheckDistance, GroundCheckRadius;

    //private refs
    private Rigidbody rb;

    //private values
    private bool jumpUsed;

    private void Awake() {
        rb = GetComponent<Rigidbody>();
        if(rb == null)
            rb = gameObject.AddComponent<Rigidbody>();
    }

    private void Update() {
        //get inputs
        float hInput = Input.GetAxis("Horizontal");
        bool jumpInput = Input.GetButtonDown("Jump");
        bool grounded = false;

        //ground check
        if( Physics.BoxCast(transform.position, Vector3.one * GroundCheckRadius, Vector3.down, out RaycastHit hit, Quaternion.identity, GroundCheckDistance, LayerMask.GetMask("ForegroundEnvironment")) )
        {
            print("grounded");
            grounded = true;
        }
        else
        {
            print("not grounded");
            grounded = false;
        }

        //move player horizontally
        rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, hInput * MoveSpeed);

        //jump
        if(jumpInput && grounded)
        {
            rb.AddForce(Vector3.up * JumpForce, ForceMode.Impulse);
        }
    }

    private void FixedUpdate() {
        if(rb.velocity.y < TerminalVelocity)
            rb.AddForce(Vector3.down * 9.81f * GravityScale, ForceMode.Acceleration);
    }
}
