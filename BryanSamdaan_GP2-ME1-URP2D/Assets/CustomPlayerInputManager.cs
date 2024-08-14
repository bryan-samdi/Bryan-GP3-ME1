using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CustomPlayerInputManager : MonoBehaviour
{
    public static CustomPlayerInputManager Instance;

    public PlayerInputManager inputManager;
    public GameObject playerPrefab1;  
    public GameObject playerPrefab2;  

    public Transform spawnPoint1; 
    public Transform spawnPoint2; 

    private GameObject player1; 
    private GameObject player2; 

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

        inputManager.onPlayerJoined += OnPlayerJoined;
    }

    public void ResetPlayerInputManager()
    {
        player1 = null;
        player2 = null;

        inputManager.playerPrefab = playerPrefab1;
    }

    private void OnPlayerJoined(PlayerInput input)
    {
        if (player1 == null)
        {
            player1 = input.gameObject;
            player1.transform.position = spawnPoint1.position;
            inputManager.playerPrefab = playerPrefab2; 
        }
        else if (player2 == null)
        {
            player2 = input.gameObject;
            player2.transform.position = spawnPoint2.position;
        }
    }

    private void OnDestroy()
    {
        inputManager.onPlayerJoined -= OnPlayerJoined;
    }
}
