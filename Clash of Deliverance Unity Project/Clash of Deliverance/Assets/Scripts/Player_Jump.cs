using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player_Jump : MonoBehaviour
{

    public float force;
    private Rigidbody2D rb2D;
    private bool key_down;
    private float initial_player_position_x;
    private Quaternion initial_player_rotation;
    public static bool dead;
    public CameraShake cameraShake;
    private float time_death = 1.5f;
    private float timer;
    public GameObject Restart;

    // Start is called before the first frame update
    private void Awake()
    {
        rb2D = gameObject.GetComponent<Rigidbody2D>();
        initial_player_position_x = gameObject.transform.position.x;
        initial_player_rotation = new Quaternion(gameObject.transform.rotation.x, gameObject.transform.rotation.y, gameObject.transform.rotation.z, gameObject.transform.rotation.w);

    }

    void Start()
    {
        //First Jump without Input Keydown
        rb2D.velocity = Vector2.zero;
        rb2D.AddForce(transform.up * force);
        dead = false;
    }
    
    // Update is called once per frame
    void Update()
    {
       
        if(Input.GetKeyDown("space") && !key_down && !dead)
        {
            rb2D.velocity = Vector2.zero;
            rb2D.AddForce(transform.up * force);
            key_down = true;
        }
        else if (Input.GetKeyUp("space") && key_down)
        {
            key_down = false;
        }

        if (!dead)
        {
            //Keep Vector initial position x & Quaterion rotation gameObject the same all time
            gameObject.transform.position = new Vector2(initial_player_position_x, gameObject.transform.position.y);
            gameObject.transform.rotation = initial_player_rotation;
        }
        else //Flip when die and SetActive Restart Button
        {
            gameObject.transform.rotation = new Quaternion(gameObject.transform.rotation.x + 10, gameObject.transform.rotation.y, gameObject.transform.rotation.z, gameObject.transform.rotation.w);
            timer += Time.deltaTime;
            if (timer > time_death)
            {
                Restart.gameObject.SetActive(true);
            }
        }


    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Obstacle")
        {
            Debug.Log("You are dead!");

            cameraShake.Shake(0.8f, 0.3f);
            dead = true;
            rb2D.AddForce(transform.right * -force);
            rb2D.AddForce(transform.up * force);
            
        }
        if (collision.transform.tag == "DeathFloor")
        {
            Debug.Log("You are dead!");

            cameraShake.Shake(0.1f, 0.2f);
            dead = true;
            rb2D.AddForce(transform.right * (force/2));
            rb2D.AddForce(transform.up * (force/2));
        }
        if (collision.transform.tag == "Score")
        {
            Score.score_value += 1;
        }
    }
}
