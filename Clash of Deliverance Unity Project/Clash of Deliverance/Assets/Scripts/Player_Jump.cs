using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player_Jump : MonoBehaviour
{

    public float force;
    private Rigidbody2D rb2D;
    private bool key_down;
    private float initial_player_position_x;
    private Quaternion initial_player_rotation;

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
    }
    
    // Update is called once per frame
    void Update()
    {
       
        if((Input.GetKeyDown("space") || Input.GetKeyDown(KeyCode.Mouse0))&& !key_down)
        {
            rb2D.velocity = Vector2.zero;
            rb2D.AddForce(transform.up * force);
            key_down = true;
        }
        else if ((Input.GetKeyUp("space") || Input.GetKeyDown(KeyCode.Mouse0)) && key_down)
        {
            key_down = false;
        }

        //Keep Vector initial position x & Quaterion rotation gameObject the same all time
        gameObject.transform.position = new Vector2(initial_player_position_x, gameObject.transform.position.y);
        gameObject.transform.rotation = initial_player_rotation;

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "DeathFloor" || collision.transform.tag == "Obstacle")
        {
            Debug.Log("You are dead!");
            SceneManager.LoadScene("Main Menu");
            //SceneManager.UnloadScene("Clash of Deliverance");
        }
        if (collision.transform.tag == "Score")
        {
            Score.score_value += 1;
        }
    }
}
