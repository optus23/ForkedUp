using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuAnimationPlayer : MonoBehaviour
{
    public float force;
    private Rigidbody2D rb2D;
    private float initial_player_position_x;
    private Quaternion initial_player_rotation;
    public GameObject Dust;

    // Start is called before the first frame update
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        initial_player_position_x = gameObject.transform.position.x;
        initial_player_rotation = new Quaternion(gameObject.transform.rotation.x, gameObject.transform.rotation.y, gameObject.transform.rotation.z, gameObject.transform.rotation.w);

    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position = new Vector2(initial_player_position_x, gameObject.transform.position.y);
        gameObject.transform.rotation = initial_player_rotation;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.transform.tag == "DanceFloor")
        {
            rb2D.AddForce(transform.up * force);
            Instantiate(Dust, new Vector2 (transform.position.x , transform.position.y ), Dust.transform.rotation);


        }
    }

    
}
