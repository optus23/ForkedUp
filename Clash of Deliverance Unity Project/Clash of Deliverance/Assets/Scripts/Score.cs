using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    Text score;
    public static int score_value;
    public static bool player_pickup_score;

    private bool change_scale;
    private int scale_text = 85;
    private int change_count;   
    public int change_cont_limit;   

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



        if (player_pickup_score && score_value > 20) //  Size effect
        {
            if (change_scale)
            {
                scale_text += 4;
                score.fontSize = scale_text;
            }
            else
            {
                scale_text -= 2;
                score.fontSize = scale_text;

            }
            if (scale_text >= 110 || scale_text <= 75)
            {
                change_scale = !change_scale;
                change_count++;
            }

            if(change_count >= change_cont_limit)
            {
                player_pickup_score = false;
                Debug.Log(change_count);
                change_count = 0;
            }
        }
       
    }

    
}
