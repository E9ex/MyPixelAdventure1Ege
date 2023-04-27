using System;
using Microsoft.Unity.VisualStudio.Editor;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;
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
    public  int score = 0;
    public int bestscore = 0;
    public static bool isStart = false;
    [SerializeField]  GameObject player;
    [SerializeField]  Transform spawnPoint;
    
    
    #region yeniden oyna

    private void Awake()
    {
        SpawnPlayer();
        isStart = true;
    }

    #region PlayerHealth
    public void Lives()
    {
        playerLife--;
        Destroy(PlayerLives[playerLife]);
        
        if (playerLife < 1)
        {
            restartPanel.SetActive(true);
            lastScoreText.text = "Last Score: "+score.ToString();
        }
    }
    #endregion

    public void restartGame()
    {
        isRestart = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    #endregion

    void SpawnPlayer()
    {
        Instantiate(player, spawnPoint.position, quaternion.identity);
    }
   public void ReSpawnPlayer()
    {
        Instantiate(player, spawnPoint.position, quaternion.identity);
    }

    #region cikis islemi
    public void quitGame()
    {
        Application.Quit();
        Debug.Log("çıktım bb");
    }
    #endregion
    

    #region levelgecme

    public  static void skipLevel()
    {
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
    public void PlayGame()
    {
             isStart = true;
        startPanel.SetActive(false);
    }


    #endregion
        
}