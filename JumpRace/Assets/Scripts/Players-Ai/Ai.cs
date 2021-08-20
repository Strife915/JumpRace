using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Ai : MonoBehaviour
{
    enum State  {Alive,Dead};
    State state = State.Alive;
    [SerializeField] Rigidbody myRigidBody;
    [SerializeField] Animator myAnimator;

    [SerializeField] float jumpForce;
    [SerializeField] float speed;
    [SerializeField] bool isJumping;
    [SerializeField] bool isFinished;

    [SerializeField] Transform[] path;
    [SerializeField] int pathIndex;

    private void Start()
    {
        myRigidBody = GetComponent<Rigidbody>();
        
    }
    
    private void Update()
    {
        Move();
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
                Jump();
                break;
            case "PerfectZone":
                Jump();
                break;
            case "FinishZone":
                if(GameManager.instance.state != GameManager.gameState.isOver)
                {
                    GameManager.instance.state = GameManager.gameState.isOverbyAi;
                }
                isFinished = true;
                myAnimator.SetTrigger("Win");
                break;
            case "DeadZone":
                state = State.Dead;
                Debug.Log(gameObject.name + " Dead");
                break;
        }
    }
    void Jump()
    {
        if (isJumping || isFinished) return;
        isJumping = true;
        
        GetComponent<Actor>().IncreasePoint(10);
        myRigidBody.AddForce(Vector3.up * jumpForce , ForceMode.Impulse);
        myAnimator.SetTrigger("Roll");
        if(GameManager.instance.state==GameManager.gameState.isPlaying)
        {
            pathIndex++;
        }
    }
    void Move()
    {
        if (GameManager.instance.state == GameManager.gameState.isPlaying && state == State.Alive)
        transform.position = Vector3.Lerp(transform.position, path[pathIndex].position, speed * Time.deltaTime);
    }
    void RollUpdater()
    {
        myAnimator.SetBool("Roll", false);
    }
    void IsJumpingUpdater()
    {
        isJumping = false;
    }
}
    
    
