using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player2Health : MonoBehaviour
{
    private float player2health;
    private float lerpTimer;
    private float maxHealth = 100f;
    public float chipSpeed = 2f;

    public Image frontHealthBar, backHealthBar;


    private void Start()
    {
        player2health = maxHealth;
    }

    private void Update()
    {
        player2health = Mathf.Clamp(player2health, 0, maxHealth);
        UpdateHealthUI();
    }

    public void UpdateHealthUI()
    {
        float fillF = frontHealthBar.fillAmount;
        float fillB = backHealthBar.fillAmount;
        float hFraction = player2health / maxHealth;

        if (fillB > hFraction)
        {
            frontHealthBar.fillAmount = hFraction;
            backHealthBar.color = Color.yellow;
            lerpTimer += Time.deltaTime;
            float percentComplete = lerpTimer / chipSpeed;
            backHealthBar.fillAmount = Mathf.Lerp(fillB, hFraction, percentComplete);
        }
        else if (fillF < hFraction)
        {
            backHealthBar.fillAmount = hFraction;
            backHealthBar.color = Color.green;
            lerpTimer += Time.deltaTime;
            float percentComplete = lerpTimer / chipSpeed;
            frontHealthBar.fillAmount = Mathf.Lerp(fillF, hFraction, percentComplete);
        }
    }

    public void TakeDamage(float damage)
    {
        // Debug.Log(gameObject.name + "Took Damage");
        player2health -= damage;
        lerpTimer = 0f;

        if (player2health < 0f)
        {
            Die();
        }

    }

    void Die()
    {
        // if (gameObject.CompareTag("Player1"))
        // {
        //     GameManager.Instance.EndRound(2);
        // }
        // else if (gameObject.CompareTag("Player2"))
        // {
        //     GameManager.Instance.EndRound(1);
        // }

        Destroy(gameObject);
    }
}
