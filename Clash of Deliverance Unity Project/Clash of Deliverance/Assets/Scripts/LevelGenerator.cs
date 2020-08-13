using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public GameObject pipe;
    public GameObject enemy3;
    public GameObject enemy2;
    public GameObject Boss;
    public GameObject enemy4;
    public GameObject enemy1;
    public GameObject easyPipe;
    public GameObject normalPipe;
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
    private bool both_enemy_appear;
    private int  multiple_enemy_appear_counter;
    private bool final_boss_enemy_appear;
    private bool boss_enemy_appear;
    private bool miniboss_enemy_appear;
    private bool miniboss2_enemy_appear;
    private bool both_miniboss_appear = true; 

    public bool final_boss_enemy_defeat;
    public bool boss_enemy_defeat;
    public bool miniboss_enemy_defeat;
    public bool miniboss2_enemy_defeat;
    public bool enter_on_75_phase;

    float timer_pre_boss = 0;

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

    void Update()
    {
        //Difficulty curve
        time -= Mathf.Log(time + 0.6f) / 3000;

        //Max time per obstacle (35 score)

        if (time < 1.65f)
            time = 1.65f;


        if (enemy_appear)
        {
            //pipe_offset += 2;
            enemy_appear = false;
            
        }

        
        if(final_boss_enemy_appear)
        {
            PrepareFinalBossTimer();
            CancelInvoke("Generator");
        }
        if(boss_enemy_appear)
        {
            PrepareBossTimer();
            CancelInvoke("Generator");
        }
        if (miniboss_enemy_appear)
        {
            PrepareMiniBossTimer();
            CancelInvoke("Generator");
        }
        if(miniboss2_enemy_appear)
        {
            PrepareMiniBoss2Timer();
            CancelInvoke("Generator");
        }
        if(enter_on_75_phase && both_miniboss_appear)
        {
            PrepareBothMiniBossTimer();
            CancelInvoke("Generator");
        }
        else if (Score.score_value >= 75) //  DIFFICULTY: High   
        {
            enter_on_75_phase = true;
        }
    }

    void PrepareFinalBossTimer()
    {
        if (timer_pre_boss >= 4.25f)
        {
          
            timer_pre_boss = 0;
            BossEnemyGenerator();
            final_boss_enemy_appear = false;
        }
        else
            timer_pre_boss += Time.deltaTime;
    }

    void PrepareBossTimer()
    {
        if (timer_pre_boss >= 4.25f)
        {
          
            timer_pre_boss = 0;
            BossEnemyGenerator();
            boss_enemy_appear = false;
        }
        else
            timer_pre_boss += Time.deltaTime;
    }

    void PrepareMiniBossTimer()
    {
        if (timer_pre_boss >= 3f)
        {
          
            timer_pre_boss = 0;
            Debug.Log("MiniBoss1");
            Enemy1Generator();
            miniboss_enemy_appear = false;
        }
        else
            timer_pre_boss += Time.deltaTime;
    }
    void PrepareMiniBoss2Timer()
    {
        if (timer_pre_boss >= 3f)
        {

            timer_pre_boss = 0;
            Debug.Log("MiniBoss2");
            Enemy4Generator();
            miniboss2_enemy_appear = false;
        }
        else
            timer_pre_boss += Time.deltaTime;
    }
    void PrepareBothMiniBossTimer()
    {
        if(timer_pre_boss >= 3f)
        {

            timer_pre_boss = 0;
            Debug.Log("BothMiniBoss");
            both_miniboss_appear = false;
            Enemy4and5Generator();
        }
        else
            timer_pre_boss += Time.deltaTime;
    }
    public void Generator()
    {
        
        if (!Player_Jump.dead)
        {
            if (!final_boss_enemy_defeat && Score.score_value >= 100) //  BOSS
            {
                if (!final_boss_enemy_appear)
                {
                    random_boss_enemy_number = Random.Range(1, 101);
                    final_boss_enemy_appear = true;
                }
            }
            else if (!boss_enemy_defeat &&  Score.score_value >= 49) //  BOSS
            {
                if (!boss_enemy_appear)
                {
                    random_boss_enemy_number = Random.Range(1, 101);
                    boss_enemy_appear = true;
                }
            }
            else if (!miniboss_enemy_defeat &&  Score.score_value >= 24) // MINI BOSS
            {
                if (!miniboss_enemy_appear)
                {
                    miniboss_enemy_appear = true;
                }
            }
            else if (!miniboss2_enemy_defeat &&  Score.score_value >= 14) // MINI BOSS
            {
                if (!miniboss2_enemy_appear)
                {
                    miniboss2_enemy_appear = true;
                }
            }

            else if (multiple_enemy_appear)
            {
                MultipleEnemy2Generator(num: 3);
            }
            else if(both_enemy_appear)
            {
                Enemy2AndEnemy3Generator();
            }
            
            else
            {
                if (random_pipe_number <= pipe_limit_generator) // Instantiate pipe     90% - 5% every time a pipe is repeated
                {
                    if (Score.score_value <= 7)
                        EasyPipeGenerator();
                    else if (Score.score_value <= 50)
                        NormalPipeGenerator();
                    else
                        PipeGenerator();

                    // repeat % pipe
                    if(!enter_on_75_phase)
                    {
                        if (Score.score_value >= 50)
                        {
                            pipe_limit_generator -= 15;
                        }
                        else if (Score.score_value >= 15)
                        {
                            pipe_limit_generator -= 15;
                        }
                        else
                            pipe_limit_generator -= 25;
                    }
                   

                }
                else // Instantiate Enemies
                {
                    pipe_limit_generator = pipe_limit_generator_var;

                    if (Score.score_value > 50) //  DIFFICULTY: Medium-hard
                    {
                        if (random_both_enemy_number <= 35)
                        {
                            Enemy2AndEnemy3Generator();
                            random_both_enemy_number = Random.Range(1, 101);
                            enemy_appear = true;
                        }
                        else if (random_multiple_enemy2_number <= 25)
                        {
                            MultipleEnemy2Generator(num: 3);
                            random_multiple_enemy2_number = Random.Range(1, 101);

                        }
                        else if (random_multiple_enemy3_number <= 30)
                        {
                            MultipleEnemy3Generator(num: 3);
                            random_multiple_enemy3_number = Random.Range(1, 101);
                            multiple_enemy_appear = true;

                        }
                        else
                        {
                            if (random_enemy3_number <= 50)
                            {
                                Enemy3Generator();
                                random_enemy3_number = Random.Range(1, 101);
                                enemy_appear = true;
                            }                                          
                            else
                            {
                                //PipeGenerator();
                                Enemy2Generator();
                                random_enemy2_number = Random.Range(1, 101);
                                enemy_appear = true;
                            }
                        }
                    }
                    else if (Score.score_value > 20) //  DIFFICULTY: Medium
                    {
                        if (random_both_enemy_number <= 33)
                        {
                            Enemy2AndEnemy3Generator();
                            random_both_enemy_number = Random.Range(1, 101);
                            Debug.Log("random_both_enemy_number");
                        }
                        else if (random_multiple_enemy2_number <= 33)
                        {
                            MultipleEnemy2Generator(num: 3);
                            random_multiple_enemy2_number = Random.Range(1, 101);
                            Debug.Log("random_multiple_enemy2_number");
                        }
                        else
                        {
                            if (random_enemy3_number <= 50)
                            {
                                Enemy3Generator();
                                random_enemy3_number = Random.Range(1, 101);
                                enemy_appear = true;
                                Debug.Log("random_enemy3_number");
                            }
                            else
                            {
                                Enemy2Generator();
                                random_enemy2_number = Random.Range(1, 101);
                                enemy_appear = true;
                                Debug.Log("random_enemy2_number");
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

        //Last level full pipes
        if (enter_on_75_phase && Score.score_value >= 90) random_pipe_number = 100;
        else if (enter_on_75_phase && Score.score_value >= 75) random_pipe_number = 0;

    }

    void PipeGenerator()
    {
        Instantiate(pipe, new Vector3(transform.position.x + pipe_offset, Random.Range(-2.2f, 2.6f), 0), Quaternion.identity);
        Invoke("Generator", time);
    }

    void EasyPipeGenerator()
    {
        Instantiate(easyPipe, new Vector3(transform.position.x + pipe_offset, Random.Range(-2.2f, 2.6f), 0), Quaternion.identity);
        Invoke("Generator", time);

    }
    void NormalPipeGenerator()
    {
        Instantiate(normalPipe, new Vector3(transform.position.x + pipe_offset, Random.Range(-2.2f, 2.6f), 0), Quaternion.identity);
        Invoke("Generator", time);
    }

    void Enemy2Generator()
    {
        Instantiate(enemy2, new Vector3(transform.position.x + pipe_offset - 3, Random.Range(-4f, -2f), 0), Quaternion.identity);
        Invoke("Generator", time);
    }

    void Enemy3Generator()
    {
        Instantiate(enemy3, new Vector3(transform.position.x + pipe_offset - 3, Random.Range(2f, 4f), 0), Quaternion.identity);
        Invoke("Generator", time);
    }


    void Enemy2AndEnemy3Generator()
    {
        //Instantiate(enemy2, new Vector3(transform.position.x + pipe_offset - 1, Random.Range(-4f, -2f), 0), Quaternion.identity);
        //Instantiate(enemy3, new Vector3(transform.position.x + pipe_offset - 2, Random.Range(3f, 4f), 0), Quaternion.identity);

        if (multiple_enemy_appear_counter < 2)
        {
            both_enemy_appear = true;
            multiple_enemy_appear_counter++;

            if (multiple_enemy_appear_counter == 1) Instantiate(enemy2, new Vector3(transform.position.x + pipe_offset - 3, Random.Range(-4f, -2f), 0), Quaternion.identity);
            else Instantiate(enemy3, new Vector3(transform.position.x + pipe_offset - 2, Random.Range(3f, 4f), 0), Quaternion.identity);

            Invoke("Generator", time - 0.8f);
        }
        else
        {
            both_enemy_appear = false;
            multiple_enemy_appear_counter = 0;
            Invoke("Generator", time);
        }
    }

    void MultipleEnemy2Generator(int num)
    {
        //Instantiate(enemy2, new Vector3(transform.position.x + pipe_offset + 2, Random.Range(-3.5f, 0f), 0), Quaternion.identity);
        //Instantiate(enemy2, new Vector3(transform.position.x + pipe_offset, Random.Range(-3.5f, 0f), 0), Quaternion.identity);
        //Instantiate(enemy2, new Vector3(transform.position.x + pipe_offset - 2, Random.Range(-3.5f, 0f), 0), Quaternion.identity);

        if (multiple_enemy_appear_counter < num)
        {
            multiple_enemy_appear = true;
            multiple_enemy_appear_counter++;
            Instantiate(enemy2, new Vector3(transform.position.x + pipe_offset - 3, Random.Range(-4f, -2f), 0), Quaternion.identity);
            Invoke("Generator", time - 0.8f);
        }
        else
        {
            multiple_enemy_appear = false;
            multiple_enemy_appear_counter = 0;
            Invoke("Generator", time);
        }
    }

    void MultipleEnemy1Generator() // TODO: Change enemy 2 to 1
    {
        //Instantiate(enemy2, new Vector3(transform.position.x + pipe_offset + 2, Random.Range(-3.5f, 0f), 0), Quaternion.identity);
        //Instantiate(enemy2, new Vector3(transform.position.x + pipe_offset, Random.Range(-3.5f, 0f), 0), Quaternion.identity);
        //Instantiate(enemy2, new Vector3(transform.position.x + pipe_offset - 2, Random.Range(-3.5f, 0f), 0), Quaternion.identity);
        Invoke("Generator", time);
    }

    void Enemy2AndEnemy3AndEnemy1Generator()
    {
        Instantiate(enemy2, new Vector3(transform.position.x + pipe_offset, Random.Range(-4f, -2f), 0), Quaternion.identity);
        Instantiate(enemy3, new Vector3(transform.position.x + pipe_offset - 2, Random.Range(3f, 4f), 0), Quaternion.identity);
        //Instantiate(enemy3[0], new Vector3(transform.position.x + pipe_offset - 2, Random.Range(3f, 4f), 0), Quaternion.identity); //  ENMY 1  
        Invoke("Generator", time);
    }

    void MultipleEnemy3Generator(int num)
    {
        //Instantiate(enemy3, new Vector3(transform.position.x + pipe_offset + 2, Random.Range(2f, 4f), 0), Quaternion.identity);
        //Instantiate(enemy3, new Vector3(transform.position.x + pipe_offset, Random.Range(2f, 4f), 0), Quaternion.identity);
        //Instantiate(enemy3, new Vector3(transform.position.x + pipe_offset - 2, Random.Range(2f, 4f), 0), Quaternion.identity);
        //Invoke("Generator", time);

        if (multiple_enemy_appear_counter < num)
        {
            multiple_enemy_appear = true;
            multiple_enemy_appear_counter++;
            Instantiate(enemy3, new Vector3(transform.position.x + pipe_offset - 3, Random.Range(2f, 4f), 0), Quaternion.identity);
            Invoke("Generator", time - 0.8f);
        }
        else
        {
            multiple_enemy_appear = false;
            multiple_enemy_appear_counter = 0;
            Invoke("Generator", time);
        }
    }

    void BossEnemyGenerator()
    {
        //  Boss Time :D
        Instantiate(Boss/*, new Vector3(transform.position.x + pipe_offset - 2, Random.Range(2f, 4f), 0), Quaternion.identity*/);
        Invoke("Generator", time);
    }
    void Enemy4and5Generator()
    {
        Instantiate(enemy4);
        Instantiate(enemy1);
        Invoke("Generator", time);
    }
    void Enemy4Generator()
    {
        //  Boss Time :D
       // Instantiate(MiniBoss1, new Vector3(transform.position.x + pipe_offset - 2, Random.Range(-4f, 0f), 0), Quaternion.identity);

        Instantiate(enemy4/*, new Vector3(transform.position.x + pipe_offset - 2, Random.Range(2f, 4f), 0), Quaternion.identity*/);
        Invoke("Generator", time);
    }

    void Enemy1Generator()
    {
        //  Boss Time :D
        Instantiate(enemy1/*, new Vector3(transform.position.x + pipe_offset - 2, Random.Range(2f, 4f), 0), Quaternion.identity*/);
        Invoke("Generator", time);
    }
    
    public void InvokingGenerator(float time)
    {
        Invoke("Generator", time);
    }

}
