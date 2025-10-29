using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_GrandpaSpriteDirection : MonoBehaviour
{
    public Transform graphics;
    [SerializeField] float result;
    [SerializeField] Transform Square;

    [SerializeField] Sprite front;
    [SerializeField] Sprite right;
    [SerializeField] Sprite back;
    [SerializeField] Sprite left; 
    



    public void LateUpdate()
    {
        float graphicsAngle = graphics.localEulerAngles.y;

        if ((graphicsAngle > 0 &&  graphicsAngle < 45) || (graphicsAngle < 360 && graphicsAngle > 315))
        {
            //FRONT
            Square.GetComponent<SpriteRenderer>().sprite = front; 
        }

        if ((graphicsAngle < 315 && graphicsAngle > 225))
        {
            //RIGHT
            Square.GetComponent<SpriteRenderer>().sprite = right; 
        }

        if ((graphicsAngle < 225 && graphicsAngle > 135))
        {
            //BACK
            Square.GetComponent<SpriteRenderer>().sprite = back; 
        }

        if ((graphicsAngle < 135 && graphicsAngle > 45))
        {
            //LEFT
            Square.GetComponent<SpriteRenderer>().sprite = left;
        }
    }
}
