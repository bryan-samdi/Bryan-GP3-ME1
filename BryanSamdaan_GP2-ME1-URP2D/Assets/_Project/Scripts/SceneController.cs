using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour
{
    public void OnMainMenuButtonPressed()
    {
        CustomSceneManager.Instance.LoadMainMenu();
    }

    public void OnWaitingAreaButtonPressed()
    {
        CustomSceneManager.Instance.LoadWaitingArea();
    }

    public void OnPlaySceneButtonPressed()
    {
        CustomSceneManager.Instance.LoadPlayScene();
    }

    public void OnResumeButtonPressed()
    {
        CustomSceneManager.Instance.ResumeGame();
    }

    public void OnQuitButtonPressed()
    {
        CustomSceneManager.Instance.QuitGame();
    }
}
