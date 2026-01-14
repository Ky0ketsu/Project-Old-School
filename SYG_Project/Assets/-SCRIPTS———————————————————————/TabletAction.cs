using System.Collections;
using System.Collections.Generic;
using System.Threading;
using DG.Tweening;
using Rewired;
using UnityEngine;

public class TabletAction : MonoBehaviour
{
    public Vector3 initTransform;
    public Transform selfTransform; 

    

    public List<AudioClip> cameraActionOpenList;
    public List<AudioClip> cameraActionCloseList;
    [SerializeField] AudioSource audioCameraAction;

    Player player;

    private void Start()
    {
        initTransform = transform.position;
        ServicesLocator.Get<IPlayerActionService>().SetTabletAction(this);
        player = ReInput.players.GetPlayer(0);
    }


    public void EnterTabletView()
    {
        ServicesLocator.Get<IAudioService>().PlayAudioOneShot(cameraActionOpenList[Random.Range(0, cameraActionOpenList.Count)]);
        selfTransform.DOLocalMove(new Vector3(transform.position.x, 0, transform.position.z), 0.5f, false);
    }

    public void ExitTabletView()
    {
        selfTransform.DOLocalMove(new Vector3(0, transform.position.y - 1000, 0), 0.5f, false);

        int r = Random.Range(0, cameraActionCloseList.Count);
        audioCameraAction.clip = cameraActionCloseList[r];
        audioCameraAction.Play();
    }


}
