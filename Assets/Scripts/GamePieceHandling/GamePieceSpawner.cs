﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePieceSpawner : MonoBehaviour
{
    public static GamePieceSpawner Instance;
    [SerializeField]
    List<GameObject> GamePiecesToPool;
    List<GameObject> GamePiecePool;
    public float DisableAtDistance = 500f;

    private GamePiece latestPieceAdded = null;

    private void Awake() {
        Instance = this;
    }

    private void Start() {
        GamePiecePool = new List<GameObject>();
        if(GamePiecesToPool != null)
        {
            for(int i = 0; i < GamePiecesToPool.Count; i++)
            {
                GameObject obj = Instantiate(GamePiecesToPool[i]);
                obj.SetActive(false);
                GamePiecePool.Add(obj);
            }
        }

        SpawnRandomGamePiece();
    }

    private void Update() {
        if(Input.GetButtonDown("Fire1"))
            SpawnRandomGamePiece();
    }

    private void SpawnRandomGamePiece()
    {
        //get random object from pool
        GameObject go = GamePiecePool[Random.Range(0, GamePiecePool.Count)];
        
        //if the object is already in use
        if(go.activeSelf)
        {
            //duplicate a new one instead
            go = Instantiate(go);
            go.GetComponent<GamePiece>().duplicate = true;
        }

        go.SetActive(true);
        
        //if not the first piece
        if(latestPieceAdded != null)
        {
            //position the new piece right after the previous one
            go.transform.position = new Vector3(0f, 0f, latestPieceAdded.groundCollider.bounds.max.z + go.GetComponent<GamePiece>().groundCollider.transform.localScale.z/2);
        }
        else //first piece
        {
            //position the first piece
            go.transform.position = Vector3.zero;
        }

        //update previous/latest piece
        latestPieceAdded = go.GetComponent<GamePiece>();
    }
}