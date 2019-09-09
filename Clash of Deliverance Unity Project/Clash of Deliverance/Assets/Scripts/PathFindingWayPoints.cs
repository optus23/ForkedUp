using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFindingWayPoints : MonoBehaviour
{

    [SerializeField]
    private Transform Goal;
    [SerializeField]
    private float speed;

    private void LateUpdate()
    {
        Vector2 vec2;
        vec2 = Vector2.MoveTowards(transform.position, Goal.position, speed * Time.deltaTime);
        this.transform.position = vec2;
    }
}
