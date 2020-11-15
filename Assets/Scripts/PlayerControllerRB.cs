using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerRB : MonoBehaviour
{
    public static PlayerControllerRB Instance;

    //public values
    public float MoveSpeed, InAirMoveSpeedMultiplier = 1, JumpForce, GravityScale, TerminalVelocity;
    public float GroundCheckDistance, GroundCheckRadius;

    [HideInInspector]
    private bool InputEnabled = true;

    //private refs
    private Rigidbody rb;

    //private values
    private bool jumpUsed, grounded;
    private float hInput;

    private void Awake() {
        Instance = this;
        rb = GetComponent<Rigidbody>();
        if(rb == null)
            rb = gameObject.AddComponent<Rigidbody>();
    }

    private void Update() {
        grounded = false;
        //ground check
        if( Physics.BoxCast(transform.position, Vector3.one * GroundCheckRadius, Vector3.down, out RaycastHit hit, Quaternion.identity, GroundCheckDistance, LayerMask.GetMask("ForegroundEnvironment", "Ground")) )
        {
            //print("grounded");
            grounded = true;
        }
        else
        {
            //print("not grounded");
            grounded = false;
        }

        if(InputEnabled)
        {
            //get inputs
            hInput = -Input.GetAxis("Horizontal");
            bool jumpInput = Input.GetButtonDown("Jump");

            //move
            rb.velocity = new Vector3(hInput * MoveSpeed * (grounded ? 1 : InAirMoveSpeedMultiplier), rb.velocity.y, rb.velocity.z);

            //jump
            if(jumpInput && grounded)
            {
                //making sure the jump propels the player upwards always the same amount, regardless of other downward/upward forces
                //main reason being that you can jump ever so slightly before you actually hit the ground. So you might have downward motion when you jump, resulting in a smaller jump.
                rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

                rb.AddForce(Vector3.up * JumpForce, ForceMode.Impulse);
            }
        }
    }

    private void FixedUpdate() {
        //gravity
        if(rb.velocity.y < TerminalVelocity)
            rb.AddForce(Vector3.down * 9.81f * GravityScale, ForceMode.Acceleration);
    }

    public void StunForXSeconds(float seconds = 1f)
    {
        StopAllCoroutines();
        StartCoroutine(StunForXSecondsIE(seconds));
    }

    private IEnumerator StunForXSecondsIE(float seconds = 1f)
    {
        PlayerState.Instance.invulnerable = true;   //make player invulnerable
        InputEnabled = false; //disable inputs for stun duration
        rb.constraints = RigidbodyConstraints.FreezePositionZ; //disable all other constrains, but z movement
        rb.AddTorque(new Vector3(100f, 100f, 0f), ForceMode.Impulse); //add torque to set player spinning

        yield return new WaitForSeconds(seconds); //wait set amount

        //reset player rotation and invulnerability
        PlayerState.Instance.invulnerable = false;
        rb.angularVelocity = Vector3.zero;
        transform.rotation = Quaternion.Euler(Vector3.zero);

        rb.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation; //disable z movement and all rotation
        InputEnabled = true; //re-enable inputs
    }
}
