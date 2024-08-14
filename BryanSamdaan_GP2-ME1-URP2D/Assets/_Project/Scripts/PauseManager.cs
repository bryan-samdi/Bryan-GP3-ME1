using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PauseManager : MonoBehaviour
{
    public GameObject pauseMenuUI;
    public TextMeshProUGUI pauseMessage;

    void Start()
    {
       // GameManager.Instance.pauseMenu = pauseMenuUI;
       // GameManager.Instance.pauseMessage = pauseMessage;
        pauseMenuUI.SetActive(false);
    }
}
