using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crocodile : MonoBehaviour
{
    // Public
    public Animator animator;
    public float movementSpeed;

    // Method based
    private bool walking;
    private bool chomping;

    // private
    private Rigidbody rb;

    private void Awake()
    {
        rb = gameObject.GetComponentInChildren<Rigidbody>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            Chomping = false;
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            Chomping = true;
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Walking = false;
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            Walking = true;
        }
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

    public void MoveRight()
    {
        rb.AddForce(transform.forward * movementSpeed * Time.deltaTime);
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
            Debug.Log("Bonked player, do things");         
            // rb.constraints = RigidbodyConstraints.None; // only needed for testing with temporarily inanimate "player" cubes



            rb.AddForce((-Vector3.right * 10f) + Vector3.up * 10f, ForceMode.Impulse);
        }
    }
}
