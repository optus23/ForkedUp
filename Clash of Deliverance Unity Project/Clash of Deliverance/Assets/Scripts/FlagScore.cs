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

    // Start is called before the first frame update
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        initial_player_rotation = new Quaternion(gameObject.transform.rotation.x, gameObject.transform.rotation.y, gameObject.transform.rotation.z, gameObject.transform.rotation.w);

    }
  

    // Update is called once per frame
    void Update()
    {
        //
        //  1  MIN = -2.54 min    
        //  100 MAX = 2.54 max
        //  50 MID = 0 mid
        //
        //  N*((max-min)/(MAX-MIN) - max = n      FORMULA PARA TRADUCIR Score.score_value to screen unity meters  (N = Score, n = unity meters)

        player_position_x = Score.score_value * ((Unity_screen_max - Unity_screen_min) / (Score_MAX - Score_MIN)) - Unity_screen_max;
        player.transform.position = new Vector2(player_position_x, player.transform.position.y);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.transform.tag == "DanceFloor")
        {
            rb2D.AddForce(transform.up * force);
            Instantiate(Dust, new Vector2(transform.position.x, transform.position.y), Dust.transform.rotation);


        }
    }
}
