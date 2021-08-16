using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    enum State { Transcending, Dying, Alive };
    State state = State.Alive;

    [SerializeField] Rigidbody myRigidBody;
    [SerializeField] Animator myAnimator;
    [SerializeField] LaserSight myLaserSight;
    [SerializeField] BoxCollider myBoxCollider;
    [SerializeField] ParticleSystem perfectParticle;
    [SerializeField] ScoreBoard myScoreBoard;
    [SerializeField] GameObject crown;

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
        myScoreBoard = FindObjectOfType<ScoreBoard>();

    }

    void Update()
    {  
        if (state == State.Alive && GameManager.instance.state == GameManager.gameState.Onhold || GameManager.instance.state == GameManager.gameState.isPlaying)
        {
            Move();
            DraggingUpdater();
            BlendTreeController();
            Crown();
        }

    }


    private void OnCollisionEnter(Collision collision)
    {        
        GameObject other = collision.gameObject;
        switch (other.tag)
        {
            case "JumpPad":
                if (other.GetComponent<JumpPad>().ReturnIsExplored())
                {
                    Jump(other, jump);
                    other.SendMessage("JumpPadAnimation");
                }
                else
                {
                    Jump(other, jump);
                    LevelManager.instance.IncreaseLevelScore(10);
                    GetComponent<Actor>().IncreasePoint(10);
                    other.SendMessage("IsExploredUpdate");
                    other.SendMessage("JumpPadAnimation");
                }
                break;
            case "PerfectZone":
                if (other.GetComponent<JumpPad>().ReturnIsExplored())
                {
                    Jump(other, jump);
                }
                else
                {
                    Jump(other, jump);
                    LevelManager.instance.IncreaseLevelScore(20);
                    GetComponent<Actor>().IncreasePoint(20);
                    UiManager.instance.PopPerfectText();
                    other.SendMessage("IsExploredUpdate");
                    perfectParticle.Play();
                }
                break;

            case "LongJump":
                if(other.GetComponent<JumpPad>().ReturnIsExplored())
                {
                    Jump(other, longJump);
                }
                else
                {
                    Jump(other, 2500);
                    UiManager.instance.PopLongJumpText();
                    LevelManager.instance.IncreaseLevelScore(50);
                    GetComponent<Actor>().IncreasePoint(50);
                    other.SendMessage("IsExploredUpdate");
                }
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
            GameManager.instance.state = GameManager.gameState.isPlaying;
            dragging = true;
            transform.position += transform.forward * speed * Time.deltaTime;
            if (dragging)
            {
                float y = Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;
                transform.Rotate(new Vector3(0, y, 0));
            }
        }
    }
    void Jump(GameObject other, float jump)
    {
        if (isJumping) return;
        myAnimator.SetBool("Roll", true);
        isJumping = true;
        myRigidBody.AddForce(Vector3.up * jump * Time.deltaTime, ForceMode.VelocityChange);
        ColliderUpdater();
    }
    private void Crown()
    {
        if (myScoreBoard.ReturnFirstPlace())
        {
            crown.SetActive(true);
        }
        else if (!myScoreBoard.ReturnFirstPlace())
        {
            crown.SetActive(false);
        }
    }
    void StartDeathSequence()
    {
        Debug.Log("Player Dead");
        UiManager.instance.PopDeathScreen();
        GameManager.instance.state = GameManager.gameState.isOver;
        state = State.Dying;
        myLaserSight.myLaser.enabled = false;   
    }
    void StartSuccesSequence()
    {
        LevelManager.instance.IncreaseLevelScore(150);
        GetComponent<Actor>().IncreasePoint(250);
        LevelLoader.instance.IncreaseLevelIndex();
        state = State.Transcending;
        myAnimator.SetTrigger("Succes");
        myLaserSight.myLaser.enabled = false;
        if (myScoreBoard.ReturnFirstPlace())
        {
            UiManager.instance.PopSuccesScreen();
        }
        else if(!myScoreBoard.ReturnFirstPlace())
        {
            UiManager.instance.PopDeathScreen();
        }
        GameManager.instance.state = GameManager.gameState.isOver;
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
        if (myBoxCollider.enabled)
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
