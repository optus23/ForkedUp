using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetHit : MonoBehaviour
{
    Enemy4 enemy_4;
    public GameObject enemy_4_obj;

    // Start is called before the first frame update
    void Start()
    {
        enemy_4 = GetComponentInParent<Enemy4>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (enemy_4.get_hit)
        {
            StartCoroutine("Get_Hit");
        }
    }

    IEnumerator Get_Hit()
    {
        for (int i = 0; i < 6; ++i)
        {
            enemy_4_obj.SetActive(false);
            yield return new WaitForSeconds(0.2f);
            enemy_4_obj.SetActive(true);
            yield return new WaitForSeconds(0.2f);
        }
        StopCoroutine("GetHit");
        enemy_4.get_hit = false;
    }

}
