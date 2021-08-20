using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    Player myPlayerScript;
    Animator myAnimator;

    private void Start()
    {
        myPlayerScript = FindObjectOfType<Player>();
        myAnimator = GetComponent<Animator>();
        
    }

    void IsjumpingUpdate()
    {
        myPlayerScript.isJumping = false;
    }
    
}
