using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public Camera MainCam;
    private float shake_amount;

    private void Awake()
    {
        if(MainCam == null)
        {
            MainCam = Camera.main;
        }
    }
   
    public void Shake(float amount, float length)
    {
        shake_amount = amount;
        InvokeRepeating("BeginShake", 0, 0.01f);
        Invoke("StopShake", length);

    }

    private void BeginShake()
    {
        Vector3 camPos = MainCam.transform.position;
        float offset_x = Random.value * shake_amount * 2 - shake_amount;
        float offset_y = Random.value * shake_amount * 2 - shake_amount;
        camPos.x = offset_x;
        camPos.y = offset_y;

        MainCam.transform.position = camPos;
    }

    private void StopShake()
    {
        CancelInvoke("BeginShake");
        MainCam.transform.localPosition = new Vector3(0,0,-10);
    }

    //private void Update()
    //{
    //    if (Input.GetKey("q"))
    //        Shake(0.1f, 0.2f);
    //}
}
