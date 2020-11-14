using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerRB : MonoBehaviour
{
    public static PlayerControllerRB Instance;

    //public values
    public float MoveSpeed, JumpForce, GravityScale, TerminalVelocity;
    public float GroundCheckDistance, GroundCheckRadius;

    //private refs
    private Rigidbody rb;

    //private values
    private bool jumpUsed;

    private void Awake() {
        Instance = this;
        rb = GetComponent<Rigidbody>();
        if(rb == null)
            rb = gameObject.AddComponent<Rigidbody>();
    }

    private void Update() {
        //get inputs
        float hInput = -Input.GetAxis("Horizontal");
        bool jumpInput = Input.GetButtonDown("Jump");
        bool grounded = false;

        //ground check
        if( Physics.BoxCast(transform.position, Vector3.one * GroundCheckRadius, Vector3.down, out RaycastHit hit, Quaternion.identity, GroundCheckDistance, LayerMask.GetMask("ForegroundEnvironment", "Ground")) )
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
        rb.velocity = new Vector3(hInput * MoveSpeed, rb.velocity.y, rb.velocity.z);

        //jump
        if(jumpInput && grounded)
        {
            //making sure the jump propels the player upwards always the same amount, regardless of other downward/upward forces
            //main reason being that you can jump ever so slightly before you actually hit the ground. So you might have downward motion when you jump, resulting in a smaller jump.
            rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
            
            rb.AddForce(Vector3.up * JumpForce, ForceMode.Impulse);
        }
    }

    private void FixedUpdate() {
        if(rb.velocity.y < TerminalVelocity)
            rb.AddForce(Vector3.down * 9.81f * GravityScale, ForceMode.Acceleration);
    }
}
