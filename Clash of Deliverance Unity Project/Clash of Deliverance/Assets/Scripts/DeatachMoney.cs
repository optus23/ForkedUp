using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeatachMoney : MonoBehaviour
{
    MoneyVelocityEnabled money;

    private void Start()
    {
        money = gameObject.GetComponentInChildren<MoneyVelocityEnabled>();
        Debug.Log(money);
        //money = GameObject.Find("Money +10").GetComponent<MoneyVelocityEnabled>();
    }
    private void OnDestroy()
    {
        transform.DetachChildren();
        //money.money_deatached = true;
        money.GetComponent<MoneyVelocityEnabled>().enabled = true;


    }
}
