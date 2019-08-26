using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_3 : MonoBehaviour
{
    Player_Jump player;
    public float velocity;
    public float offset_camera_x;
    private bool change_direction;
    private int bounce = 1;
    public int bounce_number;

    public GameObject Enemy_Shot;
    public GameObject Mouth;

    public bool destroy_shot;

    public bool dead;

    void Start()
    {
        bounce_number = Random.Range(2, 4);
        destroy_shot = false;
        Shot();
    }

    void Update()
    {
        if (gameObject.transform.position.x <= Camera.main.transform.position.x - offset_camera_x)
        {
            change_direction = true; //  Go right

        }
        else if (gameObject.transform.position.x >= Camera.main.transform.position.x && bounce < bounce_number)
        {
            change_direction = false;
            bounce++;

        }


        //Main Direction

        if (!Player_Jump.dead)
        {
            if (change_direction)
            {
                MoveRight();
            }
            else
                MoveLeft();
        }          
    }


    void MoveRight()
    {
        gameObject.transform.position = new Vector2(gameObject.transform.position.x + velocity * Time.deltaTime, transform.position.y);
    }

    void MoveLeft()
    {
        gameObject.transform.position = new Vector2(gameObject.transform.position.x - velocity * Time.deltaTime, transform.position.y);

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
        Instantiate(Enemy_Shot, gameObject.transform.position, Quaternion.identity);
        Invoke("Shot", 0.5f);
    }
}
