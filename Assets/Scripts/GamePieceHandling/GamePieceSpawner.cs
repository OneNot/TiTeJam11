using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePieceSpawner : MonoBehaviour
{
    public static GamePieceSpawner Instance;
    [SerializeField]
    List<GameObject> GamePiecesToPool;
    List<GameObject> GamePiecePool;

    private void Awake() {
        Instance = this;
    }

    private void Start() {
        if(GamePiecesToPool != null)
        {
            for(int i = 0; i < GamePiecesToPool.Count; i++)
            {
                //GamePiecePool.Add()
            }
        }
    }
}