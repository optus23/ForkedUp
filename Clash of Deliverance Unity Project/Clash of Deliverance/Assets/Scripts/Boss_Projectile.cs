﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Projectile : MonoBehaviour
{
   
    [SerializeField]
    private float velocity;
    private GameObject Player;
    private Boss_Manager Boss;
    private Vector2 distance;

    private float directional_angle;
    private bool calculate_directional_angle;

    void Start()
    {      
        Boss = gameObject.GetComponentInParent<Boss_Manager>();
        Player = GameObject.FindGameObjectWithTag("Player");

        distance = Player.transform.position - transform.position;
        calculate_directional_angle = true;

        Destroy(gameObject, 3);
    }

    void LateUpdate()
    {

        if (Boss.type1_state != Boss_Manager.Type1State.NONE)  //Following bulets
        {
            //Calculates trigonometric angle with player and move fordward
            float angle = Mathf.Atan(distance.y / distance.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle);

            transform.Translate(new Vector3(-velocity /** 1.3f*/ * Time.deltaTime, 0, 0));
        }

        if (Boss.type2_state != Boss_Manager.Type2State.NONE) //Directional bulets
        {
            if(calculate_directional_angle)
            {
                if(Boss.type2_state == Boss_Manager.Type2State.PHASE_1)
                {
                    directional_angle = 280 + (Boss.number_of_directional_shots * 10); //  Up - Down
                }
                else if (Boss.type2_state == Boss_Manager.Type2State.PHASE_2)
                {
                    directional_angle = 55 - (Boss.number_of_directional_shots * 10); //  Down - Up
                }
                calculate_directional_angle = false;
            }
            transform.rotation = Quaternion.Euler(0, 0,  directional_angle);
            transform.Translate(new Vector3(-velocity * Time.deltaTime, 0, 0));
        }      
    }
}
