using System.Collections;
using System.Collections.Generic;  
using UnityEngine;
using TMPro;

public class StartGameCountdown : MonoBehaviour
{ 

    public float countdownTime = 5f;
    public TextMeshProUGUI countdownText;  
    private bool countdownActive = false;
    private List<Collider2D> playersInZone = new List<Collider2D>();
    

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (IsPlayer(other) && !playersInZone.Contains(other))
        {
            playersInZone.Add(other);
            CheckAndStartCountdown();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (IsPlayer(other) && playersInZone.Contains(other))
        {
            playersInZone.Remove(other);
            StopCountdown();
        }
    }

    private void CheckAndStartCountdown()
    {
        if (playersInZone.Count == 2 && !countdownActive)
        {
            StartCoroutine(StartCountdown());
        }
    }

    private void StopCountdown()
    {
        if (countdownActive)
        {
            countdownActive = false;
            StopCoroutine("StartCountdown");
            if (countdownText != null)
            {
                countdownText.enabled = false;  
            }
        }
    }

    private IEnumerator StartCountdown()
    {
        countdownActive = true;
        float remainingTime = countdownTime;

        if (countdownText != null)
        {
            countdownText.enabled = true;  
        }
        else
        {
            Debug.LogError("Countdown Text is not assigned in the inspector!");
            yield break;
        }

        while (remainingTime > 0 && countdownActive)
        {
            if (countdownText != null)
            {
                countdownText.text = Mathf.Ceil(remainingTime).ToString();
            }
            else
            {
                Debug.LogError("Countdown Text component is missing!");
                yield break;
            }
            remainingTime -= Time.deltaTime;
            yield return null;
        }

        if (countdownActive)
        {
            GameManager.Instance.StartGame();
        }
    }

    private bool IsPlayer(Collider2D collider)
    {
        return collider.GetComponent<PlayerInputController>() != null;
    }
}