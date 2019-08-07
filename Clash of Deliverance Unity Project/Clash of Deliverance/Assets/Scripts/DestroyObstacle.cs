using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObstacle : MonoBehaviour
{
    public GameObject Obstacle;
    void Update()
    {
        if(Obstacle.transform.position.x <= -40)
        {
            Destroy(gameObject);

            Debug.Log("fuck");
        }
    }
}
