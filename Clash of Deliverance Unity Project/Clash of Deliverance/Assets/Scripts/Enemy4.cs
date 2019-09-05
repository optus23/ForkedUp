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
    private float degree;
    private float return_degree;
    Vector3 Player_pos_right;

    // Start is called before the first frame update
    void Start()
    {
        detect_player_position = true;
        half_velocity = velocity / 4;

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
                detect_player_position = false;
                velocity = half_velocity * 4;

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


                if (gameObject.transform.position.x <= Camera.main.transform.position.x + offset_camera_x && !stop)
                {
                    if(timer_rotate_right > 1f)
                    {
                        //MoveRight();
                        detect_player_position = false;
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
                        if(return_degree >= 0)
                        {
                            return_degree = -1 * (Mathf.Rad2Deg * Mathf.Atan(3.8f / return_distance.y) + 180);
                        }
                       
                        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, return_degree), 6 * Time.deltaTime);
                        detect_player_position = true;

                    }
                    if (gameObject.transform.position.x >= Camera.main.transform.position.x + offset_camera_x/2)
                    {
                        velocity = half_velocity;
                    }
                    else
                        velocity = half_velocity * 4;

                }
                else if(!stop)
                {
                    transform.position = new Vector2(offset_camera_x, transform.position.y);
                    return_initial_pos = false;
                    detect_player_position = true;
                    timer_rotate_right = 0;
                    velocity = half_velocity * 4;
                }              
            }
        }
    }

    private void DetectWayPoint()
    {
        
        if (finish_rotation)
        {          
            Vector2 vec2;
            vec2 = Vector2.MoveTowards(transform.position, new Vector2(-3.8f, Player_pos.y), velocity * Time.deltaTime);
            this.transform.position = vec2;

        }
        else
            StartCoroutine("Rotate");

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
            Vector2 distance = Player_pos - transform.position;
            degree = Mathf.Rad2Deg* Mathf.Atan(distance.y / -3.8f) + 90;                     
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, degree), 7 * Time.deltaTime);

            yield return new WaitForSeconds(0.1f);
        }
        finish_rotation = true;
        stop = false;
        StopCoroutine("Rotate");


    }

}
