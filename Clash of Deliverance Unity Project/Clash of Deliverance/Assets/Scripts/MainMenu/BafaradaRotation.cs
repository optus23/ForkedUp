using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BafaradaRotation : MonoBehaviour
{
    float rotation = 7;
    bool change_direction = true;
    public float speed_rotation;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {


        if(rotation < 11 && change_direction)
        {
            gameObject.transform.eulerAngles = new Vector3(0, 0, rotation += speed_rotation);
        }
        else
        {
            gameObject.transform.eulerAngles = new Vector3(0, 0, rotation -= speed_rotation);
            change_direction = false;
            if (rotation <= -1)
            {
                change_direction = true;
            }
        }

        


    }
}
