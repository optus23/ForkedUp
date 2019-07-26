using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicObstacle : MonoBehaviour
{
    public float velocity;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position = new Vector2(gameObject.transform.position.x - velocity, gameObject.transform.position.y);

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.transform.tag == "Destroy")
        {
            Destroy(gameObject);
        }

    }
}
