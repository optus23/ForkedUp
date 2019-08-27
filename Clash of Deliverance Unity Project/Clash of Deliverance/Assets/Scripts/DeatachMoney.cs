using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeatachMoney : MonoBehaviour
{
    MoneyVelocityEnabled money;

    private void Start()
    {
        money = gameObject.GetComponentInChildren<MoneyVelocityEnabled>();
    }
    private void OnDestroy()
    {
        money.GetComponent<MoneyVelocityEnabled>().enabled = true;
        transform.DetachChildren();
    }
}
