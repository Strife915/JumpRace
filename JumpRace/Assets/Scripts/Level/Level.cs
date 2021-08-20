using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    private void Awake()
    {
        LevelLoader.instance.LoadLevel();
    }
}
