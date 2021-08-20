using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TMPro;

public class ScoreBoard : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI firstText, secondText, thirdText;
    [SerializeField] List<Actor> Actors = new List<Actor>();

    private void Start()
    {
        Actors.AddRange(FindObjectsOfType<Actor>());
    }
    private void Update()
    {
        UpdateScore();
    }

    public void Sort()
    {
        Actors = Actors.OrderByDescending(x => x.point).ToList();
    }
    public void UpdateScore()
    {
        Sort();
        firstText.text = "1 : " + Actors[0].gameObject.name;
        secondText.text = "2 : " + Actors[1].gameObject.name;
        thirdText.text = "3 : " + Actors[2].gameObject.name;
    }
    public bool ReturnFirstPlace()
    {
        return string.Equals(Actors[0].gameObject.name.ToString(), "Player");
    }
}
