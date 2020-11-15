using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    public void StartGame()
    {
        print("Scene Changed");
        SceneManager.LoadScene("GameScene");
    }

    public void QuitGame()
    {
        print("Game Quit");
        Application.Quit();
    }
}
