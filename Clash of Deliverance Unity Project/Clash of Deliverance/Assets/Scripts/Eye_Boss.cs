using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eye_Boss : MonoBehaviour
{

    [SerializeField]
    private int life;
    public bool get_hit;
    Boss_Manager Boss;

    GameObject player;
    public GameObject Black_Eye;
    public GameObject White_Eye;

    public float Unity_screen_min;
    public float Unity_screen_max;
    public float Player_pos_MIN;
    public float Player_pos_MAX;

    public Quaternion Eye_Close;
    public Quaternion Eye_Normal;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        Boss = GetComponentInParent<Boss_Manager>();

        Eye_Close = Quaternion.Euler(90, 0, 90);
        Eye_Normal = Quaternion.Euler(0, 0, 90);
    }

    private void Update()
    {
        FollowPlayer();

        Blink();
    }
    

   

    //  1  MIN = 0.6 min    
    //  100 MAX = 1.4 max
    // N = Position player, n = unity meters

    //  N*((max-min)/(MAX-MIN) - max = n      FORMULA PARA TRADUCIR Position player to screen unity meters

    void FollowPlayer()
    {
        Vector2 _vec2;
        _vec2 = new Vector2(Black_Eye.transform.position.x, player.transform.position.y * ((Unity_screen_max - Unity_screen_min) / (Player_pos_MAX - Player_pos_MIN)) + Unity_screen_max - 0.4f);
        Black_Eye.transform.position = _vec2;
    }

    void Blink()
    {
       White_Eye.transform.rotation = Quaternion.Lerp(Black_Eye.transform.rotation, Eye_Close, 4 * Time.deltaTime);
    }

    public bool IsDead()
    {
        if (life <= 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            life--;
            get_hit = true;
        }
    }
}
