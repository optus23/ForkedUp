using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public GameObject[] obj;
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
        if (time < 1.2f)
            time = 1.2f;
    }

    void Generator()
    {
        Instantiate(obj[Random.Range(0, obj.Length)], new Vector3(transform.position.x, Random.Range(-3, 3), 0), Quaternion.identity);
        Invoke("Generator", time);
    }
}
