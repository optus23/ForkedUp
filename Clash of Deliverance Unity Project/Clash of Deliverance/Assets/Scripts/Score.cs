using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public static int score_value;
    Text score;
    // Start is called before the first frame update
    void Start()
    {
        score = GetComponent<Text>();
        score_value = 0;
    }

    // Update is called once per frame
    void Update()
    {
        score.text = "" + score_value;
    }
}
