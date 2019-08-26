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

    private bool can_shot;
    private float shot_position;
    public GameObject Kamehameha_shot;

    void Start()
    {
        bounce_number = Random.Range(1, 4);

    }

    void Update()
    {
         
        if (gameObject.transform.position.x >= Camera.main.transform.position.x + offset_camera_x && !stop)
        {
            MoveLeft();
            MoveDown();

        }
        else if (gameObject.transform.position.x < Camera.main.transform.position.x + offset_camera_x && !stop)
        {
            if (gameObject.transform.position.y <= Camera.main.transform.position.x - offset_camera_x * 2)
            {
                change_direction = true;
                can_shot = true;
                shot_position = Random.Range(Camera.main.transform.position.x - offset_camera_x * 2, Camera.main.transform.position.x + offset_camera_x * 2);
                Debug.Log("POSITION RANGE: " + shot_position);

            }
            else if (!change_direction)
                MoveDown();

        }
        else if (gameObject.transform.position.y <= Camera.main.transform.position.x - offset_camera_x * 2)
        {
            MoveUp();
        }
        else if (can_shot)
        {
            can_shot = false;
            shot_position = Random.Range(Camera.main.transform.position.x - offset_camera_x * 2, Camera.main.transform.position.x + offset_camera_x * 2);
            Debug.Log("POSITION RANGE: " + shot_position);
        }

        //Main Direction
        if(change_direction && !stop)
        {
            MoveUp();
        }

        // Kamehameha
        if(gameObject.transform.position.y >= shot_position && can_shot)
        {
            Kamehameha();
            can_shot = false;
            stop = true;
            stop_timer += Time.deltaTime;

            
        }

        if(stop)  // Enemy_stopped
        {
            stop_timer += Time.deltaTime;
            if (stop_timer >= time_stopped) //  2s stopped
            {
                stop = false;
                stop_timer = 0;
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
            Instantiate(Kamehameha_shot, gameObject.transform.position, Quaternion.identity);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            Destroy(gameObject);
            dead = true;
        }
    }

    
}
