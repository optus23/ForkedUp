﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_1 : MonoBehaviour
{
    public float velocity;
    public float offset_camera_x;
    private bool change_direction;
    private int bounce = 1;
    public int bounce_number;

    public GameObject Enemy_Shot;
    public GameObject Mouth;

    public static bool dead;

    void Start()
    {
        bounce_number = Random.Range(1, 4);

        Shot();
    }

    void Update()
    {
        switch (bounce_number)
        {
            case 1:
                if (gameObject.transform.position.x <= Camera.main.transform.position.x - offset_camera_x && !MoveRight())
                {
                    ChangeDirection();
                }
                break;

            case 2:
                if (gameObject.transform.position.x <= Camera.main.transform.position.x - offset_camera_x && !MoveRight())
                {
                    ChangeDirection(); //  Go right

                }
                else if (gameObject.transform.position.x >= Camera.main.transform.position.x && !MoveLeft() && bounce < bounce_number)
                {
                    ChangeDirection();
                    bounce++;

                }
                    break;

            case 3:
                if (gameObject.transform.position.x <= Camera.main.transform.position.x - offset_camera_x && !MoveRight())
                {
                    ChangeDirection(); //  Go right

                }
                else if (gameObject.transform.position.x >= Camera.main.transform.position.x && !MoveLeft() && bounce < bounce_number)
                {
                    ChangeDirection();
                    bounce++;

                }
                break;
            default:
                break;
        }
        
        //  Main Direction
        if (change_direction)
        {
            MoveRight();
        }
        else
            MoveLeft();
        
    }


    bool MoveRight()
    {
        if (change_direction)
        {
            gameObject.transform.position = new Vector2(transform.position.x + velocity * Time.deltaTime, transform.position.y);
            return true;
        }
         return false;                 
    }

    bool MoveLeft()
    {
        if (change_direction)
        {
            return false;
        }
        else
        {
            gameObject.transform.position = new Vector2(transform.position.x - velocity * Time.deltaTime, transform.position.y);
        }
        return true;
    }

    void ChangeDirection()
    {
        if (change_direction)
        {
            change_direction = false;
        }
        else
            change_direction = true ;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            Destroy(gameObject);
            dead = true;
        }
    }

    void Shot()
    {
        if(!dead)
        {
            Invoke("Shot", 0.5f);
            Instantiate(Enemy_Shot, gameObject.transform.position, Quaternion.identity);
        }
    }
}
