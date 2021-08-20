using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actor : MonoBehaviour
{
    public int point;

    public void IncreasePoint(int value)
    {
        if(GameManager.instance.state==GameManager.gameState.isPlaying)
        point += value;
    }
}
