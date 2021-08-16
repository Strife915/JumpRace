using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actor : MonoBehaviour
{
    public int point;
    [SerializeField] ScoreBoard myScoreBoard;

    public void IncreasePoint(int value)
    {
        if(GameManager.instance.state==GameManager.gameState.isPlaying)
        point += value;
    }
}
