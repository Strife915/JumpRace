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
        myAnimator = GetComponent<Animator>();
    }
    
    private void Update()
    {
        Move();
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
                GameManager.instance.state = GameManager.gameState.isOverbyAi;
                isFinished = true;
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
        pathIndex++;
        GetComponent<Actor>().IncreasePoint(10);
        myRigidBody.AddForce(Vector3.up * jumpForce * Time.deltaTime, ForceMode.VelocityChange);
        myAnimator.SetBool("Roll", true);
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
    
    
