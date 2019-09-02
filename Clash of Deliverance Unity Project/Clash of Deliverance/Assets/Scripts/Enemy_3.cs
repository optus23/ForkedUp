﻿using System.Collections;
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
    public bool start_fading;
    Rigidbody2D rb;
    BoxCollider2D collider;
    public GameObject Right_eye;
    public GameObject Left_eye;
    public GameObject Enemy_mouth;

    public GameObject Money;


    void Start()
    {
        bounce_number = Random.Range(2, 4);
        destroy_shot = false;
        Shot();

        rb = GetComponent<Rigidbody2D>();
        rb.isKinematic = true;

        collider = GetComponent<BoxCollider2D>();

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
            Destroy(gameObject, 1f);

            rb.AddForce(transform.right * 10000 * Time.deltaTime);
            start_fading = true;
            GetComponent<EchoEffect>().enabled = false;
            rb.isKinematic = false;
            dead = true;
            Enemy_Shot.SetActive(false);
            Right_eye.SetActive(false);
            Left_eye.SetActive(false);
            Enemy_mouth.SetActive(false);
            collider.enabled = false;

            Destroy(Money);
        }
    }

    void Shot()
    {
        Instantiate(Enemy_Shot, gameObject.transform.position, Quaternion.identity);
        Invoke("Shot", 0.5f);
    }
}
