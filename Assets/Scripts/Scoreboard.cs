using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class    Scoreboard : MonoBehaviour
{
    [SerializeField] Text   happiness, population, score;

    public void refresh(Vector2 values) {
        happiness.text = values.x.ToString();
        population.text = values.y.ToString();
        score.text = (values.x * values.y).ToString();
    }
}
