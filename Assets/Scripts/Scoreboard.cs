using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class    Scoreboard : MonoBehaviour
{
    [SerializeField] Text   happiness, population, score;

    public void refresh(Vector2 values) {
        population.text = values.x.ToString();
        happiness.text = values.y.ToString();
        score.text = (values.x * values.y).ToString();
    }
}
