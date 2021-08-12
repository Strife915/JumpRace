using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelLoader_ : MonoBehaviour
{
    [SerializeField] GameObject[] Levels;
    [SerializeField] int levelIndex;

    private void Start()
    {
        Instantiate(Levels[levelIndex]);
    }

}
