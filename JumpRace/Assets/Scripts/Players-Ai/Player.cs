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
    [SerializeField] ParticleSystem rightFootParticle;
    [SerializeField] ParticleSystem leftFootParticle;
    [SerializeField] ParticleSystem finishParticle;
    [SerializeField] ScoreBoard myScoreBoard;
    [SerializeField] GameObject crown;

    [SerializeField] float speed;
    [SerializeField] float jump;
    [SerializeField] float longJump;
    [SerializeField] float rotationSpeed;
    [SerializeField] bool dragging = false;
    public bool isJumping;



    
    private void Awake()
    {
        myScoreBoard = FindObjectOfType<ScoreBoard>();
    }

    void Update()
    {  
        if (state == State.Alive && GameManager.instance.state == GameManager.gameState.Onhold || GameManager.instance.state == GameManager.gameState.isPlaying)
        {
            Move();
            DraggingUpdater();
            CheckPlayerRank();
        }
        if (myRigidBody.velocity.y < 0)
        {
            isJumping = false;
        }
    }


    private void OnCollisionEnter(Collision collision)
    {        
        GameObject other = collision.gameObject;
        switch (other.tag)
        {
            case "JumpPad":
                if (other.GetComponent<JumpPadExplore>().ReturnIsExplored())
                {
                    Jump(other, jump);
                    other.SendMessage("JumpPadAnimation");
                    other.SendMessage("JumpParticlePlay");
                }
                else
                {
                    Jump(other, jump);
                    LevelManager.instance.IncreaseLevelScore(10);
                    GetComponent<Actor>().IncreasePoint(10);
                    other.SendMessage("IsExploredUpdate");
                    other.SendMessage("JumpPadAnimation");
                    other.SendMessage("JumpParticlePlay");
                }
                break;
            case "PerfectZone":
                if (other.GetComponent<JumpPadExplore>().ReturnIsExplored())
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
                    other.SendMessage("JumpParticlePlay");
                    rightFootParticle.Play();
                    leftFootParticle.Play();                  
                }
                break;

            case "LongJump":
                if(other.GetComponent<JumpPadExplore>().ReturnIsExplored())
                {
                    Jump(other, longJump);
                    other.SendMessage("JumpPadAnimation");
                    other.SendMessage("JumpParticlePlay");
                }
                else
                {
                    Jump(other, longJump);
                    UiManager.instance.PopLongJumpText();
                    LevelManager.instance.IncreaseLevelScore(50);
                    GetComponent<Actor>().IncreasePoint(50);
                    other.SendMessage("IsExploredUpdate");
                    other.SendMessage("JumpPadAnimation");
                    other.SendMessage("JumpParticlePlay");
                }
                break;
            case "FinishZone":
                StartSuccesSequence();
                break;
            case "DeadZone":
                StartDeathSequence();
                break;
        }
    }
    void Jump(GameObject other, float jump)
    {
        if (isJumping) return;
        myAnimator.SetTrigger("Roll");
        isJumping = true;
        myRigidBody.AddForce(Vector3.up * jump , ForceMode.Impulse);
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
    void DraggingUpdater()
    {
        if (Input.GetMouseButtonUp(0))
        {
            dragging = false;
        }
    }
    private void CheckPlayerRank()
    {
        crown.SetActive(myScoreBoard.ReturnFirstPlace());
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
        finishParticle.Play();
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
    





}
