using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public GameObject[] obj;
    //public GameObject[] Enemy1;
    //public GameObject[] Enemy2;
    public float time;


    // Start is called before the first frame update
    void Start()
    {
        Generator();
    }

    // Update is called once per frame
    void Update()
    {
        //Difficulty curve
        time -= Mathf.Log(time + 0.6f) / 3000;

        //Max time per obstacle (35 score)
        if (time < 1.65f)
            time = 1.65f;
    }

    void Generator()
    {
        if (!Player_Jump.dead)
        {
            Instantiate(obj[0], new Vector3(transform.position.x, Random.Range(-2.2f, 2.6f), 0), Quaternion.identity);
            Invoke("Generator", time); 
        }
         
    }

    void Enemy1Generator()
    {

    }
    void Enemy2Generator()
    {

    }
    
}
