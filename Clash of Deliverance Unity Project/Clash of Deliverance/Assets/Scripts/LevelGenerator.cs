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
    public int random_multiple_enemy3_number;
    public int random_multiple_enemy1_number;
    public int random_boss_enemy_number;

    private int pipe_limit_generator;
    public int pipe_limit_generator_var;
    public int pipe_offset;

    private bool enemy_appear;
    private bool multiple_enemy_appear;
    private bool boss_enemy_appear;
    public bool boss_enemy_defeat;

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
        random_multiple_enemy3_number = Random.Range(1, 101);
        random_multiple_enemy1_number = Random.Range(1, 101);
        random_boss_enemy_number = Random.Range(1, 101);

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


        if (enemy_appear)
        {
            pipe_offset += 3;
            enemy_appear = false;
            
        }
        if(multiple_enemy_appear)
        {
            pipe_offset += 9;
            multiple_enemy_appear = false;
        }
    }

    void Generator()
    {
        if (!Player_Jump.dead)
        {
            if (Score.score_value >= 100) // FINAL BOSS
            {
                if(!boss_enemy_defeat)
                {
                    BossEnemyGenerator();
                    random_boss_enemy_number = Random.Range(1, 101);
                    boss_enemy_appear = true;
                }
                else
                {
                    // TODO: Victory!!!!!!! Go to credits
                }
                
            }
            else if(Score.score_value >= 75) //  DIFFICULTY: High   (no pipes)
            {
                if (random_both_enemy_number <= 20)
                {
                    Enemy2AndEnemy3AndEnemy1Generator();
                    random_both_enemy_number = Random.Range(1, 101);
                    enemy_appear = true;
                }
                else if (random_multiple_enemy1_number <= 25)
                {
                    MultipleEnemy1Generator();
                    random_multiple_enemy1_number = Random.Range(1, 101);
                    multiple_enemy_appear = true;

                }
                else if (random_multiple_enemy2_number <= 25)
                {
                    MultipleEnemy2Generator();
                    random_multiple_enemy2_number = Random.Range(1, 101);
                    multiple_enemy_appear = true;

                }
                if (random_multiple_enemy3_number <= 50) 
                {
                    MultipleEnemy3Generator();
                    random_multiple_enemy3_number = Random.Range(1, 101);
                    multiple_enemy_appear = true;

                }
                else if (random_boss_enemy_number <= 5)
                {
                    BossEnemyGenerator();
                    random_boss_enemy_number = Random.Range(1, 101);
                    boss_enemy_appear = true;

                }
                else
                {
                    if (random_enemy3_number <= 33)
                    {
                        Enemy3Generator();
                        random_enemy3_number = Random.Range(1, 101);
                        enemy_appear = true;
                    }
                    else if (random_enemy2_number <= 33)
                    {
                        Enemy2Generator();
                        random_enemy2_number = Random.Range(1, 101);
                        enemy_appear = true;
                    }
                    else
                    {
                        Enemy1Generator();
                        random_enemy1_number = Random.Range(1, 101);
                        enemy_appear = true;
                    }                  
                }
            }
            else
            {
                if (random_pipe_number <= pipe_limit_generator /*pipe_limit_generator*/) // Instantiate pipe     90% - 5% every time a pipe is repeated
                {
                    PipeGenerator();
                    if(Score.score_value >= 50)
                    {
                        pipe_limit_generator -= 15;
                    }
                    else if (Score.score_value >= 20)
                    {
                        pipe_limit_generator -= 10;
                    }
                    else
                        pipe_limit_generator -= 5;

                }
                else // Instantiate Enemies
                {
                    pipe_limit_generator = pipe_limit_generator_var;

                    if (Score.score_value > 20) //  DIFFICULTY: Medium
                    {
                        if (random_both_enemy_number <= 20)
                        {
                            Enemy2AndEnemy3Generator();
                            random_both_enemy_number = Random.Range(1, 101);
                            enemy_appear = true;
                        }
                        else if (random_multiple_enemy2_number <= 25)
                        {
                            MultipleEnemy2Generator();
                            random_multiple_enemy2_number = Random.Range(1, 101);
                            multiple_enemy_appear = true;

                        }
                        else if (random_boss_enemy_number <= 5)
                        {
                            BossEnemyGenerator();
                            random_boss_enemy_number = Random.Range(1, 101);
                            boss_enemy_appear = true;

                        }
                        else
                        {
                            if (random_enemy3_number <= 33)
                            {
                                Enemy3Generator();
                                random_enemy3_number = Random.Range(1, 101);
                                enemy_appear = true;
                            }
                            else if (random_enemy2_number <= 33)
                            {
                                Enemy2Generator();
                                random_enemy2_number = Random.Range(1, 101);
                                enemy_appear = true;
                            }
                            else if (random_enemy1_number <= 33)
                            {
                                Enemy1Generator();
                                random_enemy1_number = Random.Range(1, 101);
                                enemy_appear = true;
                            }
                            else
                            {
                                PipeGenerator();
                            }
                        }

                    }

                    if (Score.score_value > 50) //  DIFFICULTY: Medium
                    {
                        
                        if (random_both_enemy_number <= 30)
                        {
                            Enemy2AndEnemy3AndEnemy1Generator();
                            random_both_enemy_number = Random.Range(1, 101);
                            enemy_appear = true;
                        }
                        else if (random_multiple_enemy2_number <= 20)
                        {
                            MultipleEnemy2Generator();
                            random_multiple_enemy2_number = Random.Range(1, 101);
                            multiple_enemy_appear = true;

                        }
                        else if (random_multiple_enemy3_number <= 35)
                        {
                            MultipleEnemy3Generator();
                            random_multiple_enemy3_number = Random.Range(1, 101);
                            multiple_enemy_appear = true;

                        }
                        else
                        {
                            if (random_enemy3_number <= 33)
                            {
                                Enemy3Generator();
                                random_enemy3_number = Random.Range(1, 101);
                                enemy_appear = true;
                            }
                            else if (random_enemy2_number <= 33)
                            {
                                Enemy2Generator();
                                random_enemy2_number = Random.Range(1, 101);
                                enemy_appear = true;
                            }
                            else if (random_enemy1_number <= 33)
                            {
                                Enemy1Generator();
                                random_enemy1_number = Random.Range(1, 101);
                                enemy_appear = true;
                            }
                            else
                            {
                                PipeGenerator();
                            }
                        }
                    }
                    else //  DIFFICULTY: Low
                    {
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
        Instantiate(enemy2[0], new Vector3(transform.position.x + pipe_offset - 2, Random.Range(-4f, 0f), 0), Quaternion.identity);
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
        Instantiate(enemy2[0], new Vector3(transform.position.x + pipe_offset, Random.Range(-4f, -2f), 0), Quaternion.identity);
        Instantiate(enemy3[0], new Vector3(transform.position.x + pipe_offset - 2, Random.Range(3f, 4f), 0), Quaternion.identity);
        Invoke("Generator", time);
    }

    void MultipleEnemy2Generator()
    {
        Instantiate(enemy2[0], new Vector3(transform.position.x + pipe_offset + 2, Random.Range(-3.5f, 0f), 0), Quaternion.identity);
        Instantiate(enemy2[0], new Vector3(transform.position.x + pipe_offset, Random.Range(-3.5f, 0f), 0), Quaternion.identity);
        Instantiate(enemy2[0], new Vector3(transform.position.x + pipe_offset - 2, Random.Range(-3.5f, 0f), 0), Quaternion.identity);
        Invoke("Generator", time);
    }

    void MultipleEnemy1Generator() // TODO: Change enemy 2 to 1
    {
        Instantiate(enemy2[0], new Vector3(transform.position.x + pipe_offset + 2, Random.Range(-3.5f, 0f), 0), Quaternion.identity);
        Instantiate(enemy2[0], new Vector3(transform.position.x + pipe_offset, Random.Range(-3.5f, 0f), 0), Quaternion.identity);
        Instantiate(enemy2[0], new Vector3(transform.position.x + pipe_offset - 2, Random.Range(-3.5f, 0f), 0), Quaternion.identity);
        Invoke("Generator", time);
    }

    void Enemy2AndEnemy3AndEnemy1Generator()
    {
        Instantiate(enemy2[0], new Vector3(transform.position.x + pipe_offset, Random.Range(-4f, -2f), 0), Quaternion.identity);
        Instantiate(enemy3[0], new Vector3(transform.position.x + pipe_offset - 2, Random.Range(3f, 4f), 0), Quaternion.identity);
        //Instantiate(enemy3[0], new Vector3(transform.position.x + pipe_offset - 2, Random.Range(3f, 4f), 0), Quaternion.identity); //  ENMY 1  
        Invoke("Generator", time);
    }

    void MultipleEnemy3Generator()
    {
        Instantiate(enemy3[0], new Vector3(transform.position.x + pipe_offset + 2, Random.Range(2f, 4f), 0), Quaternion.identity);
        Instantiate(enemy3[0], new Vector3(transform.position.x + pipe_offset, Random.Range(2f, 4f), 0), Quaternion.identity);
        Instantiate(enemy3[0], new Vector3(transform.position.x + pipe_offset - 2, Random.Range(2f, 4f), 0), Quaternion.identity);
        Invoke("Generator", time);
    }

    void BossEnemyGenerator()
    {
        //  Boss Time :D
        Invoke("Generator", time);
    }

}
