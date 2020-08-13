using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eye_Boss : MonoBehaviour
{
    public enum EyeState
    {
        BLINK,
        BLINK_UP,
        FOLLOW,
        GETHIT,
        NONE,
    }

    public EyeState state;

    public int life;
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
    [SerializeField]
    private float scale_eye_open;

    private float initial_position_y;

    public Animator getHitAnimation;

    private void Start()
    {
        getHitAnimation = GetComponentInChildren<Animator>();
        getHitAnimation.Play("GetHit", 0, 1);
        player = GameObject.FindGameObjectWithTag("Player");
        Boss = GetComponentInParent<Boss_Manager>();

        state = EyeState.FOLLOW;

        initial_position_y = Black_Eye.transform.position.y;

        Eye_Close = Quaternion.Euler(88, 0, 0);
        Eye_Normal = Quaternion.Euler(0, 0, 0);

        scale_eye_open = Black_Eye.transform.localScale.x;
    }

    private void Update()
    {      
        switch (state)
        {
            case EyeState.BLINK:
                Blink();
                break;

            case EyeState.BLINK_UP:
                BlinkUp();
                break;

            case EyeState.FOLLOW:
               
                FollowPlayer();
                break;

            case EyeState.GETHIT:
                
                if (life <= 2)
                    scale_eye_open = scale_eye_close;
                
                break;
        }
        //Debug
        if (Input.GetKeyDown(KeyCode.F5))
            life = 0;
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

    void BlinkUp()
    {
        White_Eye.transform.rotation = Quaternion.Lerp(White_Eye.transform.rotation, Eye_Normal, 4 * Time.deltaTime);
        StartCoroutine("ScaleEyeOpen");
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
            Black_Eye.transform.position = Vector2.MoveTowards(Black_Eye.transform.position, new Vector2(Black_Eye.transform.position.x, initial_position_y), 4 * Time.deltaTime);

        }
          
    }

    IEnumerator ScaleEyeOpen()
    {
        float actual_scale = Black_Eye.transform.localScale.x;

        if (Black_Eye.transform.localScale.x >= scale_eye_open)
        {
            state = EyeState.FOLLOW;   
            StopCoroutine("ScaleEyeOpen");
        }
        else
        {
            yield return new WaitForSeconds(0.02f);
            Black_Eye.transform.localScale = new Vector3(actual_scale += 0.012f, actual_scale += 0.012f, actual_scale += 0.012f);
        }

    }
}
