using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Scr_TabletMove : MonoBehaviour
{
    public Vector3 initTransform;
    public Transform selfTransform; 

    private void Start()
    {
        initTransform = transform.position;
        
        //selfTransform.localPosition = initTransform;
    }
    public void Update()
    {
        if (Input.GetKey(KeyCode.E))
        {
            selfTransform.DOLocalMove(new Vector3(transform.position.x, 0, transform.position.z), 0.5f, false);
        }

        if (Input.GetKeyUp(KeyCode.E))
        {
            selfTransform.DOLocalMove(new Vector3(0, transform.position.y - 1000, 0), 0.5f, false);
        }
    }
}
