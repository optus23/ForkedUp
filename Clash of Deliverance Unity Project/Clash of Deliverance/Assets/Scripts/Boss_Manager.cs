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
        SHOT,
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

    public GameObject Shot;
    private int number_of_shots;

    public GameObject Kamehameha;
    public GameObject Prepare_Kamehameha;
    private float timer_kamehameha;

    public Transform Goal;
    public Transform Boss;

    //private Boss_Projectile projectile;
    //public GameObject projectile_obj;

    private float timer_next_phase;
    private float time_limit_next_phase;
    private bool is_changing_phase;


    // Start is called before the first frame update
    void Start()
    {
        phases = Phases.TYPE_2;

        type1_state = Type1State.NONE;
        //type2_state = Type2State.NONE;
        type3_state = Type3State.NONE;

    }

    // Update is called once per frame
    void Update()
    {
        switch(phases)
        {
            case Phases.TYPE_1:
                switch(type1_state)
                {
                    case Type1State.PREPARE_SHOT:
                        InvokeFollowShot();
                        type1_state = Type1State.SHOT;
                        break;

                    case Type1State.SHOT:
                        if(number_of_shots >= 5)
                            type1_state = Type1State.FINISH_SHOT;
                        break;

                    case Type1State.FINISH_SHOT:
                        CancelInvoke();
                        CalculatePhases();
                        number_of_shots = 0;
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
                        InvokeDirectionalShot();
                        type2_state = Type2State.SHOT;
                        break;

                    case Type2State.SHOT:
                        //projectile = projectile_obj.GetComponentInChildren<Boss_Projectile>();
                        //projectile.StartCoroutine("DirectionalProjectiles");
                        break;

                    case Type2State.FINISH_SHOT:
                        break;
                }
                break;

            case Phases.TYPE_3:
                switch(type3_state)
                {
                    case Type3State.PREPARE_SHOT:
                        Prepare_Kamehameha.SetActive(true);
                        type3_state = Type3State.SHOT;
                        break;

                    case Type3State.SHOT:
                        if (timer_kamehameha >= 3)
                        {
                            Prepare_Kamehameha.SetActive(false);
                            Instantiate(Kamehameha, new Vector3(transform.position.x - 6.3f, transform.position.y, Kamehameha.transform.position.z), Kamehameha.transform.rotation, Boss);
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
                }


                break;

            case Phases.STATIC:
                CalculatePhases();

                //  TO DO: Open Eye

                break;

            case Phases.GETHIT:

                //  TO DO: Close eye, gethit animation, next phase (use the enemy1 code)

                break;
        }
    }

    void InvokeFollowShot()
    {
        Instantiate(Shot, Boss);
        number_of_shots++;
        Invoke("InvokeShot", 1f);
    }
    void InvokeDirectionalShot()
    {
        Instantiate(Shot, Boss);
        Invoke("InvokeDirectionalShot", 0.1f);
    }

    void CalculatePhases()
    {
        if(type1_state == Type1State.FINISH_SHOT && phases != Phases.STATIC)
        {
            phases = Phases.STATIC;
            time_limit_next_phase = Random.Range(8, 14);
            is_changing_phase = true;
            Debug.Log("End Phase 1: " + time_limit_next_phase);
        }
        if (type2_state == Type2State.FINISH_SHOT && phases != Phases.STATIC)
        {
            phases = Phases.STATIC;
            time_limit_next_phase = Random.Range(8, 14);
            is_changing_phase = true;
            Debug.Log("End Phase 2: " + time_limit_next_phase);
        }
        if (type3_state == Type3State.FINISH_SHOT && phases != Phases.STATIC)
        {
            phases = Phases.STATIC;
            time_limit_next_phase = Random.Range(5, 9);
            is_changing_phase = true;
            Debug.Log("End Phase 3: " + time_limit_next_phase);
        }





        if (timer_next_phase >= time_limit_next_phase && is_changing_phase) //  Changing phase, setting none the other phases
        {
            is_changing_phase = false;
            timer_next_phase = 0;

            if(type1_state != Type1State.NONE)
            {
                type1_state = Type1State.NONE;
                type3_state = Type3State.PREPARE_SHOT;
                phases = Phases.TYPE_3;
            }
            if (type2_state != Type2State.NONE)
            {
                type2_state = Type2State.NONE;
                type3_state = Type3State.PREPARE_SHOT;
                phases = Phases.TYPE_3;
            }
            else if (type3_state != Type3State.NONE)
            {
                type3_state = Type3State.NONE;
                type1_state = Type1State.PREPARE_SHOT;
                phases = Phases.TYPE_1;
            }

        }
        else if(is_changing_phase)
        {
            timer_next_phase += Time.deltaTime;
        }

    }
}
