using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagScore : MonoBehaviour
{
    public GameObject player;
    public float player_position_x;

    public float Unity_screen_min;
    public float Unity_screen_max;
    public float Score_MIN;
    public float Score_MAX;

    public float force;
    private Rigidbody2D rb2D;
    private Quaternion initial_player_rotation;
    public GameObject Dust;

    private float timer_player_respawn;

    // Start is called before the first frame update
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        initial_player_rotation = new Quaternion(gameObject.transform.rotation.x, gameObject.transform.rotation.y, gameObject.transform.rotation.z, gameObject.transform.rotation.w);

    }


    [Range(0, 150)]
    public int test;

    // Update is called once per frame
    void Update()
    {
        TimerPlayerRespawn();


        //
        //  1  MIN = -2.54 min    
        //  100 MAX = 2.54 max
        //  50 MID = 0 mid
        // N = Score, n = unity meters

        //  N*((max-min)/(MAX-MIN) - max = n      FORMULA PARA TRADUCIR Score.score_value to screen unity meters
       
        player_position_x = /*test*/ Score.score_value * ((Unity_screen_max - Unity_screen_min) / (Score_MAX - Score_MIN)) - Unity_screen_max;
        player.transform.position = new Vector2(player_position_x, player.transform.position.y);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "DanceFloor" && collision.gameObject.layer == 5)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
            //transform.position = new Vector2(transform.position.x, 0.4f);
            //rb2D.AddRelativeForce(transform.up * 0);
            rb2D.AddForce(transform.up * force, ForceMode2D.Impulse);
            //rb2D.AddForceAtPosition(transform.up * 2 * force, new Vector2(transform.position.x, 0.4f));
            Instantiate(Dust, new Vector2(transform.position.x, transform.position.y), Dust.transform.rotation);

        }
        if(collision.gameObject.layer == 0)
        {
            Collider2D player_collider = GetComponent<Collider2D>();
            Collider2D tube_collider = collision.gameObject.GetComponent<Collider2D>();
            Physics2D.IgnoreCollision(tube_collider, player_collider);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
       
    }

    void TimerPlayerRespawn()
    {
        timer_player_respawn += Time.deltaTime;

        if (timer_player_respawn >= 2.65)
        {
            timer_player_respawn = 0; //Reset timer
            gameObject.SetActive(false);
        }
        gameObject.SetActive(true);
    }
}
