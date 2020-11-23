using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePieceSpawner : MonoBehaviour
{
    public static GamePieceSpawner Instance;
    [SerializeField]
    List<GameObject> GamePiecePrefs;
    [Tooltip("use negative value for random")]
    public int FirstPieceToSpawn = -1;
    public float DestroyAtDistance = 500f;
    public float DistanceToSpawnNew = 500f;

    private Vector3 endPointOfPreviousPiece;


    private void Awake() {
        Instance = this;
    }

    private void Start() {
        SpawnGamePiece(true, FirstPieceToSpawn);
    }

    private void Update() {

        if(Vector3.Distance(PlayerControllerRB.Instance.transform.position, endPointOfPreviousPiece) < DistanceToSpawnNew)
            SpawnGamePiece();

        // if(Input.GetButtonDown("Fire1"))
        //     SpawnGamePiece();
        else if(Input.GetKeyDown(KeyCode.Alpha0))
            SpawnGamePiece(false, 0);
        else if(Input.GetKeyDown(KeyCode.Alpha1))
            SpawnGamePiece(false, 1);
        else if(Input.GetKeyDown(KeyCode.Alpha2))
            SpawnGamePiece(false, 2);
        else if(Input.GetKeyDown(KeyCode.Alpha3))
            SpawnGamePiece(false, 3);
        else if(Input.GetKeyDown(KeyCode.Alpha4))
            SpawnGamePiece(false, 4);
        else if(Input.GetKeyDown(KeyCode.Alpha5))
            SpawnGamePiece(false, 5);
    }

    private void SpawnGamePiece(bool first = false, int indexOfPieceToSpawn = -1)
    {
        GameObject go;

        //get random object
        if(indexOfPieceToSpawn < 0)
            go = Instantiate(GamePiecePrefs[Random.Range(0, GamePiecePrefs.Count)], Vector3.zero, Quaternion.Euler(0f, -90f, 0f));
        else //get given object
        {
            if(indexOfPieceToSpawn < GamePiecePrefs.Count)
                go = Instantiate(GamePiecePrefs[indexOfPieceToSpawn]);
            else
                return;
        }

        go.SetActive(true);

        Transform gr = go.transform.Find("ground");

        //These values should be the same for all pieces. They should be the same in the prefabs as well, but just in case, might as well set them here too
        gr.localPosition = Vector3.zero;
        gr.localScale = new Vector3(20f, 5f, gr.localScale.z);
        gr.localRotation = Quaternion.identity;

        if(first)
        {
            go.transform.position = transform.position;
        }
        else
        {
            go.transform.position = new Vector3(endPointOfPreviousPiece.x - (gr.GetComponent<Collider>().bounds.extents.x), endPointOfPreviousPiece.y, endPointOfPreviousPiece.z);
        }

        endPointOfPreviousPiece = new Vector3(go.transform.position.x + gr.GetComponent<Collider>().bounds.min.x, go.transform.position.y, go.transform.position.z);
        Debug.DrawLine(endPointOfPreviousPiece, new Vector3(endPointOfPreviousPiece.x, endPointOfPreviousPiece.y + 100f, endPointOfPreviousPiece.z), Color.red, 5f);
    }
}