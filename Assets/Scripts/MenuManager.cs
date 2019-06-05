using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [SerializeField]
    private Text highScoreText;

    public void MainScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainScene");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    private void Start()
    {
        Debug.Log("Menu started");
        highScoreText.text = "High Score: " + Score.GetHighScore().ToString();
    }
}
