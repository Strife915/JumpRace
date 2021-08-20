using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class JumpPadExplore : MonoBehaviour
{
    [SerializeField] bool isExplored;
    public bool ReturnIsExplored()
    {
        return isExplored;
    }
    public void IsExploredUpdate()
    {
        isExplored = true;
    }

}
