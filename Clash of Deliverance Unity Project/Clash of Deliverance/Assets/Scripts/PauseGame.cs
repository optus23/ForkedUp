using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseGame : MonoBehaviour
{
    private float time_scale;
    // Start is called before the first frame update
    void Start()
    {
        time_scale = Time.timeScale;
        Time.timeScale = 0;

    }

    // Update is called once per frame
    void Update()
    {
        //if(Input.GetKey("space"))
        //{
        //    Time.timeScale = time_scale;
        //}
        

    }
}
