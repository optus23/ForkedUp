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
    public bool start_fading;
    public bool dead;
    private bool touch_first_wall;

    Rigidbody2D rb;
    public GameObject Right_korn;
    public GameObject Left_korn;
    public GameObject Right_Eye;
    public GameObject Left_Eye;
    public GameObject Mouth;
    BoxCollider2D collider;

    public GameObject Money;


    // Start is called before the first frame update
    void Start()
    {
        bounce_number = Random.Range(1, 3);
        rb = GetComponent<Rigidbody2D>();
        rb.isKinematic = true;

        collider = GetComponent<BoxCollider2D>();
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

        if (!Player_Jump.dead)
        {
            if (change_direction && !static_direction)
            {
                MoveRight();
            }
            else if (static_direction)
            {
                MoveUp();
            }
            else
                MoveLeft();
        }
        

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
            Destroy(gameObject, 1f);

            rb.isKinematic = false;
            rb.AddForce(transform.right * 10000 * Time.deltaTime);
            rb.AddForce(transform.up * 500 * Time.deltaTime);
            start_fading = true;
            GetComponent<EchoEffect>().enabled = false;
            dead = true;
            Left_Eye.SetActive(false);
            Right_Eye.SetActive(false);
            Right_korn.SetActive(false);
            Left_korn.SetActive(false);
            Mouth.SetActive(false);

            collider.enabled = false;
            FindObjectOfType<AudioManager>().Play("Death_Enemy2");

            Destroy(Money);
            
        }
    }
}
