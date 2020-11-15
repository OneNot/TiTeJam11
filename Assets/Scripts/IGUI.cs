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
    public Text scoreText;
    public Text scoreText2;

    public float score = 0;

    private void Awake()
    {
        Instance = this;
        Time.timeScale = 1;
    }
    // Start is called before the first frame update
    void Start()
    {
        
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

        scoreText.text = "Score: " + Mathf.RoundToInt(score).ToString();
        scoreText2.text = "Score: " + Mathf.RoundToInt(score).ToString();

    }

    public void ShowGameOverScreen()
    {
        Time.timeScale = 0;
        GameOver.SetActive(true);
    }

    public void Pause()
    {
        Time.timeScale = 0;
        PauseScreen.SetActive(true);
    }

    public void Resume()
    {
        Time.timeScale = 1;
        PauseScreen.SetActive(false);
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
