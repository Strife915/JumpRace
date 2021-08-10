using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class JumpPadColor : MonoBehaviour
{
    [SerializeField] Transform jumpPadTransform;

    [SerializeField] int touchCounter;
    [SerializeField] float animDuration;

    Renderer myRenderer;

    private void Start()
    {
        jumpPadTransform = gameObject.transform;
        //myRenderer = GetComponent<Renderer>();
    }
    //public void JumpPadColorUpdate()
    //{
    //    touchCounter++;
    //    switch(touchCounter)
    //    {
    //        case 1:
    //            myRenderer.material.color = Color.yellow;
    //            JumpPadAnimation();
    //            break;
    //        case 2:
    //            myRenderer.material.color = Color.red;
    //            JumpPadAnimation();
    //            break;
    //        case 3:
    //            Destroy(gameObject);
    //            JumpPadAnimation();
    //            break;   
    //    }
    //}
    public void JumpPadAnimation()
    {
        jumpPadTransform.DOShakePosition(animDuration, 0.5f, 50, 150, false, true);
    }
}
