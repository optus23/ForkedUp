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

    string login;
    string logout;
   

    private void Awake()
    {
        high_score_value = 0;
    }
    // Start is called before the first frame update
    void Start()
    {
        high_score = GetComponent<Text>();
        CreateText();
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

    private void OnApplicationQuit()
    {
        GameObject quadm = GameObject.Find("Quads");

        string path = Application.dataPath + "/HighScore.txt";

        logout = Environment.NewLine + "----Logout date:  " + System.DateTime.Now + "----" + Environment.NewLine;

        File.AppendAllText(path, logout);
    }

    void CreateText()
    {
        string path = Application.dataPath + "/HighScore.txt";
        //Create File 
        if (!File.Exists(path))
        {
            File.WriteAllText(path, "PlayTesting - Variables Parsing" + Environment.NewLine + "Player:  <Set number>" + Environment.NewLine + "Name:	<Set name>" + Environment.NewLine + "Age:	<Set age>" + Environment.NewLine + "Gender:	<Set Gender>" + Environment.NewLine + "Experience:	< Low / Medium / High >" + Environment.NewLine);
        }

        //Content of the file
        login = Environment.NewLine + "----Login date:  " + System.DateTime.Now + "----";

        //Add some to text to it
        File.AppendAllText(path, login);
    }
}
