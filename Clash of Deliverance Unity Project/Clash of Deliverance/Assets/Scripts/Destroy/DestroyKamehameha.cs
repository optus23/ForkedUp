﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyKamehameha : MonoBehaviour
{

    private float timer_close_shot;

    Quaternion shotOpen;
    Quaternion shotClose;
    Quaternion shotNormal;

    private bool opening;
    private bool closing;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 2.5f);
        shotOpen = Quaternion.Euler(0, 90, 90);
        shotClose = Quaternion.Euler(90, 0, 90);
        shotNormal = Quaternion.Euler(0, 0, 90);


        transform.rotation = shotOpen;

    }

    // Update is called once per frame
    void Update()
    {
        timer_close_shot += Time.deltaTime;

        if(!opening)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, shotNormal, 4 * Time.deltaTime);
            if (transform.rotation == shotNormal)
                opening = true;
        }
        

        if (timer_close_shot > 1.75f)
        {
            timer_close_shot = 0;
            closing = true;
        }

        if (closing)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, shotClose, 8 * Time.deltaTime);
            if (transform.rotation == shotClose)
                closing = false;
        }
    }
}
