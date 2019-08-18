using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public GameObject[] pipe;
    public GameObject[] enemy1;
    //public GameObject[] enemy2;
    public float time;

    public int random_pipe_number;
    public int random_enemy1_number;
    public int random_enemy2_number;
    public int random_enemy3_number;


    // Start is called before the first frame update
    void Start()
    {
        //  Random vars
        random_enemy1_number = Random.Range(1, 101);

        //  Start Generating
        Generator();
    }

    // Update is called once per frame
    void Update()
    {
        //Difficulty curve
        time -= Mathf.Log(time + 0.6f) / 3000;

        //Max time per obstacle (35 score)
        if (time < 1f)
            time = 1f;

        //if (time < 1.65f)
        //    time = 1.65f;
    }

    void Generator()
    {
        if (!Player_Jump.dead)
        {
            if (random_enemy1_number <= 50)
            {
                Enemy2Generator();
                random_enemy1_number = Random.Range(1, 101);
            }
            else
            {
                Enemy3Generator();
                random_enemy1_number = Random.Range(1, 101);
            }




        }
         
    }

    void Enemy2Generator()
    {
        Instantiate(pipe[0], new Vector3(transform.position.x, Random.Range(-2.2f, 2.6f), 0), Quaternion.identity);
        Invoke("Generator", time);
    }

    void Enemy3Generator()
    {
        Instantiate(enemy1[0], new Vector3(transform.position.x, Random.Range(-2.2f, 2.6f), 0), Quaternion.identity);
        Invoke("Generator", time);
    }
    void PipeGenerator()
    {

    }
    
}
