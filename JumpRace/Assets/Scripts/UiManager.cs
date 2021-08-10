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
    public void PopSuccesScreen()
    {
        succesScreen.SetActive(true);
    }

    public void PopPerfectText()
    {
        perfectText.SetActive(true);
    }
    public void ClosePerfectText()
    {
        perfectText.SetActive(false);
    }
    
    
}
