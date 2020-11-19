using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrocodileDeathZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            PlayerState.Instance.ChangeHealth(-3, true);
        }
    }
}
