using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
  public static GameManager Instance;

    public int player1Wins = 0;
    public int player2Wins = 0;
    public int maxWins = 3;

    public TextMeshProUGUI countdownText;
    public GameObject countdownZone;

    private bool player1Joined = false;
    private bool player2Joined = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void StartGame()
    {
        // Reset the player input manager
        CustomPlayerInputManager.Instance.ResetPlayerInputManager();

        // Transition to the game scene
        SceneManager.sceneLoaded += OnGameSceneLoaded;
        SceneManager.LoadScene("MainGame"); 
    }

   private void OnGameSceneLoaded(Scene scene, LoadSceneMode mode)
{
    SceneManager.sceneLoaded -= OnGameSceneLoaded;

    // No need to reassign spawn points here since they're assigned in the Inspector
}

    public void PlayerJoined(int playerNumber)
    {
        if (playerNumber == 1)
            player1Joined = true;
        else if (playerNumber == 2)
            player2Joined = true;

        if (player1Joined && player2Joined)
        {
            ShowCountdownZone();
        }
    }

    private void ShowCountdownZone()
    {
        if (countdownZone != null)
        {
            countdownZone.SetActive(true);
        }
    }

    public void PlayerDisconnected(int playerNumber)
    {
        if (playerNumber == 1)
            player1Joined = false;
        else if (playerNumber == 2)
            player2Joined = false;

        PauseGame();
    }

    private void PauseGame()
    {
        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
    }

    public bool IsPlayerJoined(int playerNumber)
    {
        return playerNumber == 1 ? player1Joined : player2Joined;
    }

    public void EndRound(int winningPlayer)
    {
        if (winningPlayer == 1)
        {
            player1Wins++;
        }
        else if (winningPlayer == 2)
        {
            player2Wins++;
        }

        if (player1Wins >= maxWins)
        {
            EndMatch(1);
        }
        else if (player2Wins >= maxWins)
        {
            EndMatch(2);
        }
        else
        {
            CustomSceneManager.Instance.LoadWaitingArea();
        }
    }

    private void EndMatch(int winningPlayer)
    {
        Debug.Log("Player " + winningPlayer + " wins the match!");
        player1Wins = 0;
        player2Wins = 0;
        CustomSceneManager.Instance.LoadWaitingArea();
    }
}