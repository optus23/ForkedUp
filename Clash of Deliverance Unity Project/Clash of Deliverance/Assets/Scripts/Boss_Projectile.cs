using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Projectile : MonoBehaviour
{
   
    [SerializeField]
    private float velocity;
  
    private Boss_Manager Boss;
    private Vector2 distance;

    float directional_angle;

    void Start()
    {      
        Boss = gameObject.GetComponentInParent<Boss_Manager>();

        distance = Boss.Goal.position - transform.position;

        Destroy(gameObject, 3);
    }

    void Update()
    {

        if (Boss.type1_state != Boss_Manager.Type1State.NONE)  //Follow bulets
        {
            //Calculates angle and move fordward
            float angle = Mathf.Atan(distance.y / distance.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle);

            transform.Translate(new Vector3(-velocity * Time.deltaTime, 0, 0));
        }

        if (Boss.type2_state != Boss_Manager.Type2State.NONE) //Directional bulets
        {
            //  TO DO: directional projectiles

            StartCoroutine("DirectionalProjectiles");
            transform.rotation = Quaternion.Euler(0, 0, directional_angle);
            transform.Translate(new Vector3(-velocity * Time.deltaTime, 0, 0));
        }

      
    }

    IEnumerator DirectionalProjectiles()
    {
        yield return new WaitForSeconds(0.1f);
        directional_angle++;
        
        if(directional_angle >= 180)
        {
            StopCoroutine("DirectionalProjectiles");
        }
    }

}
