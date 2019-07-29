using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxGenerator : MonoBehaviour
{

    public GameObject Background1;
    public GameObject Background2;
    public GameObject Background3;

    public float time1;
    public float time2;
    public float time3;


    // Start is called before the first frame update
    void Start()
    {
        SpawnBackground1();
        SpawnBackground2();
        SpawnBackground3();
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    void SpawnBackground1()
    {
        Invoke("SpawnBackground1", time1);
        Instantiate(Background1, new Vector2(Background1.transform.position.x, Background1.transform.position.y), Quaternion.identity);

    }
    void SpawnBackground2()
    {
        Invoke("SpawnBackground2", time2);
        Instantiate(Background2, new Vector2(Background2.transform.position.x, Background2.transform.position.y), Quaternion.identity);

    }
    void SpawnBackground3()
    {
        Invoke("SpawnBackground3", time3);
        Instantiate(Background3, new Vector2(Background3.transform.position.x, Background3.transform.position.y), Quaternion.identity);

    }
}
