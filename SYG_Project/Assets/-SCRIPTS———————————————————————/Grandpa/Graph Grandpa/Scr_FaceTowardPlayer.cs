using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_FaceTowardPlayer : MonoBehaviour
{
    private Vector3 originalRotation;

    public void Start()
    {
        originalRotation = transform.rotation.eulerAngles; 
    }

    public void LateUpdate()
    {
        transform.LookAt(Camera.main.transform.position, Vector3.up);


        Vector3 rotation = transform.rotation.eulerAngles; 
        rotation.x = 0;
        rotation.z = 0;

        transform.rotation = Quaternion.Euler(rotation);
    }
}
