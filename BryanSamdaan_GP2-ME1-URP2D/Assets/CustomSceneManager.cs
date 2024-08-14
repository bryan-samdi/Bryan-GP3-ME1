using UnityEngine;
using UnityEngine.SceneManagement;

public class CustomSceneManager : MonoBehaviour
{
    public static CustomSceneManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");      
    }

    public void LoadWaitingArea()
    {
        SceneManager.LoadScene("WaitingArea");
    }

    public void LoadPlayScene()
    {
        SceneManager.LoadScene("MainGame");
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        //GameManager.Instance.pauseMenu.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}