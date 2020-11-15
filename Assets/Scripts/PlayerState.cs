﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
    public static PlayerState Instance;

    public int MaxHP = 3;
    public bool invulnerable = false;
    private int currentHP;


    private void Awake() {
        Instance = this;
        currentHP = MaxHP;
    }

    private void Update() {
        if(currentHP <= 0)
        {
            print("Ya dead my guy");
        }
    }

    public void ChangeHealth(int healthChange)
    {
        if(!invulnerable || healthChange > 0)
        currentHP += healthChange;

        Debug.Log(currentHP);
    }

}
