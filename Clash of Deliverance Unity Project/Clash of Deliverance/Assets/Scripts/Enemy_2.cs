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

    public static bool dead;


    // Start is called before the first frame update
    void Start()
    {
        bounce_number = Random.Range(1, 4);
        Debug.Log(bounce_number);

    }

    // Update is called once per frame
    void Update()
    {
        switch (bounce_number)
        {
            case 1:
                if (gameObject.transform.position.x <= Camera.main.transform.position.x - offset_camera_x && !MoveRight() && !MoveUp())
                {
                    ChangeDirection();
                }
                else if (gameObject.transform.position.x >= Camera.main.transform.position.x - offset_camera_x/3 && !MoveLeft())
                {
                    static_direction = true;
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
                else if (gameObject.transform.position.x >= Camera.main.transform.position.x - offset_camera_x / 3 && bounce >= bounce_number &&!MoveLeft())
                {
                    static_direction = true;
                    Debug.Log("Up");
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
                else if (gameObject.transform.position.x >= Camera.main.transform.position.x - offset_camera_x / 3 && bounce >= bounce_number && !MoveLeft())
                {
                    static_direction = true;
                    Debug.Log("Up");
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

    bool MoveUp()
    {
        if(change_direction && static_direction)
        {
            gameObject.transform.position = new Vector2(transform.position.x, transform.position.y + velocity * Time.deltaTime);
            return true;

        }
        else
            return false;
    }

    bool MoveRight()
    {
        if (change_direction && !static_direction)
        {
            gameObject.transform.position = new Vector2(transform.position.x + velocity * Time.deltaTime, transform.position.y);
            return true;
        }
        return false;
    }

    bool MoveLeft()
    {
        if (change_direction && !static_direction)
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
            change_direction = true;
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
