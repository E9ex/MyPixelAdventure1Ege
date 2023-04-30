using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyBody : MonoBehaviour
{

    public int health = 100;
    public void Takedamage(int damage)
    {
        health -=damage ;
        if (health <= 0)
        {
            Die();
            GameManager manager = FindObjectOfType<GameManager>();
            manager.score += 8;
            manager.scoreText.text = manager.score.ToString();
            
        }
    } 
    public void Die()
    {
        Destroy(transform.parent.gameObject);
    }
   
    
    
}
