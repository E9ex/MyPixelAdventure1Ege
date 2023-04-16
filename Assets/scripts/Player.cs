using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public EnemyController _enemyController;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] float speed = 7.5f;
    [SerializeField] private Animator anim;
    [SerializeField] Text scoreText,lastScoreText;
    [SerializeField]  GameObject restartPanel, startPanel;
    [SerializeField]  AudioSource applecrunchsound, applecrunchextrasound, playerdeathsound;
    public static bool isStart = false;
    private int score = 0;
    
    
    

    private enum MovementState {idle, run,jump,fall}
// Start is called before the first frame update
    void Start()
    {
        scoreText.text = score.ToString();
        if (GameManager.isRestart)
        {
            startPanel.SetActive(false);
        }
    } 
    private void FixedUpdate()
    {
        if (!isStart)
        {
            return;
        }
        float h = Input.GetAxis("Horizontal");
        Move(h);
        PlayerTurn(h);
        PlayerAnim(h);
       
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Apple")
        {
            applecrunchsound.Play();
            Destroy(col.gameObject,0.2f);
            score += 5;
            scoreText.text = score.ToString();
        }
        else if (col.tag == "AppleExtra")
        {
            applecrunchextrasound.Play();
            Destroy(col.gameObject,0.2f);
            score += 15;
            scoreText.text = score.ToString();
        }
        else if (col.tag=="Enemy")
        {
            _enemyController = col.GetComponentInParent<EnemyController>();
            _enemyController.killenemy();
            
           score +=20;
           scoreText.text = score.ToString();
           Debug.Log("player col.tag==Enemyden geliyorum.");
        }
    }


     public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag=="Death")
        {
            death(collision.gameObject);
            Debug.Log("lan nerden geliyorum dur bakiyim player oncollisionEnter2d");
        }
    }   
    
     

    #region hareket islemleri
    void Move(float h)
    {
        rb.velocity = new Vector2(h*speed,rb.velocity.y);
    }
    #endregion

    #region karakter yon islemi
    void PlayerTurn(float h)
    {
        if (h > 0)
            transform.localScale = new Vector3(1f, 1f, 1f);
        else if (h < 0)
            transform.localScale = new Vector3(-1f, 1f, 1f);
    }
    #endregion

    
    #region animasyon işlemleri 
    void PlayerAnim(float h)
    {
        MovementState state;
        if (h != 0)
            state = MovementState.run;
        else
            state = MovementState.idle;
        if (rb.velocity.y > .1f)
            state = MovementState.jump;
        else if (rb.velocity.y < -.1f)
            state = MovementState.fall;

        anim.SetInteger("state",(int)state);
    }
    #endregion
    
    
    #region olme işlemi

    public void death(GameObject p)
    {
            playerdeathsound.Play();
            Destroy(gameObject,0.5f);
            anim.SetTrigger("Death");
            restartPanel.SetActive(true);
            lastScoreText.text = "Last Score: " + score.ToString();
            
 
    }

    #endregion

    #region oyunubaslatma

    public void PlayGame()
    {
        isStart = true;
        startPanel.SetActive(false);
    }

    #endregion
    
}//class
