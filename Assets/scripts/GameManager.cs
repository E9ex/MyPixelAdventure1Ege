using System;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;
using Image = UnityEngine.UI.Image;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject Mute, unmute;

    
    [SerializeField] private Image[] PlayerLives;
    [SerializeField] private int playerLife = 3;
    [SerializeField] public Text scoreText,lastScoreText,bestScoreText;
    public GameObject restartPanel, startPanel;
    public static bool isRestart = false;
    public   int score = 0;
    public int bestscore = 0;
    public static bool isStart = false;
    [SerializeField]  GameObject player;
    [SerializeField]  Transform spawnPoint;
    
    
    

    private void Awake()
    {
        bestscore = PlayerPrefs.GetInt("BestScore"); 
        score = PlayerPrefs.GetInt("score"); 
        SpawnPlayer();
    }
    
    public void addPoints(int point)
    {
        score += point;
        scoreText.text = score.ToString();
        PlayerPrefs.SetInt("score",score);
        if (bestscore<score)
        {
            PlayerPrefs.SetInt("BestScore",score);
        }
      
    }
  #region PlayerHealth
    public void Lives()
    {
        playerLife--;
        Destroy(PlayerLives[playerLife]);
        if (playerLife < 1 )
        {
            isStart = false;
            restartPanel.SetActive(true);
            startPanel.SetActive(false);
            lastScoreText.text = "Last Score: "+score.ToString();
            bestScoreText.gameObject.SetActive(true);//true
        }
       
        
    }
    #endregion

    private void Update()
    {
       bestscore = PlayerPrefs.GetInt("BestScore");
       bestScoreText.text = "Best Score: " + bestscore;
       scoreText.text = score.ToString();
    }

    #region yenidenBaslatma
    
    public void restartGame()
    {
        isStart = true;
        startPanel.SetActive(false);
        restartPanel.SetActive(false);
        isRestart = true; 
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        score = 0;
        
        
    }
    #endregion

    #region SpawnveReSpawnIslemleri
    void SpawnPlayer()
    {
        Instantiate(player, spawnPoint.position, quaternion.identity);
    }
   public void ReSpawnPlayer()
    {
        Instantiate(player, spawnPoint.position, quaternion.identity);
    }
   #endregion

    #region cikis islemi
    public void quitGame()
    {
        Application.Quit();
        Debug.Log("çıktım bb");
    }
    #endregion
    

    #region levelgecme

    public   void NextLevel()
    {
        isStart = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    #endregion
  

    #region  mute işlemleri

    public void mute()
    {
        unmute.SetActive(true);
        Mute.SetActive(false);
    }

    public void unMute()
    {
        unmute.SetActive(false);
        Mute.SetActive(true);
    } 
    #endregion

    #region StartIslemleri

    public void PlayGame()
    {
        isStart = true;
        startPanel.SetActive(false);
        score = 0;
    }

    #endregion
    


   
        
}