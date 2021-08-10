using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserSight : MonoBehaviour
{


     public LineRenderer myLaser;

    

    private void Awake()
    {
        myLaser = GetComponent<LineRenderer>();
        
    }

    private void Update()
    {
        LaserUpdate();
    }

    private void LaserUpdate()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit))
        {
            if (hit.collider.CompareTag("JumpPad") || hit.collider.CompareTag("FinishZone"))
            {
                myLaser.SetPosition(1, new Vector3(0, -hit.distance, 0));
                myLaser.startColor = Color.green;
                myLaser.endColor = Color.green;
            }
            else if(hit.collider.CompareTag("PerfectZone"))
            {
                myLaser.SetPosition(1, new Vector3(0, -hit.distance, 0));
                myLaser.startColor = Color.yellow;
                myLaser.endColor = Color.yellow;
            }
            else
            {
                myLaser.SetPosition(1, new Vector3(0, -5000, 0));
                myLaser.startColor = Color.red;
                myLaser.endColor = Color.red;
            }
        }
    }
}
