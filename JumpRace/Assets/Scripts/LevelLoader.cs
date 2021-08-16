using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LevelLoader : MonoBehaviour
{
    [SerializeField] GameObject[] dontDestroyOnLoad;
    [SerializeField] GameObject[] Levels;
    //[SerializeField] GameObject player;
    [SerializeField] int levelIndex;
    public static LevelLoader instance = null;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);             
    }
    private void Start()
    {
        for (int i = 0; i < dontDestroyOnLoad.Length; i++)
        {
            DontDestroyOnLoad(dontDestroyOnLoad[i]);
        }
    }
    public void LoadLevel()
    {
        Instantiate(Levels[levelIndex]);
    }
    public void RestartScene()
    {
        SceneManager.LoadScene("JumpRace");
    }
    public void IncreaseLevelIndex()
    {
        levelIndex++;
    }

    public int ReturnLevelIndex()
    {
        return levelIndex;
    }
}
