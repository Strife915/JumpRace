using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class JumpPadAnimationControl : MonoBehaviour
{
    [SerializeField] Transform jumpPadTransform;
    [SerializeField] float animDuration;





    private void Start()
    {
        jumpPadTransform = gameObject.transform;
    }   
    public void JumpPadAnimation()
    {
        jumpPadTransform.DOShakePosition(animDuration, 0.5f, 50, 150, false, true);
    }
}
