using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
    public static PlayerState Instance;

    public int MaxHP = 3;
    private int currentHP;


    private void Awake() {
        Instance = this;
    }

    private void Update() {
        if(currentHP <= 0)
        {
            print("Ya dead my guy");
        }
    }

    public void ChangeHealth(int healthChange)
    {
        currentHP += healthChange;
    }

}
