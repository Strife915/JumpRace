using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
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

    private void Start()
    {
        myAnimator = GetComponent<Animator>();
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
                    Debug.Log("Normal Jump");
                    JumpProcess(other,jump);
                    other.SendMessage("JumpPadAnimation");
                    myAnimator.SetBool("Roll", true);
                }
                else
                {
                    Debug.Log("Normal Jump");
                    JumpProcess(other,jump);
                    myAnimator.SetBool("Roll", true);
                    LevelManager.instance.IncreaseLevelScore(10);
                    other.SendMessage("IsExploredUpdate");
                    other.SendMessage("JumpPadAnimation");
                }
                break;
            case "PerfectZone":
                JumpProcess(other, jump);
                myAnimator.SetBool("Roll", true);
                LevelManager.instance.IncreaseLevelScore(10);
                UiManager.instance.PopPerfectText();
                other.SendMessage("IsExploredUpdate");
                other.SendMessage("JumpPadAnimation");
                perfectParticle.Play();
                Debug.Log("Perfect Zone");
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
        if(Input.GetMouseButton(0))
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
        //transform.position += transform.up * jump * Time.deltaTime;
        myRigidBody.AddForce(Vector3.up * jump * Time.deltaTime,ForceMode.VelocityChange);
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
    void RollAnimationUpdate()
    {
        myAnimator.SetBool("Roll", false);
    }
    void BlendTreeController()
    {
        myAnimator.SetFloat("yVelocity", myRigidBody.velocity.y);
    }




}
