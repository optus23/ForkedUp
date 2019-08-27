using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyVelocityEnabled : MonoBehaviour
{
    private float money_velocity = 2.4f;


    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position = new Vector2(transform.position.x - money_velocity * Time.deltaTime, transform.position.y);

    }
}
