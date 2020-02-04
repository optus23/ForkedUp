using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player_Jump : MonoBehaviour
{
   
    Enemy_2 enemy_2;
    Enemy_3 enemy_3;
    Enemy4 enemy_4;

    //public GameObject enemy1;
    public GameObject enemy2;
    public GameObject enemy3;
    public GameObject enemy4;

    Score score;
    public GameObject ScoreNum;
    public GameObject ScoreDead;
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
    private float prepare_enemy_2_inmune_timer;
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
    public static bool despawn_deadplayer;
    private bool restart_shake;
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

    public bool enemy2_shot_inmune;

    private Vector3 simulated_position_player;
    // Start is called before the first frame update
    private void Awake()
    {
        rb2D = gameObject.GetComponent<Rigidbody2D>();
        //enemy_3 = enemy3.GetComponent<Enemy_3>();
        //enemy_2 = enemy2.GetComponent<Enemy_2>();
        //enemy_4 = enemy4.GetComponent<Enemy4>();


        initial_player_position_x = gameObject.transform.position.x;

        forwardRotation = Quaternion.Euler(0, 0, 30);
        downRotation = Quaternion.Euler(0, 0, -70);

        upDash = Quaternion.identity;
        downDash = Quaternion.Euler(0, 0, 180); 
        rightDash = Quaternion.Euler(0, 0, -90);
        dustRotation = Quaternion.Euler(0, 0, 45);


        simulated_position_player = transform.position;
    }

    void Start()
    {
        //First Jump without Input Keydown
        rb2D.velocity = Vector2.zero;
        rb2D.AddForce(Vector2.up * force);
        dead = false;
        despawn_deadplayer = false;
        transform.rotation = Quaternion.Lerp(forwardRotation, transform.rotation, 2 * Time.deltaTime);
        restart_shake = true;

        ScoreDead.SetActive(false);
        ScoreNum.SetActive(true);


    }

    // Update is called once per frame
    void Update()
    {

        //  Dash
        if (dash)
        {
            //Timer to prepare dash
            prepare_dash_timer += Time.deltaTime;
            dashing = true;
            PrepareDash();

            rb2D.gravityScale = 0;

            if (prepare_dash_timer >= time_preparing_dash)
            {
                rb2D.gravityScale = 3;
                Dash();
                prepare_dash_timer = 0; //Reset timer
                dash = false; //Dash Finished
            }
        }
        else if(!dashing)
        {
            transform.position = new Vector3(simulated_position_player.x, transform.position.y, transform.position.z);

        }
        //  Enemy 2 inmune timer
        if (enemy2_shot_inmune)
        {
            //  Timer enmy 2 shot inmune
            prepare_enemy_2_inmune_timer += Time.deltaTime;

            if (prepare_enemy_2_inmune_timer >= 2)
            {
                enemy2_shot_inmune = false;
                prepare_enemy_2_inmune_timer = 0; //Reset timer

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
                //    
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

            //Animation
            transform.rotation = forwardRotation;
            transform.rotation = new Quaternion(0, 0, 0.3f, 1f);

            //Force
            rb2D.angularVelocity = 0f;
            rb2D.velocity = Vector2.zero;
            rb2D.AddForce(Vector2.up * force);
            key_down = true;

           

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
            if (timer > time_death && restart_shake)
            {
                Restart.gameObject.SetActive(true);
                cameraShake.Shake(0.1f, 0.3f);
                restart_shake = false;
                ScoreNum.SetActive(false);
                ScoreDead.SetActive(true);
                despawn_deadplayer = true;
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
            //dead = true;
            rb2D.AddForce(transform.right * (force / 1.5f));
            rb2D.AddForce(transform.up * (force / 1.5f));
            dashing = false;
        }
        if (collision.transform.tag == "Enemy_Up")
        {           
            if (dashing)
            {               
                cameraShake.Shake(0.2f, 0.2f);
                dashing = false;
                Score.score_value++;
                Score.player_pickup_score = true;
                enemy2_shot_inmune = true;

                if(enemy_3 != null)
                    enemy_3.destroy_shot = true;
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
        if (collision.transform.tag == "Enemy_Down")
        {
            if (dashing)
            {
                if (dashing_down)
                {
                    transform.rotation = downDash;
                    rb2D.AddForce(transform.up * -force * 1.3f);
                    dashing_down = false;
                }
                cameraShake.Shake(0.2f, 0.2f);
                dashing = false;
                Score.score_value++;
                Score.player_pickup_score = true;


                if (enemy_3 != null)
                    enemy_3.destroy_shot = true;
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
            if (dashing)
            {
                cameraShake.Shake(0.2f, 0.2f);

                dashing = false;
            }
            else
                cameraShake.Shake(0.1f, 0.1f);

            transform.position = new Vector3(simulated_position_player.x, transform.position.y, transform.position.z);

        }

        if (collision.transform.tag == "Wall")
        {
            cameraShake.Shake(0.2f, 0.2f);
            
            transform.rotation = Quaternion.identity;
            rb2D.velocity = Vector2.zero;
            rb2D.constraints = RigidbodyConstraints2D.FreezeRotation;

            rb2D.AddForce(Vector2.left * dash_force / 4);
            InitialPosition.SetActive(true);
        }

        if (collision.transform.tag == "Enemy_Wall")
        {
            if (dashing)
            {
                cameraShake.Shake(0.2f, 0.2f);

                transform.rotation = Quaternion.identity;
                rb2D.velocity = Vector2.zero;
                rb2D.constraints = RigidbodyConstraints2D.FreezeRotation;

                rb2D.AddForce(Vector2.left * dash_force / 4);
                InitialPosition.SetActive(true);
                dashing = false;
                Enemy_1 enemy_1;
                enemy_1 = collision.gameObject.GetComponent<Enemy_1>();

                enemy_1.life--;
                if (enemy_1.life >= 1)
                    enemy_1.get_hit = true;

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

        if (collision.transform.tag == "Enemy_4")
        {
            if (dashing)
            {
                transform.rotation = Quaternion.identity;
                if (dashing_down)
                {
                    transform.rotation = downDash;
                    rb2D.AddForce(transform.up * -force * 1.3f);
                    dashing_down = false;
                }
                dashing = false;
                Score.score_value++;
                Score.player_pickup_score = true;
                cameraShake.Shake(0.2f, 0.2f);

                rb2D.velocity = Vector2.zero;
                rb2D.constraints = RigidbodyConstraints2D.FreezeRotation;

                rb2D.AddForce(Vector2.left * dash_force / 4);
                InitialPosition.SetActive(true);
                dashing = false;            

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
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.transform.tag == "Enemy_Shot") //  Enemy Shot
        {


            if (dashing || enemy2_shot_inmune)
            {
                cameraShake.Shake(0.2f, 0.2f);
                enemy2_shot_inmune = false;
            }
            else
            {
                cameraShake.Shake(0.1f, 0.2f);
                dead = true;
                rb2D.AddForce(transform.right * (force * 1.5f));
                transform.rotation = downDash;
                rb2D.AddForce(transform.up * (-force * 1.5f));
                dashing = false;
            }
        }

        if (collision.transform.tag == "Score")
        {
            Score.score_value++;
            Score.player_pickup_score = true;


        }
        if (collision.transform.tag == "Basic_Money")
        {
            Money.player_money_value++;
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
