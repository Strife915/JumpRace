using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishZone : MonoBehaviour
{
    [SerializeField] ParticleSystem frontLeft;
    [SerializeField] ParticleSystem frontRight;
    [SerializeField] ParticleSystem backLeft;
    [SerializeField] ParticleSystem backRight;

    
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            frontLeft.Play();
            frontRight.Play();
            backLeft.Play();
            backRight.Play();
        }
    }
}
