using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class    Scoreboard : MonoBehaviour
{
    [SerializeField] Text   happiness, population, score;

    public void refresh(int h, int p) {
        happiness.text = h.ToString();
        population.text = p.ToString();
        score.text = (h * p).ToString();
    }
}
