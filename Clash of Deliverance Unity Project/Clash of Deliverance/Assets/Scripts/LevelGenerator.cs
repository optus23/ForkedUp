using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public GameObject[] pipe;
    public GameObject[] enemy3;
    public GameObject[] enemy2;
    public float time;

    public int random_pipe_number;
    public int random_enemy1_number;
    public int random_enemy2_number;
    public int random_enemy3_number;
    public int random_both_enemy_number;
    public int random_multiple_enemy2_number;

    private int pipe_limit_generator;
    public int pipe_limit_generator_var;
    public int pipe_offset;

    private bool enemy_appear;

    // Start is called before the first frame update
    void Start()
    {
        pipe_limit_generator = pipe_limit_generator_var;

        //  Random vars
        random_pipe_number = Random.Range(1, 101);
        random_enemy2_number = Random.Range(1, 101);
        random_enemy3_number = Random.Range(1, 101);
        random_both_enemy_number = Random.Range(1, 101);
        random_multiple_enemy2_number = Random.Range(1, 101);

        //  Start Generating
        Generator();

}

    // Update is called once per frame
    void Update()
    {
        //Difficulty curve
        time -= Mathf.Log(time + 0.6f) / 3000;

        //Max time per obstacle (35 score)

        if (time < 1.65f)
            time = 1.65f;
    }

    void Generator()
    {
        if (!Player_Jump.dead)
        {
            if (random_pipe_number <= pipe_limit_generator) // Instantiate pipe     90% - 5% every time a pipe is repeated
            {

                if(enemy_appear)
                {
                    pipe_offset += 3;
                    enemy_appear = false;
                }

                PipeGenerator();
                pipe_limit_generator -= 5;

            }
            else // Instantiate Enemies
            {
                pipe_limit_generator = pipe_limit_generator_var;

                //if(Score.score_value > 20)
                //{
                //    if(random_both_enemy_number <= 20)
                //    {
                //        Enemy2AndEnemy3Generator();
                //        random_both_enemy_number = Random.Range(1, 101);
                //    }
                //    else if(random_multiple_enemy2_number <= 20) // TODO: Fix this
                //    {
                //        Enemy2Generator();
                //        Enemy2Generator();
                //        Enemy2Generator();
                //        random_multiple_enemy2_number = Random.Range(1, 101);
                //    }
                //    else
                //    {
                //        if (random_enemy3_number <= 33)
                //        {
                //            Enemy3Generator();
                //            random_enemy3_number = Random.Range(1, 101);
                //        }
                //        else if (random_enemy2_number <= 33)
                //        {
                //            Enemy2Generator();
                //            random_enemy2_number = Random.Range(1, 101);
                //        }
                //        else if (random_enemy1_number <= 33)
                //        {
                //            Enemy1Generator();
                //            random_enemy1_number = Random.Range(1, 101);
                //        }
                //        else
                //        {
                //            random_enemy3_number = Random.Range(1, 101);
                //            random_enemy2_number = Random.Range(1, 101);
                //            random_enemy1_number = Random.Range(1, 101);
                //        }
                //    }
                    
                //}
                //else
                //{
                   
                //}

                if (random_enemy3_number <= 50)
                {
                    Enemy3Generator();
                    enemy_appear = true;
                }
                else
                {
                    Enemy2Generator();
                    enemy_appear = true;
                }
            }
        }
        //  Random vars
        random_pipe_number = Random.Range(1, 101);
        random_enemy2_number = Random.Range(1, 101);
        random_enemy3_number = Random.Range(1, 101);
        random_both_enemy_number = Random.Range(1, 101);
        random_multiple_enemy2_number = Random.Range(1, 101);
    }

    void PipeGenerator()
    {
        Instantiate(pipe[0], new Vector3(transform.position.x + pipe_offset, Random.Range(-2.2f, 2.6f), 0), Quaternion.identity);
        Invoke("Generator", time);
    }

    void Enemy1Generator()
    {
        Instantiate(enemy2[0], new Vector3(transform.position.x + pipe_offset, Random.Range(-2.2f, 2.6f), 0), Quaternion.identity);
        Invoke("Generator", time);
    }

    void Enemy2Generator()
    {
        Instantiate(enemy2[0], new Vector3(transform.position.x + pipe_offset - 2, Random.Range(-4f, 0f), 0), Quaternion.identity);
        Invoke("Generator", time);
    }

    void Enemy3Generator()
    {
        Instantiate(enemy3[0], new Vector3(transform.position.x + pipe_offset - 2, Random.Range(2f, 4f), 0), Quaternion.identity);
        Invoke("Generator", time);
    }


    void Enemy2AndEnemy3Generator()
    {
        Instantiate(enemy2[0], new Vector3(transform.position.x, Random.Range(-4f, 0f), 0), Quaternion.identity);
        Instantiate(enemy3[0], new Vector3(transform.position.x, Random.Range(2f, 4f), 0), Quaternion.identity);
        Invoke("Generator", time);
    }


}
