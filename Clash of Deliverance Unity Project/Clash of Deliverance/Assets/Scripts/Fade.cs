using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fade : MonoBehaviour
{
    SpriteRenderer sprite;
    Color alpha;
    Enemy_1 enemy_1;
    public GameObject Body;

    public bool start;
    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        alpha = sprite.GetComponent<SpriteRenderer>().color;
        enemy_1 = Body.GetComponentInParent<Enemy_1>();
    }

    private void Update()
    {

        if (enemy_1.start_fading)
        {
            StartCoroutine("Fading");
            enemy_1.start_fading = false;
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
