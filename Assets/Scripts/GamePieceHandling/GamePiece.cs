using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePiece : MonoBehaviour
{
    public string name;
    public int id;
    public Collider groundCollider;
    [HideInInspector]
    public bool duplicate = false;

    private void Awake() {
        groundCollider = transform.Find("ground").GetComponent<Collider>();
    }

    private void Update() {

        // Destroy(duplicates) / Hide object when far away
        if(Vector3.Distance(transform.position, PlayerControllerRB.Instance.transform.position) > GamePieceSpawner.Instance.DisableAtDistance)
        {
            if(duplicate)
            {
                Destroy(gameObject);
            }
            else
            {
                gameObject.SetActive(false);
            }
        }
    }

}
