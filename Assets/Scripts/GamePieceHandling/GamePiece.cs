using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePiece : MonoBehaviour
{
    public string name;
    public int id;

    private void Update() {

        // Destroy(duplicates) / Hide object when far away
        if(Vector3.Distance(transform.position, PlayerControllerRB.Instance.transform.position) > GamePieceSpawner.Instance.DestroyAtDistance)
        {
            Destroy(gameObject);
        }
    }

}
