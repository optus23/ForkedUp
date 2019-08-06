using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxMovement : MonoBehaviour
{

    public GameObject Background1;
    public GameObject Background2;
    public GameObject Background3;

    public float speed1;
    public float speed2;
    public float speed3;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if(!Player_Jump.dead)
        {
            Background1.transform.position = new Vector2(Background1.transform.position.x - speed1, Background1.transform.position.y);
            Background2.transform.position = new Vector2(Background2.transform.position.x - speed2, Background2.transform.position.y);
            Background3.transform.position = new Vector2(Background3.transform.position.x - speed3, Background3.transform.position.y);

        }
    }
}
