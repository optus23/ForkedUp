using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FPS : MonoBehaviour
{
    public Text fpsText;
    private float deltaTime;

    //inside class
    Vector2 firstPressPos;
    Vector2 secondPressPos;
    Vector2 currentSwipe;

    public void Swipe()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //save began touch 2d point
            firstPressPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        }
        if (Input.GetMouseButtonUp(0))
        {
            //save ended touch 2d point
            secondPressPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

            //create vector from the two points
            currentSwipe = new Vector2(secondPressPos.x - firstPressPos.x, secondPressPos.y - firstPressPos.y);

            //normalize the 2d vector
            //currentSwipe.Normalize();
            //Debug.Log(currentSwipe);


            //swipe upwards
            if (currentSwipe.y > 150 && currentSwipe.x > -10 && currentSwipe.x < 10f)
        {
                Debug.Log("up swipe");
                Debug.Log(currentSwipe);
            }
            //swipe down
            if (currentSwipe.y < -150 && currentSwipe.x > -10 && currentSwipe.x < 10)
        {
                Debug.Log("down swipe");
            }
            //swipe left
            if (currentSwipe.x < -150 && currentSwipe.y > -10 && currentSwipe.y < 10)
        {
                Debug.Log("left swipe");
            }
            //swipe right
            if (currentSwipe.x > 150 && currentSwipe.y > -10 && currentSwipe.y < 10)
        {
                Debug.Log("right swipe");
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
        float fps = 1.0f / deltaTime;
        fpsText.text = Mathf.Ceil(fps).ToString();

        Swipe();
    }


}
