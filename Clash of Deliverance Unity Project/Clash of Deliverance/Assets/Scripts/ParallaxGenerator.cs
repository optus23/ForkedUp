﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ParallaxGenerator : MonoBehaviour
{

    public GameObject Background1;
    public GameObject Background2;
    public GameObject Background3;

    public float time1;
    public float time2;
    public float time3;

    Scene scene;


    // Start is called before the first frame update
    void Start()
    {
        scene = SceneManager.GetActiveScene();

        SpawnBackground1();
        SpawnBackground2();
        SpawnBackground3();

    }

    // Update is called once per frame
    void Update()
    {
      
    }

    void SpawnBackground1()
    {
        if (!Player_Jump.dead || scene.name == "WaitingPlayer")
        {
            Invoke("SpawnBackground1", time1);
            Instantiate(Background1, new Vector2(Background1.transform.position.x + 7, Background1.transform.position.y), Quaternion.identity);

        }

    }
    void SpawnBackground2()
    {
        if (!Player_Jump.dead || scene.name == "WaitingPlayer")
        {
            Invoke("SpawnBackground2", time2);
            Instantiate(Background2, new Vector2(Background2.transform.position.x + 9, Background2.transform.position.y), Quaternion.identity);

        }

    }
    void SpawnBackground3()
    {
        if (!Player_Jump.dead || scene.name == "WaitingPlayer")
        {
            Invoke("SpawnBackground3", time3);
            Instantiate(Background3, new Vector2(Background3.transform.position.x + 8, Background3.transform.position.y), Quaternion.identity);

        }

    }


    
}
