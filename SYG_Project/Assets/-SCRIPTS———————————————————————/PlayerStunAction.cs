using DG.Tweening;
using Rewired;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations;

public class PlayerStunAction : MonoBehaviour
{
    [SerializeField]
    Transform cameraTransform;

    private float _initialY;
    private Vector3 _initialRota;
    private bool _canStun;

    public List<AudioClip> stunList;
    [SerializeField] AudioSource audioStun;

   

    private void Start()
    {
        _canStun = true;
        if (animator == null) animator = GetComponentInChildren<Animator>().gameObject;

        animator.SetActive(false);
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

    void EnableMimolle(bool wanted)
    {
        Scr_Mimolle mimolle = ServicesLocator.Get<GrandpaService>().grandpas[0].GetComponent<Scr_Mimolle>();

        mimolle.canMove = wanted;
        mimolle.agent.enabled = wanted;
        mimolle.graphics.SetActive(wanted);
        mimolle.colliders.SetActive(wanted);
    }

    GameObject animator;

    IEnumerator StunRoutine()
    {
        SetMove(false);

        EnableMimolle(false);
        animator.SetActive(true);

       
        yield return new WaitForSeconds(0.5f);
        cameraTransform.DOMoveY(_initialY + 0.2f, 0.3f).SetEase(Ease.InExpo);
        yield return new WaitForSeconds(0.3f);
        EnableMimolle(true);
        animator.SetActive(false);
        cameraTransform.DOMoveY(_initialY - 1.5f, 0.8f).SetEase(Ease.OutBounce);
        cameraTransform.DOLocalRotate(Vector3.right * -90, 0.8f).SetEase(Ease.InOutCubic);
        yield return new WaitForSeconds(2f);
        cameraTransform.DOMoveY(_initialY, 2f).SetEase(Ease.InCubic);
        cameraTransform.DOLocalRotate(Vector3.zero, 1.4f).SetEase(Ease.InOutCubic);
        yield return new WaitForSeconds(2f);


        
        SetMove(true);
        _canStun = true;
        
    }


    void SetMove(bool wanted)
    {
        transform.GetComponent<PlayerMove>().CanRun = wanted;
        transform.GetComponent<PlayerLook>().CanLook = wanted;
    }
}
