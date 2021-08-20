using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Line : MonoBehaviour
{
    LineRenderer myLine;

    [SerializeField] Transform[] jumpPads;
    // Start is called before the first frame update
    
    private void Start()
    {
        myLine = GetComponent<LineRenderer>();
        CreateAllLines();
    }

    private void CreateAllLines()
    {
        //myLine.GetComponent<LineRenderer>().positionCount = jumpPads.Length;
        myLine.positionCount = jumpPads.Length;
        for(int i=0; i < jumpPads.Length; i++)
        {
            myLine.SetPosition(i, jumpPads[i].position);
        }
    }


    
}
