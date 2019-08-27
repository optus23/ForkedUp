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
    public int animation_score_number;   

    // Start is called before the first frame update
    void Start()
    {
        score = GetComponent<Text>();
        score_value = 34;
    }

    // Update is called once per frame
    void Update()
    {
        score.text = "" + score_value;


        
        if (player_pickup_score && score_value > animation_score_number) //  Size effect
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

                if(scale_text > 85)
                    scale_text --;
                
                if(scale_text <= 85)
                {
                    change_count = 0;
                    player_pickup_score = false;
                }

                // Next Animation
                if (score_value > 20)
                {
                    animation_score_number += 5;

                }
                else if(score_value > 50)
                {
                    animation_score_number += 2;
                }
                else if(score_value > 80)
                {
                    animation_score_number ++;
                }
                   
            }
        }
       
    }

    
}
