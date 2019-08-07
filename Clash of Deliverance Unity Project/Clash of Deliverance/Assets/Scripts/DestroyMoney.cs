using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyMoney : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.tag == "Player")
        {
            Destroy(gameObject);
        }
    }
}
