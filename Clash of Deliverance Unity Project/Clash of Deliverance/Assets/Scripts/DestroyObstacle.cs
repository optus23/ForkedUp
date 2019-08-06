using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObstacle : MonoBehaviour
{
    void Start()
    {
        Destroy(gameObject, 6);
    }
}
