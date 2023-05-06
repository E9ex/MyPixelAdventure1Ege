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
        lastSceneIndex = PlayerPrefs.GetInt("lastPlayedSceneIndex",0);
        Debug.Log("LAST SCENE INDEX LOAD " + lastSceneIndex);
    }

    public void LoadScene()
    {
        SceneManager.LoadScene(lastSceneIndex);
        GameManager.isStart =true;
    }
}
