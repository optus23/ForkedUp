using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyVelocityEnabled : MonoBehaviour
{
    private float money_velocity = 2.4f;
    public bool money_deatached;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //if (money_deatached)
        //{
        //    money_velocity = 2.4f;
        //}
        //else
        //    money_velocity = 0;



        gameObject.transform.position = new Vector2(transform.position.x - money_velocity * Time.deltaTime, transform.position.y);

    }
}
