using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateBossProjectil : MonoBehaviour
{

    private int direction;

    private void Start()
    {
        direction = Random.Range(0, 2);
    }
    void Update()
    {
        if (direction == 0)
            direction = -1;
        else
            transform.Rotate(0, 0, 7 * direction);
    }
}
