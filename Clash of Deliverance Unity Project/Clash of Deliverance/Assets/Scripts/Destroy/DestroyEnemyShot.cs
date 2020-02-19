using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyEnemyShot : MonoBehaviour
{
    Enemy_3 enemy_3;
    Color alpha;
    public bool start_fading;
    SpriteRenderer sprite;


    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        alpha = sprite.GetComponent<SpriteRenderer>().color;
    }

    // Update is called once per frame
    void Update()
    {
        if (start_fading)
        {
            StartCoroutine("Fading");
            start_fading = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            Destroy(gameObject);
        }
    }


    IEnumerator Fading()
    {
        for (float f = 0.5f; f > 0; f -= 0.1f)
        {
            alpha.a = f;
            sprite.GetComponent<SpriteRenderer>().color = alpha;
            yield return new WaitForSeconds(.2f);
        }
        Destroy(gameObject);
    }
}
