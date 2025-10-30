using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_PlayerStun : MonoBehaviour
{
    [SerializeField]
    Transform cameraTransform;

    private float initialY;
    private bool canStun;

    public List<AudioClip> stunList;
    [SerializeField] AudioSource audioStun;

    private void Start()
    {
        canStun = true;
    }

    private void Update()
    {
        while (cameraTransform.eulerAngles.z > 360)
        {
            cameraTransform.eulerAngles -= new Vector3(0,0,360);
        }

        while (cameraTransform.eulerAngles.z < -360)
        {
            cameraTransform.eulerAngles += new Vector3(0, 0, 360);
        }
    }

    public void Stun()
    {
        if (!canStun) return;
        if (cameraTransform == null)
        {
            Debug.Log("Pas de camera");
            return;
        }
        canStun = false;
        initialY = cameraTransform.position.y;
        Debug.Log("Player Stun");

        if(audioStun != null) 
            {
            int r = Random.Range(0, stunList.Count);
            audioStun.clip = stunList[r];
            audioStun.Play();
            }

        StartCoroutine(StunRoutine());
    }

    IEnumerator StunRoutine()
    {
        transform.GetComponent<PlayerMove>().CanRun = false;
        transform.GetComponent<PlayerLook>().CanLook = false;
        cameraTransform.DOMoveY(initialY + 2f, 1f).SetEase(Ease.InExpo);
        cameraTransform.DOLocalRotate(new Vector3(0, 0, 90), 2f).SetEase(Ease.OutExpo);
        yield return new WaitForSeconds(1f);
        
        cameraTransform.DOMoveY(initialY - 1.5f, 1.5f).SetEase(Ease.OutBounce);
        
        yield return new WaitForSeconds(5f);

        cameraTransform.DOMoveY(initialY, 3f).SetEase(Ease.InCubic);
        cameraTransform.DOLocalRotate(new Vector3(0, 0, 0), 2f).SetEase(Ease.InOutCubic);
        yield return new WaitForSeconds(3f);
        transform.GetComponent<PlayerMove>().CanRun = true;
        transform.GetComponent<PlayerLook>().CanLook = true;
        canStun = true;
        
    }
}
