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

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
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
    //public void ClosePerfectText()
    //{
    //    perfectText.SetActive(false);
    //}
    
    public void PopLongJumpText()
    {
        longJumpText.SetActive(true);
    }
    //public void CloseLongJumpText()
    //{
    //    longJumpText.SetActive(false);
    //}
    
}
