using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_1 : MonoBehaviour
{
    private bool exit_battlefield;

    public float velocity;
    public float offset_camera_x;
    private bool change_direction;
    private bool stop;
    private float stop_timer;
    public float time_stopped;
    public int bounce_number;

    public bool dead;

    private bool can_first_shot;
    private bool shot_once;
    private bool can_second_shot;
    private bool first_shot = true;
    private float first_shot_position;
    private float second_shot_position;
    public GameObject Kamehameha_shot;

    public int life;

    void Start()
    {
        bounce_number = Random.Range(1, 3);
        Debug.Log(life);

    }

    void Update()
    {
         
        if (gameObject.transform.position.x >= Camera.main.transform.position.x + offset_camera_x && !stop)
        {
            MoveLeft();
            MoveDown();

            if(first_shot)
            {
                first_shot_position = Random.Range(Camera.main.transform.position.x - offset_camera_x*2, Camera.main.transform.position.x + offset_camera_x);
                can_first_shot = true;
                first_shot = false;

            }
        }
        else if (gameObject.transform.position.x < Camera.main.transform.position.x + offset_camera_x && !stop)
        {
            if (gameObject.transform.position.y <= Camera.main.transform.position.x - offset_camera_x * 2)
            {
                change_direction = true;
                can_second_shot = true;
                second_shot_position = Random.Range(Camera.main.transform.position.x - offset_camera_x , Camera.main.transform.position.x + offset_camera_x*2);

            }
            else if (!change_direction)
                MoveDown();

        }
       
        //Main Direction
        if(change_direction && !stop)
        {
            MoveUp();
        }


        // First Kamehameha
        if (gameObject.transform.position.y <= first_shot_position && can_first_shot)
        {
            can_first_shot = false;
            stop = true;
            shot_once = true;

        }

        // Second Kamehameha
        if (gameObject.transform.position.y >= second_shot_position && can_second_shot)
        {
            can_second_shot = false;
            stop = true;
            shot_once = true;

        }

        // Enemy_stopped
        if (stop) 
        {
            stop_timer += Time.deltaTime;
            if (stop_timer >= time_stopped/8)
            {
                if(shot_once)
                {
                    Kamehameha();
                    shot_once = false;
                }
                if (stop_timer >= time_stopped)
                {
                    stop = false;
                    stop_timer = 0;
                }

            }
        }
       

    }


    void MoveDown()
    {
        gameObject.transform.position = new Vector2(transform.position.x, transform.position.y - velocity * Time.deltaTime);
    }

    void MoveUp()
    {
        gameObject.transform.position = new Vector2(transform.position.x, transform.position.y + velocity * Time.deltaTime);
    }

    void MoveLeft()
    {
        gameObject.transform.position = new Vector2(transform.position.x- velocity * Time.deltaTime, transform.position.y );
    }

    void Kamehameha()
    {
        if (!dead)
        {
            Instantiate(Kamehameha_shot, new Vector3 (gameObject.transform.position.x - 3, gameObject.transform.position.y, gameObject.transform.position.z), Kamehameha_shot.transform.rotation);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            if(life <= 0)
            {
                Destroy(gameObject);
                dead = true;
            }          
        }
    }

    
}
