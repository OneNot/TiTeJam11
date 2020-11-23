using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerRB : MonoBehaviour
{
    public static PlayerControllerRB Instance;

    //public values
    public float MoveSpeed, InAirMoveSpeedMultiplier = 1, JumpForce, JumpForcePortionPerStep = 0.2f, GravityScale, TerminalVelocity;
    public float GroundCheckDistance, GroundCheckRadius;
    public Animator animator;
    public GameObject model;

    [HideInInspector]
    private bool InputEnabled = true;

    //private refs
    private Rigidbody rb;

    //private values
    private bool grounded;
    private float hInput, remainingJumpForce;

    private void Awake() {
        Instance = this;
        rb = GetComponent<Rigidbody>();
        if(rb == null)
            rb = gameObject.AddComponent<Rigidbody>();

        remainingJumpForce = 0f;
    }

    private void Update() {
        grounded = false;
        //ground check
        if( Physics.BoxCast(transform.position, Vector3.one * GroundCheckRadius, Vector3.down, out RaycastHit hit, Quaternion.identity, GroundCheckDistance, LayerMask.GetMask("ForegroundEnvironment", "Ground")) )
        {
            //print("grounded");
            grounded = true;

            animator.SetBool("Airborne", false);
        }
        else
        {
            //print("not grounded");
            grounded = false;

            animator.SetBool("Airborne", true);
            animator.SetBool("Walking", false);
        }

        //Orient to ground - Needs more work (pivot on vääräs paikkaa for one)
        /*
        if(grounded)
        {
            if(Physics.Raycast(transform.position, -transform.up, out RaycastHit hit2, 5f, LayerMask.GetMask("ForegroundEnvironment", "Ground")))
            {
                model.transform.up = hit2.normal;
            }
        }
        */

        if(InputEnabled)
        {
            //get inputs
            hInput = -Input.GetAxis("Horizontal");
            bool jumpInput = Input.GetButtonDown("Jump");
            bool jumpInputRelease = Input.GetButtonUp("Jump");

            //move
            rb.velocity = new Vector3(hInput * MoveSpeed * (grounded ? 1 : InAirMoveSpeedMultiplier), rb.velocity.y, rb.velocity.z);

            if (hInput != 0 && grounded)
            {
                animator.SetBool("Walking", true);
            }

            if (hInput > 0)
            {
                model.transform.eulerAngles = new Vector3(model.transform.eulerAngles.x, 0, model.transform.eulerAngles.z);
            }
            else if (hInput < 0)
            {
                model.transform.eulerAngles = new Vector3(model.transform.eulerAngles.x, 180, model.transform.eulerAngles.z);
            }

            //jump
            if (jumpInput && grounded)
            {
                //making sure the jump propels the player upwards always the same amount, regardless of other downward/upward forces
                //main reason being that you can jump ever so slightly before you actually hit the ground. So you might have downward motion when you jump, resulting in a smaller jump.
                rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

                remainingJumpForce = JumpForce;
            }
            if(jumpInputRelease)
                remainingJumpForce = 0f;
        }

        if (hInput == 0)    // Moved out of the former if clause to consider stunned state etc
        {
            animator.SetBool("Walking", false);
        }
    }

    private void FixedUpdate() {
        //gravity
        if(rb.velocity.y < TerminalVelocity)
            rb.AddForce(Vector3.down * 9.81f * GravityScale, ForceMode.Acceleration);

        if(remainingJumpForce > 0.0001f) //basically > 0, but probably don't need to keep calculating for no reason after the value is small enough so...
        {
            //Probably a really bad way to do this frankly, but this is what came to mind first. Will look into a better method if this is truly horrible
            float forceToApplyThisStep = remainingJumpForce * JumpForcePortionPerStep;
            //print("Jump force applied this physics step: " + forceToApplyThisStep);
            rb.AddForce(Vector3.up * forceToApplyThisStep, ForceMode.Force);
            remainingJumpForce -= forceToApplyThisStep;
        }
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
