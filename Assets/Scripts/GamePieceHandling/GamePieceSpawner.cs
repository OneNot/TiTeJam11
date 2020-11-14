using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePieceSpawner : MonoBehaviour
{
    public static GamePieceSpawner Instance;
    [SerializeField]
    List<GameObject> GamePiecePrefs;
    public float DestroyAtDistance = 500f;

    private Vector3 endPointOfPreviousPiece;


    private void Awake() {
        Instance = this;
    }

    private void Start() {
        SpawnRandomGamePiece(true);
    }

    private void Update() {
        if(Input.GetButtonDown("Fire1"))
            SpawnRandomGamePiece();
    }

    private void SpawnRandomGamePiece(bool first = false)
    {
        //get random object from pool
        GameObject go = Instantiate(GamePiecePrefs[Random.Range(0, GamePiecePrefs.Count)]);
        go.SetActive(true);

        Transform gr = go.transform.Find("ground");

        if(first)
        {
            go.transform.position = transform.position;
        }
        else
        {
            go.transform.position = new Vector3(endPointOfPreviousPiece.x - (gr.localScale.x / 2), endPointOfPreviousPiece.y, endPointOfPreviousPiece.z);
        }

        endPointOfPreviousPiece = new Vector3(go.transform.position.x + gr.GetComponent<Collider>().bounds.min.x, go.transform.position.y, go.transform.position.z);
        Debug.DrawLine(endPointOfPreviousPiece, new Vector3(endPointOfPreviousPiece.x, endPointOfPreviousPiece.y + 100f, endPointOfPreviousPiece.z), Color.red, 5f);
    }
}