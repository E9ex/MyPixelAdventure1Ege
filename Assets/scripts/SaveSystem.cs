using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SaveSystem : MonoBehaviour
{
    private GameManager Manager;
    private int lastSceneIndex = 0;
    private void Awake()
    {
        Manager = GetComponent<GameManager>();
        lastSceneIndex = PlayerPrefs.GetInt("lastPlayedSceneIndex",SceneManager.GetActiveScene().buildIndex+1);// defaulu boş bırak bi kere 
        PlayerPrefs.GetInt("bbestscore");
    }

    public void LoadScene()
    {
        SceneManager.LoadScene(lastSceneIndex);
        GameManager.isRestart = true;
        GameManager.isStart = true;
    }
}
