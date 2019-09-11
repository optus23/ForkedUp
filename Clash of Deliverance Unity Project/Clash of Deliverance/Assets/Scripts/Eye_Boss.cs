using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eye_Boss : MonoBehaviour
{

    [SerializeField]
    private int life;
    public bool get_hit;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.tag == "Player")
        {
            life--;
            get_hit = true;
        }
    }

    public bool IsDead()
    {
        if(life <= 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
