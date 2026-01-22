using DG.Tweening;
using Rewired;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStunAction : MonoBehaviour
{
    [SerializeField]
    Transform cameraTransform;

    private float _initialY;
    private Vector3 _initialRota;
    private bool _canStun;

    public List<AudioClip> stunList;
    [SerializeField] AudioSource audioStun;

    [HideInInspector]
    private Rigidbody rigid;

    private void Start()
    {
        _canStun = true;
        rigid = GetComponent<Rigidbody>();
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
        if (!_canStun) return;
        if (cameraTransform == null)
        {
            Debug.Log("Pas de camera");
            return;
        }
        _canStun = false;
        _initialY = cameraTransform.position.y;
        _initialRota = cameraTransform.eulerAngles;
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
        Vector3 dirToMimolle = (ServicesLocator.Get<GrandpaService>().grandpas[0].position - transform.position).normalized;
        Debug.DrawRay(transform.position, dirToMimolle * Vector3.Distance(ServicesLocator.Get<GrandpaService>().grandpas[0].position, transform.position), Color.red, 5f);
        SetMove(false);

        while(transform.eulerAngles != dirToMimolle)

        cameraTransform.DORotate(dirToMimolle, 0.2f).SetEase(Ease.InOutCubic);
        yield return new WaitForSeconds(0.6f);
        cameraTransform.DOMoveY(_initialY + 0.2f, 0.4f).SetEase(Ease.InExpo);
        rigid.AddForce(-dirToMimolle * 3);
        yield return new WaitForSeconds(0.4f);
        cameraTransform.DOMoveY(_initialY - 1.5f, 0.5f).SetEase(Ease.OutBounce);
        yield return new WaitForSeconds(2f);
        cameraTransform.DOMoveY(_initialY, 2f).SetEase(Ease.InCubic);
        cameraTransform.DORotate(_initialRota, 2f).SetEase(Ease.InOutCubic);
        yield return new WaitForSeconds(2f);


        SetMove(true);
        _canStun = true;
        
    }


    void SetMove(bool wanted)
    {
        transform.GetComponent<PlayerMove>().CanRun = wanted;
        transform.GetComponent<PlayerLook>().CanLook= wanted;
    }
}
