using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemy4 : MonoBehaviour
{

    [SerializeField]
    private float velocity;
    [SerializeField]
    private Transform Goal;

    private bool detect_player_position;
    private Vector3 Player_pos;
    public int offset_camera_x;
    private bool stop;
    private bool return_initial_pos;
    private bool has_entered;
    private bool finish_rotation;
    private float timer_rotate_right;
    private float half_velocity;
    private float double_velocity;
    private float degree;
    private float return_degree;
    Vector3 Player_pos_right;
    public GameObject AngryMouth;
    public GameObject HappyMouth;

    private float displacement;
    float temorizador_DEBUG;
    bool calcule_velocity;
    bool calculate_velocity2;

    SpriteRenderer renderer;
    SpriteRenderer happy_renderer;
    SpriteRenderer angry_renderer;

    public bool dead;
    public bool start_fading;

    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponent<SpriteRenderer>();
        happy_renderer = HappyMouth.GetComponent<SpriteRenderer>();
        angry_renderer = AngryMouth.GetComponent<SpriteRenderer>();

        detect_player_position = true;
        half_velocity = velocity / 4;
        double_velocity = half_velocity * 4;

    }

    // Update is called once per frame
    void Update()
    {              

        //  Move Left
        if (gameObject.transform.position.x >= Camera.main.transform.position.x + offset_camera_x && !has_entered && !stop)
        {
            MoveLeft();
            velocity = half_velocity;
        }
        else
        {
            has_entered = true;

            //  Guide Enemy Attack
            if (detect_player_position)
            {
                Player_pos = new Vector3 (Goal.position.x - offset_camera_x/2, Goal.position.y, Goal.position.z);
                velocity = double_velocity;

                detect_player_position = false;
            }

            if (transform.position.x > Player_pos.x && !return_initial_pos)
            {
                DetectWayPoint();
                if (gameObject.transform.position.x <= Camera.main.transform.position.x - offset_camera_x/2)
                {
                    velocity = half_velocity;
                    
                }
            }
            else
            {
                return_initial_pos = true;
                finish_rotation = false;            
                calcule_velocity = false;


                if (gameObject.transform.position.x <= Camera.main.transform.position.x + offset_camera_x && !stop)
                {
                    if(timer_rotate_right > 1f)
                    {
                        //MoveRight();

                        if (!calculate_velocity2)
                        {
                            Vector2 distance_vector = Player_pos_right - transform.position;
                            displacement = Mathf.Sqrt(Mathf.Pow(distance_vector.x, 2) + Mathf.Pow(distance_vector.y, 2));
                            velocity = displacement / 0.35f;
                            calculate_velocity2 = true;
                            

                        }

                        detect_player_position = false;
                        AngryMouth.SetActive(true);
                        HappyMouth.SetActive(false);

                        Vector2 _vec2;
                        _vec2 = Vector2.MoveTowards(transform.position, new Vector2(3.8f, Player_pos_right.y), velocity * Time.deltaTime);
                        this.transform.position = _vec2;
                    }                  
                    else
                    {
                        Player_pos_right = new Vector3(Player_pos.x + 3.8f, Player_pos.y, Player_pos.z);
                        timer_rotate_right += Time.deltaTime;
                        
                        Vector2 return_distance = Player_pos_right - transform.position;
                        return_degree = -1 * (Mathf.Rad2Deg * Mathf.Atan(3.8f / return_distance.y));
                        AngryMouth.SetActive(false);
                        HappyMouth.SetActive(true);
                        if (return_degree >= 0)
                        {
                            return_degree = -1 * (Mathf.Rad2Deg * Mathf.Atan(3.8f / return_distance.y) + 180);
                        }
                       
                        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, return_degree), 6 * Time.deltaTime);

                        //  Flip X effect 
                        renderer.flipX = true;
                        happy_renderer.flipX = true;
                        angry_renderer.flipX = true;
                        detect_player_position = true;
                        calculate_velocity2 = false;                     

                    }
                    if (gameObject.transform.position.x >= Camera.main.transform.position.x + offset_camera_x/2)
                    {
                        velocity = half_velocity;                      
                    }
                    else
                        velocity = double_velocity;

                }
                else if(!stop)
                {
                    transform.position = new Vector2(offset_camera_x, transform.position.y);
                    return_initial_pos = false;
                    detect_player_position = true;
                    timer_rotate_right = 0;
                    velocity = double_velocity;
                }              
            }
        }
    }

    private void DetectWayPoint()
    {
        
        if (finish_rotation)
        {          
            if(!calcule_velocity)
            {
                Vector2 distance_vector = Player_pos - transform.position;
                displacement = Mathf.Sqrt(Mathf.Pow(distance_vector.x, 2) + Mathf.Pow(distance_vector.y, 2));
                velocity = displacement / 0.35f;            
                calcule_velocity = true;
            }

            AngryMouth.SetActive(true);
            HappyMouth.SetActive(false);
            Vector2 vec2;
            vec2 = Vector2.MoveTowards(transform.position, new Vector2(-3.8f, Player_pos.y), velocity * Time.deltaTime);
            this.transform.position = vec2;

        }
        else
        {
            detect_player_position = true;

            Vector2 distance = Player_pos - transform.position;
            degree = Mathf.Rad2Deg * Mathf.Atan(distance.y / -3.8f) + 90;
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, degree), 7 * Time.deltaTime);

            //  Flip X effect 
            renderer.flipX = false;
            happy_renderer.flipX = false;
            angry_renderer.flipX = false;

            StartCoroutine("Rotate");
        }

    }

    private void MoveLeft()
    {
        gameObject.transform.position = new Vector2(gameObject.transform.position.x - velocity * Time.deltaTime, gameObject.transform.position.y);

    }

    private void MoveRight()
    {
        gameObject.transform.position = new Vector2(gameObject.transform.position.x + velocity * Time.deltaTime, gameObject.transform.position.y);
    }

    IEnumerator Rotate()
    {
        for (float f = 0.5f; f > 0; f -= 0.1f)
        {
           
            stop = true;
            AngryMouth.SetActive(false);
            HappyMouth.SetActive(true);
            yield return new WaitForSeconds(0.1f);
        }
        //renderer.flipX = true;
        //happy_renderer.flipX = true;
        //angry_renderer.flipX = true;
        finish_rotation = true;
        stop = false;
        StopCoroutine("Rotate");
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.tag == "Player")
        {
            dead = true;
            start_fading = true;
            Destroy(gameObject, 2);
        }
    }
}
