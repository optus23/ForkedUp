using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Manager : MonoBehaviour
{
   
    public enum Phases
    {
        TYPE_1,
        TYPE_2,
        TYPE_3,
        STATIC,
        FAKE_STATIC,
        DEAD,
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
    public enum StaticState
    {
        FIRST_EXIT_BATTLEFIELD,
        ENTER_BATTLEFIELD,
        EXIT_BATTLEFIELD,
        NONE
    }

    public StaticState static_state;
    public Type1State type1_state;
    public Type2State type2_state;
    public Type3State type3_state;
    public Phases phases;
    public Phases last_attack_phase;
    public Phases last_fake_phase;

    [SerializeField]
    private float offset_enter_battelfield;
    [SerializeField]
    private float velocity;
    private float timer_enter_battelfield;
    [SerializeField]
    private bool first_exit_battelfield;
    private bool exit_battelfield;

    GameObject levelGenerator;

    public Eye_Boss eye;
    public GameObject Body;
    public GameObject Shot;
    public GameObject LittleEyes;
    public GameObject WhiteEye;
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
    private bool first_kamehameha;
    private bool phase1_repeated;
    private bool phase2_repeated;

    private bool get_hit;
    public bool invulnerable = false;
    LevelGenerator lvlGenScript;

    // Start is called before the first frame update
    void Start()
    {
        eye = GetComponentInChildren<Eye_Boss>();
        Eye_collider = GetComponentInChildren<CircleCollider2D>();
        Eye_collider.enabled = false;

        levelGenerator = GameObject.FindGameObjectWithTag("LevelGenerator");
        lvlGenScript = levelGenerator.GetComponent<LevelGenerator>();

        SetTypesNONE();
        phases = Phases.STATIC;
        static_state = StaticState.ENTER_BATTLEFIELD;
        first_kamehameha = true;
    }

    // Update is called once per frame
    void Update()
    {
       
        switch (phases)
        {
            case Phases.TYPE_1:  //  Following Shots
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

            case Phases.TYPE_2:  //  Directional Shots
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
                            Instantiate(Kamehameha, new Vector3(transform.position.x - 8.5f, transform.position.y, Kamehameha.transform.position.z), Kamehameha.transform.rotation, Boss);
                            Kamehameha.transform.localScale = new Vector3(2.5f, 2.5f, 2.5f);
                            timer_kamehameha = 0;
                            type3_state = Type3State.FINISH_SHOT;                          
                        }
                        else
                            timer_kamehameha += Time.deltaTime;
                        break;

                    case Type3State.FINISH_SHOT:
                        CalculatePhases();
                        break;
                    default:
                        type3_state = Type3State.NONE;
                        break;
                }
                break;          

            case Phases.FAKE_STATIC:
                CheckEye();
                CalculatePhases();
                BattlefieldMovement();
                break;

            case Phases.STATIC:
                CheckEye();
                CalculatePhases();
                BattlefieldMovement();        
                break;
            case Phases.NONE:
                phases = Phases.STATIC;
                break;
        }     

        if(get_hit)
        {
            StartCoroutine("GetHit");
            Eye_collider.enabled = false;
            invulnerable = true;
            eye.getHitAnimation.Play("GetHit", 0, 0);
            get_hit = false;
        }
    }

    void MoveLeft()
    {
        gameObject.transform.position = new Vector2(transform.position.x - velocity * Time.deltaTime, transform.position.y);
    }
    void MoveRight()
    {
        gameObject.transform.position = new Vector2(transform.position.x + velocity/2 * Time.deltaTime, transform.position.y);
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
                static_state = StaticState.ENTER_BATTLEFIELD;
                timer_next_phase = 0;
            }
            else
            {
                phases = Phases.FAKE_STATIC;
                static_state = StaticState.ENTER_BATTLEFIELD;
                timer_fake_static = 0;              
            }
        }

        if (type3_state == Type3State.FINISH_SHOT && phases != Phases.STATIC && phases != Phases.FAKE_STATIC)
        {
            if (first_exit_battelfield)
            {
                first_kamehameha = false;
            }
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
        if(!invulnerable)
            Eye_collider.enabled = true;

        if (eye.IsDead())
        {
            get_hit = true;
            Destroy(gameObject, 2f);
            
            phases = Phases.DEAD;
            if(!lvlGenScript.boss_enemy_defeat)
            {
                lvlGenScript.boss_enemy_defeat = true;
                lvlGenScript.InvokingGenerator(1.5f);
            }
            

            //TODO: Particles expansion circles (cash)
        }
        else if (eye.get_hit)
        {
            get_hit = true;
            eye.state = Eye_Boss.EyeState.GETHIT;
            eye.get_hit = false;
        }
        else
        {

            //CalculatePhases();
        }
    }

    void FakeStaticPhase()
    {
        last_fake_phase = Phases.FAKE_STATIC;

        if (timer_fake_static > 2)
        {
            static_state = StaticState.EXIT_BATTLEFIELD;
            rand_type = Random.Range(1, 101);
            number_max_of_shots = Random.Range(10, 19);
            type2_max_repeat_number = Random.Range(2, 5);

            if (exit_battelfield)
            {
                if(!CheckRepeatedPhases())              
                {
                    if (rand_type <= 50)
                    {
                        ChangeAttackPhase();
                    }
                    else
                    {
                        phases = last_attack_phase;
                        ResetType(phases);

                        if (phases == Phases.TYPE_1)
                            phase1_repeated = true;
                        else if (phases == Phases.TYPE_2)
                            phase2_repeated = true;
                    }
                }
                
            }
           
        }
        else
        {
            timer_fake_static += Time.deltaTime;
        }
    }

    void StaticPhase()
    {

        if (timer_next_phase >= 3.5f)
        {
            rand_type = Random.Range(1, 101);
            static_state = StaticState.EXIT_BATTLEFIELD;

            if(exit_battelfield)
            {
                if (!CheckRepeatedPhases())
                {
                    if (rand_type <= 50)
                    {
                        ChangeAttackPhase();
                    }
                    else
                    {
                        phases = last_attack_phase;
                        ResetType(phases);

                        if (phases == Phases.TYPE_1)
                            phase1_repeated = true;
                        else if (phases == Phases.TYPE_2)
                            phase2_repeated = true;
                    }
                }                  
            }
            
        }
        else
        {
            if(timer_next_phase >= 3 && can_kamehameha)
            {
                can_kamehameha = false;

                phases = Phases.TYPE_3;
                ResetType(phases);
            }
            timer_next_phase += Time.deltaTime;
        }
    }

    bool CheckRepeatedPhases()
    {
        if (phase1_repeated)
        {
            phases = Phases.TYPE_2;
            ResetType(phases);
            phase1_repeated = false;
            return true;
        }
        else if (phase2_repeated)
        {
            phases = Phases.TYPE_1;
            ResetType(phases);
            phase2_repeated = false;
            return true;
        }
        else
        {
            return false;
        }
    }

    void BattlefieldMovement()
    {
        switch (static_state)
        {
            case StaticState.ENTER_BATTLEFIELD:
                if (gameObject.transform.position.x >= 6.5f)
                {
                    MoveLeft();
                    // if attack growl DO STUFF
                    eye.state = Eye_Boss.EyeState.BLINK_UP; 
                    exit_battelfield = false;
                }
                else
                {
                    if (first_exit_battelfield)
                    {
                       
                        if (timer_enter_battelfield > 2.5f)
                        {
                            static_state = StaticState.FIRST_EXIT_BATTLEFIELD;
                            first_exit_battelfield = false;
                        }
                        else if(timer_enter_battelfield > 0.5f && first_kamehameha)
                        {
                            phases = Phases.TYPE_3;
                            ResetType(phases);
                        }
                        else
                        {
                            timer_enter_battelfield += Time.deltaTime;
                        }
                    }                 
                }
                break;

            case StaticState.EXIT_BATTLEFIELD:
                if (gameObject.transform.position.x <= 7.5f)
                {
                    MoveRight();
                    eye.state = Eye_Boss.EyeState.BLINK;
                }
                else
                {
                    CalculatePhases();
                    exit_battelfield = true;
                }
                break;

            case StaticState.FIRST_EXIT_BATTLEFIELD:
                if (gameObject.transform.position.x <= 7.5f)
                {
                    MoveRight();
                    eye.state = Eye_Boss.EyeState.BLINK;
                }
                else
                {
                    rand_type = Random.Range(1, 3);
                    if (rand_type == 1)
                    {
                        phases = Phases.TYPE_1;
                        type1_state = Type1State.PREPARE_SHOT;
                        timer_enter_battelfield = 0;
                    }
                    else if (rand_type == 2)
                    {
                        phases = Phases.TYPE_2;
                        type2_state = Type2State.PREPARE_SHOT;
                        timer_enter_battelfield = 0;
                    }
                }
                break;
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
        for(int i=0; i < 4; ++i)
        {
            Body.SetActive(false);
            LittleEyes.SetActive(false);
            WhiteEye.SetActive(false);
            yield return new WaitForSeconds(0.2f);
            Body.SetActive(true);
            LittleEyes.SetActive(true);
            WhiteEye.SetActive(true);
            yield return new WaitForSeconds(0.2f);
        }     
       
        //if (last_fake_phase == Phases.FAKE_STATIC)
        //{
        //    can_kamehameha = true;
        //    last_fake_phase = Phases.NONE;
        //    phases = Phases.STATIC;
        //    timer_next_phase = 0;
        //}
        //else
        //{
        //    phases = Phases.FAKE_STATIC;
        //    timer_fake_static = 0;
        //}


        invulnerable = false;

        Eye_collider.enabled = true;
        StopCoroutine("GetHit");
    }
}
