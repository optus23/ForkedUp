﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicObstacle : MonoBehaviour
{
    public float velocity;

    private float timer;
    private float life_time = 8;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!Player_Jump.dead)
        {
            gameObject.transform.position = new Vector2(gameObject.transform.position.x - velocity, gameObject.transform.position.y);

            timer += Time.deltaTime;
            if (timer > life_time)
                Destroy(gameObject);
        }

       
    }

}
