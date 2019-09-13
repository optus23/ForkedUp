using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eye_Boss : MonoBehaviour
{
    enum EyeState
    {
        BLINK,
        FOLLOW,
        GETHIT,
        NONE,
    }

    EyeState state;

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

    Quaternion Eye_Close;
    Quaternion Eye_Normal;

    [SerializeField]
    private float scale_eye_close;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        Boss = GetComponentInParent<Boss_Manager>();

        Eye_Close = Quaternion.Euler(88, 0, 0);
        Eye_Normal = Quaternion.Euler(0, 0, 0);
    }

    private void Update()
    {

        switch(state)
        {
            case EyeState.BLINK:
                Blink();
                break;

            case EyeState.FOLLOW:
                FollowPlayer();
                break;

            case EyeState.GETHIT:
                // TO DO: Rotate around the eye, stroke screen, make little and close eye
                break;
        }
    }



      /**-*-*-*ALGORITM*-*-*-*
    
      1  MIN = 0.6 min    
      100 MAX = 1.4 max
     N = Position player, n = unity meters

      N* ((max-min)/(MAX-MIN) - max = n FORMULA PARA TRADUCIR Position player to screen unity meters*/

    void FollowPlayer()
    {
        Vector2 _vec2;
        _vec2 = new Vector2(Black_Eye.transform.position.x, player.transform.position.y * ((Unity_screen_max - Unity_screen_min) / (Player_pos_MAX - Player_pos_MIN)) + Unity_screen_max - 0.4f);
        Black_Eye.transform.position = _vec2;
    }

    void Blink()
    {
       White_Eye.transform.rotation = Quaternion.Lerp(White_Eye.transform.rotation, Eye_Close, 4 * Time.deltaTime);
        StartCoroutine("ScaleEyeClose");
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

    IEnumerator ScaleEyeClose()
    {
        float actual_scale = Black_Eye.transform.localScale.x;

        if (Black_Eye.transform.localScale.x <= scale_eye_close)
        {
            StopCoroutine("ScaleEyeClose");
        }
        else
        {
            yield return new WaitForSeconds(0.01f);
            Black_Eye.transform.localScale = new Vector3(actual_scale -= 0.012f, actual_scale -= 0.012f, actual_scale -= 0.012f);
        }

    }
}
