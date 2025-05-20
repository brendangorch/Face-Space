using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseController : MonoBehaviour
{
    public GameObject player;
    public GameObject pauseCanvas;

    // on resume click
    public void ResumeGame()
    {
        // unpause
        pauseCanvas.SetActive(false);
        player.GetComponent<PlayerController>().isPaused = false;
        Time.timeScale = 1f;
    }

    // on restart click
    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("LevelScene");
    }

    // on quit click
    public void QuitToMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenuScene");
    }

    // on quit after loss
    public void QuitAfterLoss()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenuScene");

        // set the score here:
    }
}
