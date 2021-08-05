using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Joystick joystick;
    public Rigidbody myRigidBody;
    public float speed;
    public float jump;

    // Start is called before the first frame update
    
    private void FixedUpdate()
    {
        Vector3 direction = Vector3.forward * joystick.Vertical + Vector3.right * joystick.Horizontal;
        myRigidBody.AddForce(direction * speed * Time.fixedDeltaTime,ForceMode.VelocityChange);

    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag=="JumpPad")
        {
            Debug.Log("1");
            myRigidBody.AddForce(Vector3.up * jump * Time.fixedDeltaTime, ForceMode.VelocityChange);
            collision.gameObject.SendMessage("JumpPadColorUpdate");
        }
    }
}
