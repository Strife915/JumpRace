using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Ai : MonoBehaviour
{
    [SerializeField] float duration;
    [SerializeField] Transform myTransform;
    [SerializeField] Vector3[] path;
    [SerializeField] Transform[] myPath;
    [SerializeField] PathType pathType;
    [SerializeField] PathMode pathMode;

    private void Start()
    {
        myTransform = GetComponent<Transform>();
        for(int i=0;i<path.Length;i++)
        {
            path[i] = myPath[i].transform.position;
        }
        
        myTransform.DOPath(path, duration, pathType, pathMode, 10);
    }
    private void OnCollisionEnter(Collision collision)
    {
        //GameObject other = collision.gameObject;
        //switch (other.tag)
        //{
        //    case "JumpPad":
        //        if (other.GetComponent<JumpPad>().ReturnIsExplored())
        //        {
        //            JumpProcess(other, jump);
        //            other.SendMessage("JumpPadAnimation");
        //        }
        //        else
        //        {
        //            JumpProcess(other, jump);
        //            LevelManager.instance.IncreaseLevelScore(10);
        //            other.SendMessage("IsExploredUpdate");
        //            other.SendMessage("JumpPadAnimation");
        //        }
        //        break;
        //    case "PerfectZone":
        //        if (other.GetComponent<JumpPad>().ReturnIsExplored())
        //        {
        //            JumpProcess(other, jump);
        //        }
        //        else
        //        {
        //            JumpProcess(other, jump);
        //            LevelManager.instance.IncreaseLevelScore(20);
        //            UiManager.instance.PopPerfectText();
        //            other.SendMessage("IsExploredUpdate");
        //            perfectParticle.Play();
        //        }
        //        break;

        //    case "LongJump":
        //        JumpProcess(other, 2500);
        //        UiManager.instance.PopLongJumpText();
        //        other.SendMessage("IsExploredUpdate");

        //        break;
        //    case "FinishZone":
        //        StartSuccesSequence();
        //        break;
        //    default:
        //        StartDeathSequence();
        //        break;
        //}
    }
}
