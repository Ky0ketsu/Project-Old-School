using DG.Tweening;
using Rewired;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class FireDoor : MonoBehaviour, ISlapable
{
    public bool CAN_BE_CLOSED = true;
    public bool isClosed = false;


    public List<AudioClip> openList;
    public List<AudioClip> closeList;
    [SerializeField] AudioSource audioFireDoor;


    [SerializeField]
    Transform rightDoor;
    [SerializeField]
    private float _maxRightRotaForward;
    [SerializeField]
    private float _maxRightRotaBackward;
    [SerializeField]
    private float _initialRightRota;


    [SerializeField]
    Transform leftDoor;
    [SerializeField]
    private float _maxLeftRotaForward;
    [SerializeField]
    private float _maxLeftRotaBackward;
    [SerializeField]
    private float _initialLeftRota;

    public void Slap()
    {
        ChangeDoorState();
    }

    public void ChangeDoorState()
    {
        if (CAN_BE_CLOSED == true)
        {
            isClosed = !isClosed;

            if (isClosed == true)
            {
                CloseDoor();
            }
            else
            {
                OpenDoor();
            }
        }
        
    }

    [HideInInspector]
    private float initialY;

    private void Start()
    {
        isClosed = true;
        ChangeDoorState();
        initialY = transform.position.y;
    }

    public void CloseDoor()
    {
        Debug.Log("Porte fermer");
        CloseDoorAnimation();

        //-------------------------
        int r = Random.Range(0, closeList.Count);
        audioFireDoor.clip = closeList[r];
        audioFireDoor.Play();
    }

    public void OpenDoor()
    {
        if (!IsForward()) OpenDoorAnimation(_maxRightRotaForward, _maxLeftRotaForward);
        else OpenDoorAnimation(_maxRightRotaBackward, _maxLeftRotaBackward);


        Debug.Log("Porte ouverte");
        
        //------------------------
        int r = Random.Range(0, openList.Count);
        audioFireDoor.clip = openList[r];
        audioFireDoor.Play();
    }

    void CloseDoorAnimation()
    {
        rightDoor.DOKill();
        leftDoor.DOKill();
        rightDoor.DOLocalRotate(Vector3.up * _initialRightRota, 3.5f).SetEase(Ease.OutElastic);
        leftDoor.DOLocalRotate(Vector3.up * _initialLeftRota, 3.5f).SetEase(Ease.OutElastic);
        Debug.Log("Je ferme la porte");
    }

    void OpenDoorAnimation(float Right, float Left)
    {
        rightDoor.DOKill();
        leftDoor.DOKill();
        rightDoor.DOLocalRotate(Vector3.up * Right, 1f).SetEase(Ease.OutBounce);
        leftDoor.DOLocalRotate(Vector3.up * Left, 1f).SetEase(Ease.OutBounce);
        Debug.Log("J'ouvre la porte");
    }

    protected bool IsForward()
    {
        Vector3 dirToPlayer = (ServicesLocator.Get<PlayerService>().player.transform.position - transform.position).normalized;
        if (Vector3.Angle(transform.forward, dirToPlayer) < 180 / 2f)
        {
            return true;
        }
        return false;
    }
}
