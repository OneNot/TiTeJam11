using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class IGUI : MonoBehaviour
{
    public static IGUI Instance;
    public GameObject GameOver;
    public GameObject PauseScreen;
    public GameObject AliveUI;
    public Text PauseScoreText;
    public Text DeadScoreText;
    public Text AliveScoreText;

    public float score = 0;

    private void Awake()
    {
        Instance = this;
        Time.timeScale = 1;
    }
    // Start is called before the first frame update
    void Start()
    {
        Resume();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
        }

        if(!PauseScreen.activeSelf)
        score += Time.deltaTime;

        PauseScoreText.text = DeadScoreText.text = "Score: " + Mathf.RoundToInt(score).ToString();
        AliveScoreText.text = Mathf.RoundToInt(score).ToString();
    }

    public void ShowGameOverScreen()
    {
        Cursor.visible = true;
        Time.timeScale = 0;
        GameOver.SetActive(true);
        AliveUI.SetActive(false);
    }

    public void Pause()
    {
        Cursor.visible = true;
        Time.timeScale = 0;
        PauseScreen.SetActive(true);
        AliveUI.SetActive(false);
    }

    public void Resume()
    {
        Cursor.visible = false;
        Time.timeScale = 1;
        PauseScreen.SetActive(false);
        AliveUI.SetActive(true);
    }

    public void Restart()
    {
        SceneManager.LoadScene(1);
    }
    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
