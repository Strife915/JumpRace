using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class JumpPad : MonoBehaviour
{
    [SerializeField] bool isExplored;
    [SerializeField] float animDuration;
    public bool ReturnIsExplored()
    {
        return isExplored;
    }
    public void IsExploredUpdate()
    {
        isExplored = true;
    }
    public void JumpPadAnimation()
    {
        transform.DOShakePosition(animDuration, 0.5f, 50, 150, false, true);
    }
}
