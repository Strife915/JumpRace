using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance = null;
    [SerializeField] Slider mySlider;
    [SerializeField] GameObject[] Levels;
    [SerializeField] GameObject[] dontDestroy;

    [SerializeField] int levelIndex;
    [SerializeField] int levelMaxScore;
    [SerializeField] int levelScore;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    private void Start()
    {
        mySlider.maxValue = levelMaxScore;
        mySlider.value = levelScore;
    }

    public void IncreaseLevelScore(int jumpPoint)
    {
        if(levelScore < levelMaxScore)
        {
            levelScore += jumpPoint;
            mySlider.value = levelScore;
        }
    }
    public void LoadFirstLevel()
    {
        Debug.Log(levelIndex);
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
    public void LoadNextLevel()
    {
        LoadFirstLevel();
        Instantiate(Levels[levelIndex]);
        SceneManager.LoadScene("Sandbox");
        for(int i=0; i<dontDestroy.Length;i++)
        {
            DontDestroyOnLoad(dontDestroy[i]);
        }
    }
    

}
