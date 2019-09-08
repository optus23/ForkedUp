using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Manager : MonoBehaviour
{
    enum State
    {
        PREPARE_SHOT,
        SHOT,
        STATIC,
        NONE
    }

    public GameObject Shot;
    private int number_of_shots;
    private State state;

    public Transform Goal;
    public Transform Boss;

    // Start is called before the first frame update
    void Start()
    {
        state = State.PREPARE_SHOT;
    }

    // Update is called once per frame
    void Update()
    {

        if (state == State.PREPARE_SHOT)
        {
            InvokeShot();
            state = State.SHOT;
        }

        if (state == State.SHOT)
        {
            if (number_of_shots >= 50)
            {
                state = State.STATIC;
            }
        }

        if (state == State.STATIC)
        {
            CancelInvoke();
        }
    }

    void InvokeShot()
    {
        Instantiate(Shot, Boss);
        number_of_shots++;
        Invoke("InvokeShot", 1f);
    }
}
