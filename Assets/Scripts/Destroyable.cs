using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyable : MonoBehaviour
{
    private Rigidbody rb;

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

    public void CollisionActions()
    {
        // Do constraint changes if necessary
        rb.constraints = RigidbodyConstraints.None;
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
       // if(collision.gameObject.tag
    }
}
