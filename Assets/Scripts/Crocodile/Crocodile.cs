using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crocodile : MonoBehaviour
{
    // Public
    public Animator animator;
    public float movementSpeed;
    public float movementSpeedIncreaseRate;

    // Method based
    private bool walking;
    private bool chomping;

    // private
    private Rigidbody rb;
    private float startingMovementSpeed;

    private void Awake()
    {
        rb = gameObject.GetComponentInChildren<Rigidbody>();
        startingMovementSpeed = movementSpeed;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        IncreaseMovementSpeed();
    }

    private void FixedUpdate()
    {
        MoveRight();
    }

    public bool Walking
    {
        get
        {
            return walking;
        }
        set
        {
            walking = value;
            animator.SetBool("Walking", value);
        }
    }

    public bool Chomping
    {
        get
        {
            return chomping;
        }
        set
        {
            chomping = value;
            animator.SetBool("Chomping", value);
        }
    }

    public void IncreaseMovementSpeed()
    {
        movementSpeed += movementSpeedIncreaseRate * Time.deltaTime;
        animator.speed = Mathf.Abs(rb.velocity.x / 10f);
    }

    public void MoveRight()
    {
        rb.AddForce(transform.forward * movementSpeed * Time.deltaTime, ForceMode.Acceleration);
        Walking = true;
        Chomping = true;
    }

    public void StopMoving()
    {
        Chomping = false;
        Walking = false;
    }

    private void OnCollisionStay(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Rigidbody rb = collision.gameObject.GetComponent<Rigidbody>();

            // Debug things
            //Debug.Log("Bonked player, do things");         
            // rb.constraints = RigidbodyConstraints.None; // only needed for testing with temporarily inanimate "player" cubes



            rb.AddForce((-Vector3.right * 5f) + Vector3.up * 5f, ForceMode.Impulse);

            if (!PlayerState.Instance.invulnerable)
            {
                PlayerState.Instance.ChangeHealth(-1);
                PlayerControllerRB.Instance.StunForXSeconds(1.5f);
                PlayerAudio.Instance.PlaySqueek();
            }  
        }
    }
}
