using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyable : MonoBehaviour
{
    private Rigidbody rb;
    private float collisionTimeLimit = 0.1f;
    private float collisionTimer;

    [HideInInspector]
    public bool crocodileTouched;

    private void Awake()
    {
        InitialChecks();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CollisionActions(Collision _collision, bool addForce = true)
    {
        // Do constraint changes if necessary
        rb.constraints = RigidbodyConstraints.None;

        if(_collision.gameObject.tag == "Crocodile" && addForce)
        rb.AddRelativeForce(_collision.GetContact(0).point.normalized * 15f, ForceMode.Impulse);
    }

    public void InitialChecks() // Initial checks
    {
        if (gameObject.GetComponent<Collider>() == null)
        {
            Debug.Log("Missing a collider, adding mesh collider. Add a proper collider beforehand if this causes problems");
            MeshCollider mc = gameObject.AddComponent<MeshCollider>();
            mc.convex = true;
        }
        rb = gameObject.GetComponentInChildren<Rigidbody>();
        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody>();
            rb.constraints = RigidbodyConstraints.FreezeAll;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Crocodile" || collision.relativeVelocity.magnitude >= 5f && collision.gameObject.tag != "Player")
        {
            if(collision.gameObject.tag == "Crocodile")
            {
                crocodileTouched = true;
            }

            CollisionActions(collision);
        }
    }

    //Failsafe
    private void OnCollisionStay(Collision collision)
    {
        // There is meticulous null checking here to ensure maximum foolproofness of using this script
        bool thisCrocodileTouched = false;
        bool otherCrocodileTouched = false;

        Destroyable thisDestroyable = this;
        Destroyable otherDestroyable = collision.gameObject.GetComponentInChildren<Destroyable>();

        if (otherDestroyable == null)
            otherDestroyable = collision.gameObject.GetComponentInParent<Destroyable>();

        if (otherDestroyable != null)   // Once more to check if it is a lost cause, defaulting the boolean to false
        {
            otherCrocodileTouched = otherDestroyable.crocodileTouched;
        }

        if (thisDestroyable != null)
        {
            thisCrocodileTouched = thisDestroyable.crocodileTouched;
        }

        if (thisCrocodileTouched || otherCrocodileTouched)
        {
            //CollisionActions(collision, false);

            collisionTimer += Time.deltaTime;
            if (collisionTimer > collisionTimeLimit)
            {
                CollisionActions(collision, false);
            }
        }
    }
}
