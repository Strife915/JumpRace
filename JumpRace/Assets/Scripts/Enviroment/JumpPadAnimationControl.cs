using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;



public class JumpPadAnimationControl : MonoBehaviour
{
    [SerializeField] ParticleSystem jumpEffectParticle;
    [SerializeField] float animDuration;
    public void JumpPadAnimation()
    {
        gameObject.transform.DOShakePosition(animDuration, 0.5f, 50, 150, false, true);
    }
    public void JumpParticlePlay()
    {
        jumpEffectParticle.Play();
    }
    
}
