using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Manager : MonoBehaviour
{
    public enum Phases
    {
        ENTER_BATTLEFIELD,
        TYPE_1,
        TYPE_2,
        TYPE_3,
        STATIC,
        FAKE_STATIC,
        GETHIT,
        NONE
    }

    public enum Type1State
    {
        PREPARE_SHOT,
        SHOT,
        FINISH_SHOT,
        NONE
    }
    public enum Type2State
    {
        PREPARE_SHOT,
        PHASE_1,
        PHASE_2,
        FINISH_SHOT,
        NONE
    }
    public enum Type3State
    {
        PREPARE_SHOT,
        SHOT,
        FINISH_SHOT,
        NONE
    }


    public Type1State type1_state;
    public Type2State type2_state;
    public Type3State type3_state;
    public Phases phases;
    public Phases last_attack_phase;
    public Phases last_fake_phase;

    [SerializeField]
    private float offset_camera_x;
    [SerializeField]
    private float velocity;
    private float timer_enter_battelfield;

    public Eye_Boss eye;
    public GameObject Body;
    public GameObject Shot;
    private int number_of_shots;
    [SerializeField]
    private int number_max_of_shots;
    public int number_of_directional_shots;

    public GameObject Kamehameha;
    public GameObject Prepare_Kamehameha;
    private float timer_kamehameha;

    public Transform Goal;
    public Transform Boss;

    public GameObject Eye;
    private CircleCollider2D Eye_collider;

    private float timer_next_phase;
    private float timer_fake_static;

    private float timer_change_type2_phase;
    private int type2_repeat_number;
    [SerializeField]
    private int type2_max_repeat_number;

    private int rand_type;

    private bool can_kamehameha;

    // Start is called before the first frame update
    void Start()
    {
        eye = GetComponentInChildren<Eye_Boss>();

        Eye_collider = GetComponentInChildren<CircleCollider2D>();
        Eye_collider.enabled = false;

        SetTypesNONE();        
    }

    // Update is called once per frame
    void Update()
    {
       
        switch (phases)
        {
            case Phases.ENTER_BATTLEFIELD:
                if (gameObject.transform.position.x >= Camera.main.transform.position.x + offset_camera_x)
                {
                    MoveLeft();
                }
                else
                {
                    if(timer_enter_battelfield > 1.5f)
                    {
                        rand_type = Random.Range(1, 3);
                        if (rand_type == 1)
                        {
                            phases = Phases.TYPE_1;
                            type1_state = Type1State.PREPARE_SHOT;
                        }
                        else if (rand_type == 2)
                        {
                            phases = Phases.TYPE_2;
                            type2_state = Type2State.PREPARE_SHOT;
                        }
                    }
                   else
                    {
                        timer_enter_battelfield += Time.deltaTime;
                    }
                }
                    break;

            case Phases.TYPE_1:
                switch(type1_state)
                {
                    case Type1State.PREPARE_SHOT:
                        Eye_collider.enabled = false;
                        InvokeFollowShot();
                        type1_state = Type1State.SHOT;
                        break;

                    case Type1State.SHOT:
                        if(number_of_shots >= number_max_of_shots)
                            type1_state = Type1State.FINISH_SHOT;
                        break;

                    case Type1State.FINISH_SHOT:
                        CancelInvoke();
                        CalculatePhases();
                        number_of_shots = 0;
                        last_attack_phase = Phases.TYPE_1;
                        break;

                    default:
                        type1_state = Type1State.NONE;
                        break;
                }
                break;
            case Phases.TYPE_2:
                switch(type2_state)
                {
                    case Type2State.PREPARE_SHOT:
                        Eye_collider.enabled = false;
                        type2_max_repeat_number = Random.Range(2, 5);
                        int random_number = Random.Range(0,2);

                        if(random_number == 0)
                            type2_state = Type2State.PHASE_1;
                        else
                            type2_state = Type2State.PHASE_2;

                        InvokeDirectionalShot();
                        break;

                    case Type2State.PHASE_1:
                        ChangeType2Direction();
                        break;

                    case Type2State.PHASE_2:
                        ChangeType2Direction();
                        break;

                    case Type2State.FINISH_SHOT:
                        CancelInvoke();
                        CalculatePhases();
                        number_of_directional_shots = 0;
                        type2_repeat_number = 0;
                        last_attack_phase = Phases.TYPE_2;
                        break;
                    default:
                        type2_state = Type2State.NONE;
                        break;
                }
                break;

            case Phases.TYPE_3:
                switch(type3_state)
                {
                    case Type3State.PREPARE_SHOT:
                        Prepare_Kamehameha.SetActive(true);
                        type3_state = Type3State.SHOT;
                        number_max_of_shots = Random.Range(10, 19);
                        break;

                    case Type3State.SHOT:
                        if (timer_kamehameha >= 1.25f)
                        {
                            Prepare_Kamehameha.SetActive(false);
                            Instantiate(Kamehameha, new Vector3(transform.position.x - 5.5f, transform.position.y, Kamehameha.transform.position.z), Kamehameha.transform.rotation, Boss);
                            Kamehameha.transform.localScale = new Vector3(2.5f, 2.5f, 2.5f);
                            timer_kamehameha = 0;
                            type3_state = Type3State.FINISH_SHOT;                          
                        }
                        else
                            timer_kamehameha += Time.deltaTime;
                        break;

                    case Type3State.FINISH_SHOT:
                        CalculatePhases();
                        last_attack_phase = Phases.TYPE_3;
                        break;
                    default:
                        type3_state = Type3State.NONE;
                        break;
                }
                break;          

            case Phases.FAKE_STATIC:
                CheckEye();
                break;

            case Phases.STATIC:
                CheckEye();
                break;

            case Phases.GETHIT:
                StartCoroutine("GetHit");
                Eye_collider.enabled = false;
                break;
        }     
    }

    void MoveLeft()
    {
        gameObject.transform.position = new Vector2(transform.position.x - velocity * Time.deltaTime, transform.position.y);
    }

    void InvokeFollowShot()
    {
        Instantiate(Shot, Boss);
        number_of_shots++;
        Invoke("InvokeFollowShot", 0.75f);
    }
    void InvokeDirectionalShot()
    {
        number_of_directional_shots++;
        if(number_of_directional_shots < 10)
        {
            Instantiate(Shot, Boss);
            Invoke("InvokeDirectionalShot", 0.25f);
        }
    }

    void CalculatePhases()
    {
        if((type1_state == Type1State.FINISH_SHOT || type2_state == Type2State.FINISH_SHOT) && phases != Phases.STATIC && phases != Phases.FAKE_STATIC)
        {
            if(last_fake_phase == Phases.FAKE_STATIC)
            {
                can_kamehameha = true;
                last_fake_phase = Phases.NONE;
                phases = Phases.STATIC;
                timer_next_phase = 0;
            }
            else
            {
                phases = Phases.FAKE_STATIC;
                timer_fake_static = 0;              
            }
        }

        if (type3_state == Type3State.FINISH_SHOT && phases != Phases.STATIC && phases != Phases.FAKE_STATIC)
        {
            phases = Phases.STATIC;
            timer_next_phase = 0;
        }

        if (phases == Phases.FAKE_STATIC)
        {
            FakeStaticPhase();
        }
        if(phases == Phases.STATIC)
        {
            StaticPhase();
        }       
    }

    void CheckEye()
    {
        Eye_collider.enabled = true;
        if (eye.IsDead())
        {
            Destroy(gameObject, 5f);
        }
        else if (eye.get_hit)
        {
            phases = Phases.GETHIT;
            eye.get_hit = false;
        }
        else
        {
            CalculatePhases();
        }
    }

    void FakeStaticPhase()
    {
        last_fake_phase = Phases.FAKE_STATIC;

        if (timer_fake_static > 5)
        {
            rand_type = Random.Range(1, 101);
            number_max_of_shots = Random.Range(10, 19);
            type2_max_repeat_number = Random.Range(2, 5);

            if (rand_type <= 50)
            {
                ChangeAttackPhase();
            }
            else
            {
                phases = last_attack_phase;
                ResetType(phases);
            }
        }
        else
        {
            timer_fake_static += Time.deltaTime;
        }
    }

    void StaticPhase()
    {

        if (timer_next_phase >= 12)
        {
            rand_type = Random.Range(1, 101);

            if (rand_type <= 50)
            {
                phases = Phases.TYPE_1;
                ResetType(phases);
            }
            else
            {
                phases = Phases.TYPE_2;
                ResetType(phases);
            }
        }
        else
        {
            if(timer_next_phase >= 4 && can_kamehameha)
            {
                can_kamehameha = false;

                phases = Phases.TYPE_3;
                ResetType(phases);
            }
            timer_next_phase += Time.deltaTime;
        }
    }

    void ChangeType2Direction()
    {
        if (number_of_directional_shots >= 10 && type2_repeat_number < type2_max_repeat_number)
        {
            timer_change_type2_phase += Time.deltaTime;
            if (timer_change_type2_phase >= 0.5f)
            {
                if (type2_state == Type2State.PHASE_1)
                {
                    type2_state = Type2State.PHASE_2;
                }
                else if (type2_state == Type2State.PHASE_2)
                {
                    type2_state = Type2State.PHASE_1;
                }
                
                type2_repeat_number++;

                if (type2_repeat_number < type2_max_repeat_number)
                    number_of_directional_shots = 0;

                InvokeDirectionalShot();
            }
        }      
        else if(type2_repeat_number >= type2_max_repeat_number)
        {
            type2_state = Type2State.FINISH_SHOT;
        }
    }

    void ChangeAttackPhase()
    {
        if (last_attack_phase == Phases.TYPE_1)
        {
            phases = Phases.TYPE_2;
        }
        else if (last_attack_phase == Phases.TYPE_2)
        {
            phases = Phases.TYPE_1;
        }

        ResetType(phases);
        last_attack_phase = Phases.NONE;
    }

    void ResetType(Phases p)
    {
        SetTypesNONE();

        if (p == Phases.TYPE_1)
        {
            type1_state = Type1State.PREPARE_SHOT;
        }
        if (p == Phases.TYPE_2)
        {
            type2_state = Type2State.PREPARE_SHOT;
        }
        if (p == Phases.TYPE_3)
        {
            type3_state = Type3State.PREPARE_SHOT;
        }
    }

    void SetTypesNONE()
    {
        type1_state = Type1State.NONE;
        type2_state = Type2State.NONE;
        type3_state = Type3State.NONE;
    }

    IEnumerator GetHit()
    {
        for(int i=0; i < 6; ++i)
        {
            Body.SetActive(false);
            yield return new WaitForSeconds(0.2f);
            Body.SetActive(true);
            yield return new WaitForSeconds(0.2f);
        }     
       
        if (last_fake_phase == Phases.FAKE_STATIC)
        {
            can_kamehameha = true;
            last_fake_phase = Phases.NONE;
            phases = Phases.STATIC;
            timer_next_phase = 0;
        }
        else
        {
            phases = Phases.FAKE_STATIC;
            timer_fake_static = 0;
        }

        StopCoroutine("GetHit");
    }
}
