using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player_Jump : MonoBehaviour
{

    public float force;
    public float dash_force;
    public bool dash;
    public bool dashing;
    public bool dashing_down;
    private Rigidbody2D rb2D;
    Quaternion upDash;
    Quaternion downDash;
    Quaternion rightDash;
    public float smoothDash;
    private float prepare_dash_timer;
    public float prepare_dash_force;
    public float time_preparing_dash;
    public GameObject InitialPosition;

    private bool key_down;
    private bool arrow_down;
    private bool arrow_up;
    private bool arrow_right;
    private float initial_player_position_x;
    Vector2 firstPressPos;
    Vector2 secondPressPos;
    Vector2 currentSwipe;
    public float offset_wipe_width;
    public float offset_wipe_high;

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

    public GameObject MoneyParticle1;
    public GameObject MoneyParticle2;
    public GameObject MoneyParticle3;

    // Start is called before the first frame update
    private void Awake()
    {
        dead = false;
        rb2D = gameObject.GetComponent<Rigidbody2D>();
        initial_player_position_x = gameObject.transform.position.x;

        forwardRotation = Quaternion.Euler(0, 0, 30);
        downRotation = Quaternion.Euler(0, 0, -70);

        upDash = Quaternion.identity;
        downDash = Quaternion.Euler(0, 0, 180);
        rightDash = Quaternion.Euler(0, 0, -90);

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
        //Dash
        if(dash)
        {
            //Timer to prepare dash
            prepare_dash_timer += Time.deltaTime;
            PrepareDash();
            dashing = true;

            rb2D.gravityScale = 0;

            if (prepare_dash_timer >= time_preparing_dash)
            {
                rb2D.gravityScale = 3;
                Dash();
                prepare_dash_timer = 0; //Reset timer
                dash = false; //Dash Finished
            }
        }


        //Touch System Swipe UP DOWN RIGHT  from here-> https://forum.unity.com/threads/swipe-in-all-directions-touch-and-mouse.165416/
        //=============================================================================================

        if (Input.touches.Length > 0)
        {
            Touch t = Input.GetTouch(0);
            if (t.phase == TouchPhase.Began)
            {
                //save began touch 2d point
                firstPressPos = new Vector2(t.position.x, t.position.y);
            }
            if (t.phase == TouchPhase.Ended)
            {
                //save ended touch 2d point
                secondPressPos = new Vector2(t.position.x, t.position.y);

                //create vector from the two points
                currentSwipe = new Vector3(secondPressPos.x - firstPressPos.x, secondPressPos.y - firstPressPos.y);

                //normalize the 2d vector
                //currentSwipe.Normalize();

                //swipe upwards
                if (currentSwipe.y > offset_wipe_high && currentSwipe.x > -offset_wipe_width && currentSwipe.x < offset_wipe_width && !dead && !dashing)
                {
                    rb2D.velocity = Vector2.zero;
                    transform.rotation = Quaternion.Lerp(transform.rotation, upDash, smoothDash * Time.deltaTime);

                    arrow_up = true;
                    dash = true;
                }
                //swipe down
                if (currentSwipe.y < -offset_wipe_high && currentSwipe.x > -offset_wipe_width && currentSwipe.x < offset_wipe_width && !dead && !dashing)
                {
                    rb2D.velocity = Vector2.zero;
                    transform.rotation = Quaternion.Lerp(transform.rotation, downDash, smoothDash * Time.deltaTime);

                    arrow_down = true;
                    dash = true;
                    dashing_down = true;
                }
                //swipe right
                if (currentSwipe.x > offset_wipe_high && currentSwipe.y > -offset_wipe_width && currentSwipe.y < offset_wipe_width && !dead && !dashing)
                {
                    rb2D.velocity = Vector2.zero;
                    transform.rotation = rightDash;

                    arrow_right = true;
                    dash = true;
                }
                ////swipe left
                //if (currentSwipe.x < 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f && !dead && !dashing)
                //{
                //    Debug.Log("left swipe");
                //}
            }
        }
        //=============================================================================================


        if (Input.GetKeyDown("up") && !dead && !dashing)
        {
            rb2D.velocity = Vector2.zero;
            transform.rotation = Quaternion.Lerp(transform.rotation, upDash, smoothDash * Time.deltaTime);

            arrow_up = true;
            dash = true;
        }
        if (Input.GetKeyDown("down") && !dead && !dashing)
        {
            rb2D.velocity = Vector2.zero;
            transform.rotation = Quaternion.Lerp(transform.rotation, downDash, smoothDash * Time.deltaTime);

            arrow_down = true;
            dash = true;
            dashing_down = true;
        }
        if (Input.GetKeyDown("right") && !dead && !dashing)
        {
            rb2D.velocity = Vector2.zero;
            transform.rotation = rightDash;

            arrow_right = true;
            dash = true;
        }
       

        //Jump
        if ((Input.GetKeyDown("space") || Input.touchCount > 0) && !key_down && !dead && !dashing)
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
        else if (!dash)
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
        
        if (dead) //Flip when die and SetActive Restart Button
        {
            gameObject.transform.rotation = new Quaternion(gameObject.transform.rotation.x + 10, gameObject.transform.rotation.y, gameObject.transform.rotation.z, gameObject.transform.rotation.w);
            timer += Time.deltaTime;
            if (timer > time_death)
            {
                Restart.gameObject.SetActive(true);
            }
        }
        
    }


    void PrepareDash()
    {
        rb2D.velocity = Vector2.zero;

        if (arrow_up)
        {
            rb2D.AddForce(Vector2.down * prepare_dash_force);
        }
        else if (arrow_down)
        {
            rb2D.AddForce(Vector2.up * prepare_dash_force);
        }
        else if (arrow_right)
        {
            rb2D.AddForce(Vector2.left * prepare_dash_force);
            rb2D.constraints = RigidbodyConstraints2D.None;
        }
    }

    void Dash()
    {
        if (arrow_up)
        {
            rb2D.AddForce(Vector2.up * dash_force);
            arrow_up = false;
        }

        else if (arrow_down)
        {
            rb2D.AddForce(Vector2.down * dash_force);
            arrow_down = false;
        }

        else if (arrow_right)
        {
            rb2D.AddForce(Vector2.right * dash_force);
            arrow_right = false;
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Obstacle")
        {
            cameraShake.Shake(0.8f, 0.3f);
            dead = true;
            rb2D.AddForce(transform.right * -force);
            rb2D.AddForce(transform.up * force);
            dashing = false;

        }
        if (collision.transform.tag == "DeathFloor")
        {
            cameraShake.Shake(0.1f, 0.2f);
            dead = true;
            rb2D.AddForce(transform.right * (force / 1.5f));
            rb2D.AddForce(transform.up * (force / 1.5f));
            dashing = false;
        }
        if (collision.transform.tag == "Enemy")
        {
            if (dashing)
            {
                cameraShake.Shake(0.2f, 0.2f);
                dashing = false;

                if(dashing_down)
                {
                    rb2D.AddForce(transform.up * -force*1.5f);
                    dashing_down = false;
                    Debug.Log("WWWW");
                }
            }
            else
            {
                cameraShake.Shake(0.1f, 0.2f);
                dead = true;
                rb2D.AddForce(transform.right * (force / 1.5f));
                rb2D.AddForce(transform.up * (force / 1.5f));
                dashing = false;
            }
        }

        if (collision.transform.tag == "Ceiling")
        {
            if(dashing)
            {
                cameraShake.Shake(0.2f, 0.2f);
                dashing = false;
            }
            else
                cameraShake.Shake(0.1f, 0.1f);
        }
        
        if (collision.transform.tag == "Wall")
        {
            cameraShake.Shake(0.2f, 0.2f);

            transform.rotation = Quaternion.identity;
            rb2D.velocity = Vector2.zero;
            rb2D.constraints = RigidbodyConstraints2D.FreezeRotation;

            rb2D.AddForce(Vector2.left * dash_force/4);
            InitialPosition.SetActive(true);            
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.transform.tag == "Enemy") //  Enemy Shot
        {
            if (dashing || Enemy_1.dead)
            {
                cameraShake.Shake(0.2f, 0.2f);
            }
            else
            {
                cameraShake.Shake(0.1f, 0.2f);
                dead = true;
                rb2D.AddForce(transform.right * (force / 1.5f));
                rb2D.AddForce(transform.up * (force / 1.5f));
                dashing = false;
            }
        }

        if (collision.transform.tag == "Score")
        {
            Score.score_value++;

        }
        if (collision.transform.tag == "Basic_Money")
        {
            Money.player_money_value ++;
            PlayerPrefs.SetInt("Money", Money.player_money_value);

            Instantiate(MoneyParticle1, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
        }
        if (collision.transform.tag == "Normal_Money")
        {
            Money.player_money_value += 5;
            PlayerPrefs.SetInt("Money", Money.player_money_value);

            Instantiate(MoneyParticle2, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
        }
        if (collision.transform.tag == "Super_Money")
        {
            Money.player_money_value += 10;
            PlayerPrefs.SetInt("Money", Money.player_money_value);

            Instantiate(MoneyParticle3, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
        }
        
        if (collision.transform.tag == "InitialPosition")
        {
            rb2D.constraints = RigidbodyConstraints2D.None | RigidbodyConstraints2D.FreezePositionX;
            InitialPosition.SetActive(false);
            dashing = false;
        }

    }
}
