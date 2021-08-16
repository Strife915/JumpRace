using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class LevelManager : MonoBehaviour
{
    public static LevelManager instance = null;
    [SerializeField] Slider mySlider;
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
   
    






}
