﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(DestroyMoney))]
public class RandomMoney : MonoBehaviour
{
    public GameObject Money_1;
    public GameObject Money_5;
    public GameObject Money_10;

    public int spawn_ratio_1;
    public int spawn_ratio_5;
    public int spawn_ratio_10;

    private void Start()
    {
        spawn_ratio_1 = Random.Range(1, 101);
        spawn_ratio_5 = Random.Range(1, 101);
        spawn_ratio_10 = Random.Range(1, 101);
        
    }

    private void Update()
    {
     

        if ((spawn_ratio_10 <= 5 && spawn_ratio_5 <= 10) || (spawn_ratio_10 <= 5 && spawn_ratio_1 <= 33) || (spawn_ratio_5 <= 10 && spawn_ratio_1 <= 33))
        {
            spawn_ratio_1 = Random.Range(1, 101);
            spawn_ratio_5 = Random.Range(1, 101);
            spawn_ratio_10 = Random.Range(1, 101);
            
        }
        else if (Money_1 != null && Money_5 != null && Money_10 != null)
        {
            
            if (spawn_ratio_1 <= 33) //  33%
            {
                Money_1.SetActive(true);
            }         
            if (spawn_ratio_5 <= 10) //  10%
            {
                Money_5.SetActive(true);
            }
            if (spawn_ratio_10 <= 3) //  3%
            {
                Money_10.SetActive(true);
            }
        }

    }
}
