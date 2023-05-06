using System;
using System.Collections;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Image = UnityEngine.UI.Image;

public class GameManager : MonoBehaviour
{
    public static GameManager Manager;
    [SerializeField] Button Continue;
    [SerializeField] GameObject Mute, unmute;
    [SerializeField] private Image[] PlayerLives;
    [SerializeField] private int playerLife = 3;
    [SerializeField] public Text scoreText,lastScoreText,bestScoreText;
    public GameObject restartPanel, startPanel;
    public static bool isRestart = false;
    [SerializeField] int score = 0;
    public int Score
    {
        get => score;
        set
        {
            score = value;
            PlayerPrefs.SetInt("score", score);
            scoreText.text = score.ToString();
        }
    }
    
    [SerializeField] int bestscore = 0;

    public int BestScore
    {
        get => bestscore;
        set
        {
            bestscore = value;
            PlayerPrefs.SetInt("BestScore", bestscore);
            bestScoreText.text = "Best Score: " + bestscore;
        }
    }
    public static bool isStart = false;
    [SerializeField] GameObject player;
    [SerializeField] Transform spawnPoint;

    private void Awake()
    {
        playerLife = PlayerPrefs.GetInt("PlayerLife", 3);
        BestScore = PlayerPrefs.GetInt("BestScore", 0);
        Score = PlayerPrefs.GetInt("score");

        if(Continue)
            if (BestScore > 0 || Score > 0 || playerLife != 3)
                Continue.interactable = true;
        
        if (playerLife == 0)
        {
            PlayerPrefs.SetInt("PlayerLife", 3);
            Score = 0;
            playerLife = 3;
        }
        
        for (int i = 0; i < 3; i++)
        {
            if(i<playerLife)
                PlayerLives[i].gameObject.SetActive(true);
            else
                PlayerLives[i].gameObject.SetActive(false);
        }
    }

    private void Start()
    {
        SpawnPlayer();
    }

    private void ResetValues()
    {
        PlayerPrefs.SetInt("PlayerLife", 3);
    }

    public void addPoints(int point)
    {
        Score += point;
        if (bestscore < score)
            BestScore = score;
    }
    #region PlayerHealth
    public void ReduceLives()
    {
        playerLife--;
        PlayerPrefs.SetInt("PlayerLife", playerLife);
        PlayerLives[playerLife].gameObject.SetActive(false);
        if (playerLife < 1 )
        {
            isStart = false;
            restartPanel.SetActive(true);
            startPanel.SetActive(false);
            lastScoreText.text = "Last Score: " + score;
            PlayerPrefs.SetInt("lastPlayedSceneIndex", SceneManager.GetActiveScene().buildIndex);
            return;
        }
        StartCoroutine(Delay());
    }
    #endregion

    #region yenidenBaslatma
    
    public void RestartGame()
    {
        isStart = true;
        startPanel.SetActive(false);
        restartPanel.SetActive(false);
        isRestart = true;
        ResetValues();
        Score = 0;
        PlayerPrefs.SetInt("lastPlayedSceneIndex",0);
        SceneManager.LoadScene("level1");
        
         
    }
    #endregion

    #region SpawnveReSpawnIslemleri
    void SpawnPlayer()
    {
        Debug.Log("Player Spawn");
        Instantiate(player, spawnPoint.position, quaternion.identity);
    }
   public void ReSpawnPlayer()
    {
        Debug.Log("Player Respawn");
        Instantiate(player, spawnPoint.position, quaternion.identity);
    }
   
   IEnumerator Delay()
   {
       yield return new WaitForSeconds(1f);
       ReSpawnPlayer();
   }
   #endregion

   #region cikis islemi
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("çıktım bb");
        
        ResetValues();
        PlayerPrefs.Save();
    }
    #endregion

    public void BackToStart()
    {
        SceneManager.LoadScene(0);
    }

    #region levelgecme

    public void NextLevel()
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
        Score = 0;
    }

    #endregion

    #region KapatmaIslemleri

    private void OnDisable()
    {
        PlayerPrefs.SetInt("lastPlayedSceneIndex", SceneManager.GetActiveScene().buildIndex);
    }

    #endregion
   
    


   
        
}