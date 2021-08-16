using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TMPro;

public class ScoreBoard : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI firstText, secondText, thirdText;
    [SerializeField] List<Actor> Actors = new List<Actor>();

    private void Awake()
    {
        Actors.AddRange(FindObjectsOfType<Actor>());
    }
    private void Update()
    {
        Sort();
        UpdateScore();
        
    }

    public void Sort()
    {
        Actors = Actors.OrderByDescending(x => x.point).ToList();
    }
    public void UpdateScore()
    {
        firstText.text = Actors[0].gameObject.name + " " + Actors[0].GetComponent<Actor>().point.ToString();
        secondText.text = Actors[1].gameObject.name + " " + Actors[1].GetComponent<Actor>().point.ToString();
        thirdText.text = Actors[2].gameObject.name + " " + Actors[2].GetComponent<Actor>().point.ToString();
    }
    public bool ReturnFirstPlace()
    {
        if(string.Equals(Actors[0].gameObject.name.ToString(),"Player"))
        {
            return true;
        }
        return false;
    }
}
