using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_2 : MonoBehaviour
{
    public float velocity;
    public float offset_camera_x;
    private bool change_direction;
    private bool static_direction;
    private int bounce = 1;
    public int bounce_number;

    public bool dead;
    private bool touch_first_wall;

    // Start is called before the first frame update
    void Start()
    {
        bounce_number = Random.Range(1, 3);

    }

    // Update is called once per frame
    void Update()
    {
        switch (bounce_number)
        {
            case 1:
                if (gameObject.transform.position.x <= Camera.main.transform.position.x - offset_camera_x)
                {
                    change_direction = true;
                }
                else if (gameObject.transform.position.x >= Camera.main.transform.position.x - offset_camera_x/3 && change_direction)
                {
                    static_direction = true;
                }
                break;

            case 2:
                if (gameObject.transform.position.x <= Camera.main.transform.position.x - offset_camera_x)
                {
                    change_direction = true;
                    touch_first_wall = true;

                }
                else if (gameObject.transform.position.x >= Camera.main.transform.position.x && bounce < bounce_number && touch_first_wall) 
                {
                    change_direction = false;
                    bounce++;

                }
                else if (gameObject.transform.position.x >= Camera.main.transform.position.x - offset_camera_x / 3 && bounce >= bounce_number && change_direction)
                {
                    static_direction = true;
                }
                break;

           
            default:
                break;
        }

        //  Main Direction
       
        if (change_direction && !static_direction)
        {
            MoveRight();
        }
        else if(static_direction)
        {
            MoveUp();
        }
        else
            MoveLeft();

    }

    void MoveUp()
    {
        gameObject.transform.position = new Vector2(transform.position.x, transform.position.y + velocity * Time.deltaTime);
    }

    void MoveRight()
    {
        gameObject.transform.position = new Vector2(transform.position.x + velocity * Time.deltaTime, transform.position.y);       
    }

    void MoveLeft()
    {
        gameObject.transform.position = new Vector2(transform.position.x - velocity * Time.deltaTime, transform.position.y);
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
