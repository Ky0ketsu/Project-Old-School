using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_GrandpaSpriteDirection : MonoBehaviour
{
    public Transform graphics;
    [SerializeField] float result;
    [SerializeField] Transform Square;
    Animator anim;

    [HideInInspector]
    public bool isAttack;

    

    

    private void Start()
    {
        anim = Square.GetComponent<Animator>();
    }

    void CheckAttack()
    {
        if (isAttack)
        {
            anim.SetBool("Attack", true);
            anim.SetBool("goBack", false);
            anim.SetBool("goLeft", false);
            anim.SetBool("goRight", false);
            anim.SetBool("goFront", false);
        }
        else
        {
            anim.SetBool("Attack", false);
            CheckDirection();
        }
    }

    void CheckDirection()
    {
        float graphicsAngle = graphics.localEulerAngles.y;

        if ((graphicsAngle > 0 && graphicsAngle < 45) || (graphicsAngle < 360 && graphicsAngle > 315))
        {
            //FRONT

            anim.SetBool("goBack", false);
            anim.SetBool("goLeft", false);
            anim.SetBool("goRight", false);
            anim.SetBool("goFront", true);
        }

        if ((graphicsAngle < 315 && graphicsAngle > 225))
        {
            //RIGHT

            anim.SetBool("goBack", false);
            anim.SetBool("goLeft", false);
            anim.SetBool("goFront", false);
            anim.SetBool("goRight", true);
        }

        if ((graphicsAngle < 225 && graphicsAngle > 135))
        {
            //BACK

            anim.SetBool("goFront", false);
            anim.SetBool("goLeft", false);
            anim.SetBool("goRight", false);
            anim.SetBool("goBack", true);
        }

        if ((graphicsAngle < 135 && graphicsAngle > 45))
        {
            //LEFT

            anim.SetBool("goBack", false);
            anim.SetBool("goFront", false);
            anim.SetBool("goRight", false);
            anim.SetBool("goLeft", true);
        }
    }

    public void Update()
    {
        if (GetComponent<Scr_Mimolle>() != null)
        {
            CheckAttack();
        }
        else
        {
            CheckDirection();
        }
    }
}
