using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class    Scoreboard : MonoBehaviour
{
    [SerializeField] private Text   happiness, population, score;

    public void refresh(int p, int h) {
        print("sb " + p + " " + h);
        happiness.text = h.ToString();
        population.text = p.ToString();
        score.text = (h * p).ToString();
    }
}
