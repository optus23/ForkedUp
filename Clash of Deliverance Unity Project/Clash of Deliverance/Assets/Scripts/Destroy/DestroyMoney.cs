using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyMoney : MonoBehaviour
{
    private void Start()
    {
        if(Random.Range(1, 4) != 1)
            gameObject.SetActive(false);
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
