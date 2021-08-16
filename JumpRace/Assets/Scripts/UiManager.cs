using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UiManager : MonoBehaviour
{
    public static UiManager instance = null;


    [SerializeField] GameObject tutorialText;
    [SerializeField] GameObject deathScreen;
    [SerializeField] GameObject succesScreen;
    [SerializeField] GameObject perfectText;
    [SerializeField] GameObject longJumpText;
    [SerializeField] TextMeshProUGUI currentLevelNumber;
    [SerializeField] TextMeshProUGUI nextLevelNumber;


    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }
    private void Start()
    {
        currentLevelNumber.text = LevelLoader.instance.ReturnLevelIndex()+1.ToString();
        nextLevelNumber.text = LevelLoader.instance.ReturnLevelIndex()+2.ToString();
    }
    private void Update()
    {
        TutorialTextUpdater();
        if(GameManager.instance.state == GameManager.gameState.isOverbyAi)
        {
            PopDeathScreen();
        }
    }

    public void PopDeathScreen()
    {
        deathScreen.SetActive(true);
    }
    public void CloseDeathScreen()
    {
        deathScreen.SetActive(false);
    }
    public void PopSuccesScreen()
    {
        succesScreen.SetActive(true);
    }
    public void CLoseSuccesScreen()
    {
        succesScreen.SetActive(false);
    }

    public void PopPerfectText()
    {
        perfectText.SetActive(true);
    }
    public void PopLongJumpText()
    {
        longJumpText.SetActive(true);
    }
    public void TutorialTextUpdater()
    {
        if(GameManager.instance.state== GameManager.gameState.Onhold)
        {
            tutorialText.SetActive(true);
        }
        else
        {
            tutorialText.SetActive(false);
        }
    }
    
    
    
    
}
