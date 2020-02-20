using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_1 : MonoBehaviour
{
    private bool exit_battlefield;

    public GameObject eyes;
    private bool eyes_changed;
    private float eye_scale;
    private float eye_position;

    private bool can_prepare_particle = true;
    public GameObject ParticlePrepareKamehameha;

    public float velocity;
    public float offset_camera_x;
    private bool change_direction;
    private bool stop;
    private float stop_timer;
    public float time_stopped;
    public int bounce_number;

    public bool dead;

    private bool can_first_shot;
    private bool shot_once;
    private bool can_second_shot;
    private bool first_shot = true;
    private float first_shot_position;
    private float second_shot_position;
    public GameObject Kamehameha_shot;

    private float get_hit_timer;
    public bool get_hit;
    public GameObject Body;
    public GameObject Ears;
    public GameObject Eyes;
    public GameObject Mouth;
   // public GameObject death;
    public int life;

    public bool start_fading;

    Rigidbody2D rb;

    LevelGenerator lvlGenScript;
    GameObject levelGenerator;

    Animator OpenEyesAnimation;
    void Start()
    {
        bounce_number = Random.Range(1, 3);
        rb = GetComponent<Rigidbody2D>();
        rb.isKinematic = true;
        get_hit = false;

        levelGenerator = GameObject.FindGameObjectWithTag("LevelGenerator");
        lvlGenScript = levelGenerator.GetComponent<LevelGenerator>();

        OpenEyesAnimation = GetComponentInChildren<Animator>();
        OpenEyesAnimation.Play("Enemy1_Kamehameha", 0, 1);
    }

    void Update()
    {
        if (gameObject.transform.position.x >= Camera.main.transform.position.x + offset_camera_x && !stop)
        {
            MoveLeft();
            MoveDown();

            if(first_shot)
            {
                first_shot_position = Random.Range(Camera.main.transform.position.x - offset_camera_x*2, Camera.main.transform.position.x + offset_camera_x*1.5f);
                can_first_shot = true;
                first_shot = false;
                eyes_changed = false;
                
            }
        }
        else if (gameObject.transform.position.x < Camera.main.transform.position.x + offset_camera_x && !stop)
        {
            if (gameObject.transform.position.y <= Camera.main.transform.position.x - offset_camera_x * 2)
            {
                second_shot_position = Random.Range(Camera.main.transform.position.x - offset_camera_x, Camera.main.transform.position.x + offset_camera_x * 2);
                change_direction = true;
                can_second_shot = true;
                eyes_changed = false;

            }
            else if (!change_direction)
                MoveDown();
            else if(gameObject.transform.position.y >= Camera.main.transform.position.x + offset_camera_x * 2)
            {
                change_direction = false;
                if (first_shot)
                {
                    first_shot_position = Random.Range(Camera.main.transform.position.x - offset_camera_x * 2, Camera.main.transform.position.x + offset_camera_x * 1.5f);
                    can_first_shot = true;
                    first_shot = false;
                    eyes_changed = false;


                }
            }

        }
       
        //Main Direction
        if(change_direction && !stop)
        {
            MoveUp();
        }


        // First Kamehameha
        if (gameObject.transform.position.y <= first_shot_position && can_first_shot)
        {
            can_first_shot = false;
            stop = true;
            shot_once = true;

        }

        // Second Kamehameha
        if (gameObject.transform.position.y >= second_shot_position && can_second_shot)
        {
            can_second_shot = false;
            stop = true;
            shot_once = true;
            first_shot = true;

        }
        // Enemy prepare shot
        if ((gameObject.transform.position.y <= first_shot_position +3 && can_first_shot) || (gameObject.transform.position.y >= second_shot_position - 3 && can_second_shot) && !eyes_changed)
        {
            if(can_prepare_particle && ParticlePrepareKamehameha != null)
            {
                ParticlePrepareKamehameha.SetActive(true);
                OpenEyesAnimation.Play("Enemy1_Kamehameha", 0, 0);
                can_prepare_particle = false;
            }

            //if (eye_scale <= 2.5f)
            //{
            //    eye_scale += 0.1f;
            //    eyes.transform.localScale = new Vector3(1, eye_scale, 1);

            //}
            //if(eye_position <= 0.2f)
            //{
            //    eye_position += 0.01f;
            //    eyes.transform.position = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y + eye_position);

            //}
            //if(eye_scale > 2.5f && eye_position > 0.2f)
            //{
            //    eyes_changed = true;

            //}
        }
        

        // Enemy_stopped
        if (stop) 
        {
            stop_timer += Time.deltaTime;
            if (stop_timer >= time_stopped/8)
            {
                if(shot_once)
                {
                    Kamehameha();
                    if(ParticlePrepareKamehameha != null)
                        ParticlePrepareKamehameha.SetActive(false);
                    shot_once = false;
                }
                if (stop_timer >= time_stopped)
                {
                   
                    eye_scale = 1;
                    eye_position = 0;
                    eyes.transform.localScale = new Vector3(1, eye_scale, 1);
                    eyes.transform.position = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y);
                    can_prepare_particle = true;

                    stop_timer = 0;
                    stop = false;
                }
            }
        }    


        if(get_hit)
        {
            EnemyGetHit();
        }
    }


    void MoveDown()
    {
        gameObject.transform.position = new Vector2(transform.position.x, transform.position.y - velocity * Time.deltaTime);
    }

    void MoveUp()
    {
        gameObject.transform.position = new Vector2(transform.position.x, transform.position.y + velocity * Time.deltaTime);
    }

    void MoveLeft()
    {
        gameObject.transform.position = new Vector2(transform.position.x- velocity * Time.deltaTime, transform.position.y );
    }

    void Kamehameha()
    {
        if (!dead)
        {
            Instantiate(Kamehameha_shot, new Vector3 (gameObject.transform.position.x - 3, gameObject.transform.position.y, gameObject.transform.position.z), Kamehameha_shot.transform.rotation);
        }
    }

    void EnemyGetHit()
    {
        get_hit_timer += Time.deltaTime;

        if(get_hit_timer < 1.2f)
        {

            if (get_hit_timer < 0.2f)
                Body.SetActive(false);
            else if (get_hit_timer < 0.4f)
                Body.SetActive(true);
            else if (get_hit_timer < 0.6f)
                Body.SetActive(false);
            else if (get_hit_timer < 0.8f)
                Body.SetActive(true);
            else if (get_hit_timer < 1)
                Body.SetActive(false);
            else
            {
                Body.SetActive(true);
                get_hit = false;
                get_hit_timer = 0;
            }

        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            if (life <= 1) 
            {

                rb.isKinematic = false;
                rb.AddForce(transform.right * -5000 * Time.deltaTime);
                Ears.SetActive(false);
                Eyes.SetActive(false);
                Mouth.SetActive(false);
                Destroy(ParticlePrepareKamehameha);
                //death.SetActive(true);
                GetComponent<EchoEffect>().enabled = false;
                start_fading = true;
                Destroy(gameObject, 0.5f);
                dead = true;

                if (!lvlGenScript.miniboss_enemy_defeat)
                {
                    lvlGenScript.miniboss_enemy_defeat = true;
                    lvlGenScript.Generator();
                }
            }          
        }
    }

    
}
