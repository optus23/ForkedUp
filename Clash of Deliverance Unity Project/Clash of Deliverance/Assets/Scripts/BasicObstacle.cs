using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicObstacle : MonoBehaviour
{
    public float velocity;

    private SpriteRenderer sprite;
    private int sorting_layer_order = 5;

    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!Player_Jump.dead)
        {
            gameObject.transform.position = new Vector2(gameObject.transform.position.x - velocity * Time.deltaTime * 60, gameObject.transform.position.y);
        }

        sprite.sortingOrder = sorting_layer_order;

       
    }

}
