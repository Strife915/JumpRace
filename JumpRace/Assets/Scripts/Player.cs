using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Actor
{
    enum State { Transcending, Dying, Alive};
    State state = State.Alive;

    [SerializeField] Rigidbody myRigidBody;
    [SerializeField] Animator myAnimator;
    [SerializeField] LaserSight myLaserSight;
    [SerializeField] BoxCollider myBoxCollider;
    [SerializeField] ParticleSystem perfectParticle;

    [SerializeField] float speed;
    [SerializeField] float jump;
    [SerializeField] float longJump;
    [SerializeField] float rotationSpeed;
    [SerializeField] bool dragging = false;
    [SerializeField] bool isJumping;

    private void Start()
    {
        myAnimator = GetComponent<Animator>();
        myBoxCollider = GetComponent<BoxCollider>();
    }
    
    void Update()
    {
        if(state == State.Alive)
        {
            Move();
            DraggingUpdater();
            BlendTreeController();
        }       
    }

    


    private void OnCollisionEnter(Collision collision)
    {
        GameObject other = collision.gameObject;
        switch(other.tag)
        {
            case "JumpPad":
                if(other.GetComponent<JumpPad>().ReturnIsExplored())
                {
                    JumpProcess(other,jump);
                    other.SendMessage("JumpPadAnimation");
                }
                else
                {
                    JumpProcess(other,jump);
                    LevelManager.instance.IncreaseLevelScore(10);
                    other.SendMessage("IsExploredUpdate");
                    other.SendMessage("JumpPadAnimation");
                }
                break;
            case "PerfectZone":
                if(other.GetComponent<JumpPad>().ReturnIsExplored())
                {
                    JumpProcess(other, jump);
                }
                else
                {
                    JumpProcess(other, jump);
                    LevelManager.instance.IncreaseLevelScore(20);
                    UiManager.instance.PopPerfectText();
                    other.SendMessage("IsExploredUpdate");
                    perfectParticle.Play();   
                }
                break;

            case "LongJump":
                JumpProcess(other, 2500);
                UiManager.instance.PopLongJumpText();
                other.SendMessage("IsExploredUpdate");

                break;
            case "FinishZone":
                StartSuccesSequence();
                break;
            default:
                StartDeathSequence();
                break;
        }
    }
    void Move()
    {
        if (Input.GetMouseButton(0))
        {
            dragging = true;
            transform.position += transform.forward * speed * Time.deltaTime;
            if (dragging)
            {
                float y = Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;
                transform.Rotate(new Vector3(0, y, 0));
            }
        }

    }
    void JumpProcess(GameObject other,float jump)
    {
        if (isJumping) return;
        myAnimator.SetBool("Roll", true);
        isJumping = true;
        Debug.Log("Player zýpladý");
        myRigidBody.AddForce(Vector3.up * jump * Time.deltaTime,ForceMode.VelocityChange);
        ColliderUpdater();
    }
    void StartDeathSequence()
    {
        Debug.Log("Player Dead");
        UiManager.instance.PopDeathScreen();
        state = State.Dying;
        myLaserSight.myLaser.enabled = false;
    }
    void StartSuccesSequence()
    {
        LevelManager.instance.IncreaseLevelScore(100);
        LevelManager.instance.IncreaseLevelIndex();
        state = State.Transcending;
        UiManager.instance.PopSuccesScreen();
        myAnimator.SetTrigger("Succes");
        myLaserSight.myLaser.enabled = false;
        Debug.Log("Level Complete");
    }

    void DraggingUpdater()
    {
        if (Input.GetMouseButtonUp(0))
        {
            dragging = false;
        }
    }
    void BlendTreeController()
    {
        myAnimator.SetFloat("yVelocity", myRigidBody.velocity.y);
    }
    
    void ColliderUpdater()
    {
        if(myBoxCollider.enabled)
        {
            myBoxCollider.enabled = false;
        }
        else
        {
            myBoxCollider.enabled = true;
        }
    }
    void RollAnimationUpdate()
    {
        myAnimator.SetBool("Roll", false);
    }
    void JumpUpdater()
    {
        isJumping = false;
    }





}
