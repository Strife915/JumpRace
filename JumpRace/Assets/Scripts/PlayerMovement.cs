using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] Joystick myJoyStick;
    [SerializeField] Rigidbody myRigidBody;
    [SerializeField] Animator myAnimator;

    private void Start()
    {
        myAnimator = GetComponent<Animator>();
    }

    [SerializeField] float speed;
    [SerializeField] float jump;
    [SerializeField] float rotationSpeed;
    [SerializeField] bool dragging = false;

    
    void Update()
    {
        
        DraggingUpdater();
        BlendTreeController();
    }

    void FixedUpdate()
    {
        MouseInput();
        if (dragging)
        {
            float y = Input.GetAxis("Mouse X") * rotationSpeed * Time.fixedDeltaTime;
            transform.Rotate(new Vector3(0, y, 0));
        }
        
    }
    
    void MouseInput()
    {
        if(Input.GetMouseButton(0))
        {
            dragging = true;
            Move();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("JumpPad"))
        {
            myRigidBody.AddForce(Vector3.up * jump * Time.fixedDeltaTime, ForceMode.VelocityChange);
            collision.gameObject.SendMessage("JumpPadColorUpdate");
            myAnimator.SetBool("Roll", true);
            LevelManager.instance.IncreaseLevelScore(10);
        }
        else if(collision.gameObject.CompareTag("FinishZone"))
        {
            Debug.Log("Level Complete");
        }
        else if(collision.gameObject.CompareTag("DeadZone"))
        {
            Debug.Log("Player died");
        }
    }

    private void Move()
    {
        
            myRigidBody.MovePosition(myRigidBody.position + (transform.forward * speed * Time.deltaTime));   
    }

    void DraggingUpdater()
    {
        if (Input.GetMouseButtonUp(0))
        {
            dragging = false;
        }
    }

    public void RollAnimationUpdate()
    {
        myAnimator.SetBool("Roll", false);
    }

    public void BlendTreeController()
    {
        myAnimator.SetFloat("yVelocity", myRigidBody.velocity.y);
    }

}
