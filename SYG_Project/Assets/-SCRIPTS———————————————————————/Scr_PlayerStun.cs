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

    private void Start()
    {
        canStun = true;
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
        StartCoroutine(StunRoutine());
    }

    IEnumerator StunRoutine()
    {

        cameraTransform.DOMoveY(initialY + 2f, 1f).SetEase(Ease.InExpo);
        yield return new WaitForSeconds(1f);
        cameraTransform.DOMoveY(initialY - 1.5f, 1.5f).SetEase(Ease.OutExpo);
        yield return new WaitForSeconds(2f);

        cameraTransform.DOMoveY(initialY, 3f).SetEase(Ease.InCubic);
        yield return new WaitForSeconds(3f);
        canStun = true;
    }
}
