using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyMoney : MonoBehaviour
{
    public GameObject Money_1;
    public GameObject Money_5;
    public GameObject Money_10;

    private void Start()
    {
        if(Random.Range(1, 4) != 1)
        {
            Money_1.SetActive(true);
        }
        else if(Random.Range(1, 8) != 1)
        {
            Money_5.SetActive(true);
        }
        else if(Random.Range(1, 15) != 1)
        {
            Money_10.SetActive(true);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.tag == "Player")
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            Destroy(gameObject);
        }
    }
}
