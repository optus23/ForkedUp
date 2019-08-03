using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System;

public class HighScore : MonoBehaviour
{
    Text high_score;
    public static int high_score_value;
    string high_score_string;

    //string logout;
   

    private void Awake()
    {
        //CreateText();
    }
    // Start is called before the first frame update
    void Start()
    {
        high_score = GetComponent<Text>();
        high_score_value = PlayerPrefs.GetInt("HighScore", 0);
        high_score.text = "High Score: " + high_score_value;
        Debug.Log(high_score_value);


    }

    // Update is called once per frame
    void Update()
    {
        if (Score.score_value > high_score_value)
        {
            high_score_value = Score.score_value;
            high_score.text = "High Score: " + high_score_value;
            PlayerPrefs.SetInt("HighScore", high_score_value);

        }
    }

    //void CreateText()
    //{
    //    string path = Application.dataPath + "/HighScore.txt";
    //    //Create File 
    //    if (!File.Exists(path))
    //    {
    //        File.WriteAllText(path, "High Score Numbers" + Environment.NewLine);
    //    }
    //}

    //private void OnApplicationQuit()
    //{
    //    string path = Application.dataPath + "/HighScore.txt";

    //    high_score_string = Environment.NewLine + "High Score: " + high_score_value;
    //    File.AppendAllText(path, high_score_string);

    //}
}
