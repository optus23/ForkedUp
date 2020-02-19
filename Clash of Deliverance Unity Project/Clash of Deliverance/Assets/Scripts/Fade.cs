using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fade : MonoBehaviour
{
    SpriteRenderer sprite;
    Color alpha;
    Enemy_1 enemy_1;
    Enemy_2 enemy_2;
    Enemy_3 enemy_3;
    Enemy4 enemy_4;
    
    public GameObject Body;

    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        alpha = sprite.GetComponent<SpriteRenderer>().color;
        enemy_1 = Body.GetComponentInParent<Enemy_1>();
        enemy_2 = GetComponentInParent<Enemy_2>();
        enemy_3 = GetComponentInParent<Enemy_3>();
        enemy_4 = GetComponentInParent<Enemy4>();

    }

    private void Update()
    {

        if(enemy_1 != null)
        {
            if (enemy_1.start_fading)
            {
                StartCoroutine("Fading");
                enemy_1.start_fading = false;
            }
        }

        if (enemy_2 != null)
        {
            if (enemy_2.start_fading)
            {
                StartCoroutine("Fading");
                
                enemy_2.start_fading = false;
            }
        }

        if (enemy_3 != null)
        {
            if (enemy_3.start_fading)
            {
                StartCoroutine("Fading");
                
                enemy_3.start_fading = false;
            }
        }

        if (enemy_4 != null)
        {
            if (enemy_4.start_fading)
            {
                StartCoroutine("Fading");
                enemy_4.start_fading = false;
            }
        }

    }



    IEnumerator Fading()
    {
        for (float f = 0.5f; f > 0; f -= 0.1f)
        {
            alpha.a = f;
            sprite.GetComponent<SpriteRenderer>().color = alpha;
            yield return new WaitForSeconds(.1f);
        }
    }
}
