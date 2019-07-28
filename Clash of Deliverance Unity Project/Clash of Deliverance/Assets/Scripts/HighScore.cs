using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScore : MonoBehaviour
{
    Text high_score;
    public static int high_score_value;

    private void Awake()
    {
        high_score_value = 0;
    }
    // Start is called before the first frame update
    void Start()
    {
        high_score = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Score.score_value > high_score_value)
        {
            high_score.text = "High Score: " + Score.score_value;
            high_score_value = Score.score_value;
        }

    }
}
