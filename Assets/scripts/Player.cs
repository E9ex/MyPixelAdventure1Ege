using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public class Player : MonoBehaviour
{
    private GameManager _gameManager;
    
    private EnemyController _enemyController;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] float speed = 7.5f;
    [SerializeField] private Animator anim;
 
    [SerializeField]  AudioSource applecrunchsound, applecrunchextrasound, playerdeathsound,enemydeathsound;
  
    [SerializeField]  GameObject BulletPrefab;
    [SerializeField]  Transform BulletSpawn;
    [SerializeField]  Transform BulletTile;
   

    
    
    private enum MovementState {idle, run,jump,fall}
// Start is called before the first frame update
    void Start()
    { 
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
         _gameManager.bestScoreText.text = _gameManager.bestscore.ToString();
          if (GameManager.isRestart)
          {
              _gameManager.startPanel.SetActive(false);
              _gameManager.bestScoreText.gameObject.SetActive(true);
          }
    }

    
    private void Update()
    {
        if (!GameManager.isStart)
            return;
        PlayerShoot();
    }

    private void FixedUpdate()
    {
        if (!GameManager.isStart )
            return;
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
            _gameManager.addPoints(5);
            
        }
        else if (col.tag == "AppleExtra")
        {
            applecrunchextrasound.Play();
            Destroy(col.gameObject,0.2f);
            _gameManager.addPoints(15);
            
        }
        else if (col.tag=="Enemy")
        {
            enemydeathsound.Play();
            _enemyController = col.GetComponentInParent<EnemyController>();
            _enemyController.killenemy();//20
           _gameManager.addPoints(20);
          
        }
        else if (col.CompareTag("End"))
        {
            _gameManager.NextLevel();
            _gameManager.scoreText.text = _gameManager.score.ToString();
            
        }
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag=="Death")
        {
            death(collision.gameObject);
    
            _gameManager.Lives();
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

    #region PlayerShoot
    void PlayerShoot()
    {
        if (Input.GetKeyDown(KeyCode.P))
        { 
            Instantiate(BulletPrefab, BulletSpawn.position, Quaternion.identity, BulletTile);
            
        }
    }
    

    #endregion
    
    
    #region olme işlemi

    public void death(GameObject p)
    {
            playerdeathsound.Play();
            Destroy(gameObject,0.5f);
            anim.SetTrigger("Death");
            
    }

    #endregion
    





}//class
