using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateBossProjectil : MonoBehaviour
{

    private int direction;

    private void Start()
    {
        direction = Random.Range(0, 3);
    }
    void Update()
    {     
        if(direction == 1)
            direction = Random.Range(0, 3);
        else
            transform.Rotate(0, 0, 7 * (direction + -1));
    }
}
