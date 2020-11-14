using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyable : MonoBehaviour
{
    private Rigidbody rb;
    private float collisionTimeLimit = 0.2f;
    private float collisionTimer;

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
        rb.AddRelativeForce(_collision.GetContact(0).point.normalized * 15, ForceMode.Impulse);
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
        if (collision.gameObject.tag == "Crocodile" || collision.relativeVelocity.magnitude >= 6f)
        {
            CollisionActions(collision);
        }

        Debug.Log(collision.relativeVelocity.magnitude);
    }

    //Failsafe
    private void OnCollisionStay(Collision collision)
    {
        collisionTimer += Time.deltaTime;
        if(collisionTimer > collisionTimeLimit)
        {
            CollisionActions(collision, false);
        }
    }
}
