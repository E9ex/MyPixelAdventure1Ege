using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject Mute, unmute;
    [SerializeField] AudioSource backgroundsound;
    public static bool isRestart = false;
    

    #region yeniden oyna

   public void restartGame()
    {
        isRestart = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    #endregion

    #region cikis islemi
    public void quitGame()
    {
        Application.Quit();
        Debug.Log("çıktım bb");
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
}
