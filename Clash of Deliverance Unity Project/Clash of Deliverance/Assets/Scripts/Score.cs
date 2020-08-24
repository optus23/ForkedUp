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
    private int value_animation;
    private int change_count;   
    public int change_cont_limit;   
    public int animation_score_number;
    Animator sizeUpScoreAnim;

    // Start is called before the first frame update
    void Start()
    {
        //-*-Start Debug-*-
        //score_value = 25;
        //-*-End Debug-*-


        value_animation = 5;
        if (TryGetComponent<Animator>(out var sizeUpScoreAnim))
        {
            //sizeUpScoreAnim = GetComponent<Animator>();
            sizeUpScoreAnim.Play("Score animation", 0, 1);

        }
        //sizeUpScoreAnim = GetComponent<Animator>();
        score = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {

        if (score_value >= 100) score_value = 100;
        score.text = "" + score_value;

        if (Score.score_value >= value_animation && TryGetComponent<Animator>(out var sizeUpScoreAnim))
        {
            value_animation += 5;
            sizeUpScoreAnim.Play("Score animation", 0, 0);
        }

        //if (player_pickup_score && score_value > animation_score_number) //  Size effect
        //{
        //    if (change_scale)
        //    {
        //        scale_text += 4;
        //        score.fontSize = scale_text;
        //    }
        //    else
        //    {
        //        scale_text -= 2;
        //        score.fontSize = scale_text;

        //    }
        //    if (scale_text >= 110 || scale_text <= 75)
        //    {
        //        change_scale = !change_scale;
        //        change_count++;
        //    }

        //    if(change_count >= change_cont_limit)
        //    {

        //        if(scale_text > 85)
        //            scale_text --;

        //        if(scale_text <= 85)
        //        {
        //            change_count = 0;
        //            player_pickup_score = false;
        //        }

        //        // Next Animation
        //        if (score_value > 20)
        //        {
        //            animation_score_number += 5;

        //        }
        //        else if(score_value > 50)
        //        {
        //            animation_score_number += 2;
        //        }
        //        else if(score_value > 80)
        //        {
        //            animation_score_number ++;
        //        }

        //    }
        //}

    }

    
}
