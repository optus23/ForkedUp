using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomBlink : MonoBehaviour
{ 

    public List<GameObject> Eyes = new List<GameObject>();
    public int random_eye;
    public int last_random_eye;

    private bool blink;
    private bool blink_up;

    public GameObject Blinking_eye;

    Quaternion Eye_Close;
    Quaternion Eye_Normal;

    // Start is called before the first frame update
    void Start()
    {
        Eye_Close = Quaternion.Euler(88, 0, 0);
        Eye_Normal = Quaternion.Euler(0, 0, 0);

        StartCoroutine("ChooseRandomBlink");
    }

    // Update is called once per frame
    void Update()
    {
        
        if(blink)
        {
            Blinking_eye = Eyes[random_eye];
            Blink(Blinking_eye);
            
        }
        else if (blink_up)
        {
            BlinkUp(Blinking_eye);
        }

    }

    void Blink(GameObject MyGameObject)
    {
        MyGameObject.transform.rotation = Quaternion.Lerp(MyGameObject.transform.rotation, Eye_Close, 8 * Time.deltaTime);

        
    }
    void BlinkUp(GameObject MyGameObject)
    {
        MyGameObject.transform.rotation = Quaternion.Lerp(MyGameObject.transform.rotation, Eye_Normal, 8 * Time.deltaTime);
    }

   

    IEnumerator ChooseRandomBlink()
    {
        random_eye = Random.Range(0, Eyes.Count);

        if (random_eye != last_random_eye)
        {
            last_random_eye = random_eye;
            blink = true;
            blink_up = false;
            yield return new WaitForSeconds(0.5f);
        }
       
        StartCoroutine("ChooseRandomBlinkUp");
    }

    IEnumerator ChooseRandomBlinkUp()
    {
        blink = false;
        blink_up = true;
        yield return new WaitForSeconds(0.5f);
        StartCoroutine("ChooseRandomBlink");
    }

}
