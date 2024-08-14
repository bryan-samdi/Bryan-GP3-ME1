using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public float damage = 10f; 
    public GameObject impactEffect; 

    void OnCollisionEnter2D(Collision2D collision)
    {
        Player2Health player2health = collision.transform.GetComponent<Player2Health>();
        if (player2health != null)
        {
            player2health.TakeDamage(damage); 
        }
        
        Player1Health player1health = collision.transform.GetComponent<Player1Health>();
        if (player1health != null)
        {
            player1health.TakeDamage(damage); 
        } 
        Destroy(gameObject); 
        GameObject impactGO = Instantiate(impactEffect, transform.position, Quaternion.identity);
        Destroy(impactGO, 1f); 

       
    }
}
