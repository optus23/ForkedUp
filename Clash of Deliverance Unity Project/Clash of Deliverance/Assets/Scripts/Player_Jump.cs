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

    public static bool dead;
    public CameraShake cameraShake;
    private float time_death = 1.5f;
    private float timer;
    public GameObject Restart;

    Quaternion downRotation;
    Quaternion forwardRotation;
    public float smoothRotation;
    private float timer_animation;
    public float start_rotating;

    Quaternion dustRotation;
    public GameObject Dust;

    Touch touch;


    // Start is called before the first frame update
    private void Awake()
    {
        rb2D = gameObject.GetComponent<Rigidbody2D>();
        initial_player_position_x = gameObject.transform.position.x;

        forwardRotation = Quaternion.Euler(0, 0, 30);
        downRotation = Quaternion.Euler(0, 0, -70);


        dustRotation = Quaternion.Euler(0, 0, 45);
    }

    void Start()
    {
        //First Jump without Input Keydown
        rb2D.velocity = Vector2.zero;
        rb2D.AddForce(Vector2.up * force);
        dead = false;
        transform.rotation = Quaternion.Lerp(forwardRotation, transform.rotation, 2 * Time.deltaTime);
    }

    // Update is called once per frame
    void Update()
    {

        if ((Input.GetKeyDown("space") || Input.touchCount > 0) && !key_down && !dead)
        {
            if (Input.touchCount > 0)
                touch = Input.GetTouch(0);

            rb2D.velocity = Vector2.zero;
            rb2D.AddForce(Vector2.up * force);
            key_down = true;

            //Animation
            transform.rotation = forwardRotation;
            Instantiate(Dust, new Vector2(transform.position.x, transform.position.y), dustRotation);
            Dust.transform.rotation = dustRotation;

            timer_animation = 0f;
            smoothRotation = 1;
        }
        else if ((Input.GetKeyUp("space") || Input.touchCount == 0) && key_down)
        {
            key_down = false;

        }
        else
        {
            timer_animation += Time.deltaTime;
            if (timer_animation > start_rotating)
            {
               
                if (smoothRotation >= 4)
                    smoothRotation += Mathf.Sqrt((smoothRotation / 10));
                else
                    smoothRotation += Mathf.Sqrt((smoothRotation / 200));
                
                if (smoothRotation >= 10)
                    smoothRotation = 10;

                transform.rotation = Quaternion.Lerp(transform.rotation, downRotation, smoothRotation * Time.deltaTime);

            }

        }
        
        if (!dead)
        {
            //Keep Vector initial position.x
            gameObject.transform.position = new Vector2(initial_player_position_x, gameObject.transform.position.y);

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
        //if (collision.transform.tag == "Obstacle")
        //{
        //    cameraShake.Shake(0.8f, 0.3f);
        //    dead = true;
        //    rb2D.AddForce(transform.right * -force);
        //    rb2D.AddForce(transform.up * force);

        //}
        //if (collision.transform.tag == "DeathFloor")
        //{
        //    cameraShake.Shake(0.1f, 0.2f);
        //    dead = true;
        //    rb2D.AddForce(transform.right * (force / 1.5f));
        //    rb2D.AddForce(transform.up * (force / 1.5f));
        //}
        if (collision.transform.tag == "Score")
        {
            Score.score_value++;

        }
        if (collision.transform.tag == "Basic_Money")
        {
            Money.player_money_value += 5;
            PlayerPrefs.SetInt("Money", Money.player_money_value);
        }
    }
}
