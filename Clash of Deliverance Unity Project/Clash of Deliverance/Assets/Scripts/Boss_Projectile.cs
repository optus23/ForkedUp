using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Projectile : MonoBehaviour
{
    [SerializeField]
    private float velocity;
  
    private Boss_Manager Boss;
    private Vector2 distance;

    void Start()
    {      
        Boss = gameObject.GetComponentInParent<Boss_Manager>();

        distance = Boss.Goal.position - transform.position;
        Debug.Log("Distance: " + distance);
        Debug.Log("BALL: " + transform.position);
        Debug.Log("PLAYER:" + Boss.Goal.position);
        Destroy(gameObject, 3);
    }

    void Update()
    {
     
        //Calculates angle and move fordward
        float angle = Mathf.Atan(distance.y/distance.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
        
        transform.Translate(new Vector3(-velocity * Time.deltaTime, 0, 0));
    }
}
