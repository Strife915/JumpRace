using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPadColor : MonoBehaviour
{
    [SerializeField] int touchCounter;
    private Renderer myRenderer;

    private void Awake()
    {
        myRenderer = GetComponent<Renderer>();
    }

    // Start is called before the first frame update
    public void JumpPadColorUpdate()
    {
        touchCounter++;
        switch(touchCounter)
        {
            case 1:
                myRenderer.material.color = Color.yellow;
                break;
            case 2:
                myRenderer.material.color = Color.red;
                break;
            case 3:
                Destroy(gameObject);
                break;
                
        }
    }
}
