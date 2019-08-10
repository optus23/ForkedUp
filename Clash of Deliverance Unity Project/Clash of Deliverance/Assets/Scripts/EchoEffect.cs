using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EchoEffect : MonoBehaviour
{
    public float timeBetweenSpawns;
    public float startTimeBetweenSpawns;

    public GameObject echo;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(timeBetweenSpawns <= 0)
        {
            //Instantiate(echo, transform.position, Quaternion.identity);
            Instantiate(echo, transform.position, Quaternion.identity);
            timeBetweenSpawns = startTimeBetweenSpawns;
        }
        else
        {
            timeBetweenSpawns -= Time.deltaTime;
        }
    }
}
