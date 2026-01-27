using Rewired;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SlapAction : MonoBehaviour
{

    

    [HideInInspector]
    int playerID = 0;
    [HideInInspector]
    Player player;


    [SerializeField]
    private bool _canSlap;

    [SerializeField] Transform slapSprite;
    [HideInInspector] Vector3 initialSlapPosition, initialSlapRotation;

    [SerializeField]
    private Transform _viewDirection;

    [SerializeField]
    private LayerMask _layerMask;

    [SerializeField]
    GameObject slapFX;
    public List<AudioClip> slapList;
    public List<AudioClip> slapmissList;


    void Awake()
    {

        initialSlapPosition = slapSprite.localPosition;
        initialSlapRotation = slapSprite.eulerAngles;
        EVENTS.OnGameplay += EnableSlap;
        EVENTS.OnGameplayExit += DisableSlap;
    }

    private void OnDestroy()
    {
        EVENTS.OnGameplay -= EnableSlap;
        EVENTS.OnGameplayExit -= DisableSlap;
    }

    private void Start()
    {
        
        player = ReInput.players.GetPlayer(playerID);
        if (GAME.MANAGER.CurrentState == State.gameplay) EnableSlap();
        ServicesLocator.Get<IPlayerActionService>()?.SetSlapAction(this);
        ServicesLocator.Get<PlayerService>()?.SetPlayer(gameObject);
    }

    void EnableSlap()
    {
        _canSlap = true;
    }

    void DisableSlap()
    {
        _canSlap = false;
    }

    bool isProxi;
    GameObject spriteSpine;

    private void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position + Vector3.up * 1.5f, _viewDirection.forward, out hit, 3f, _layerMask))
        {
            Debug.DrawRay(transform.position + Vector3.up * 1.5f, _viewDirection.forward * hit.distance, Color.green, 5f);

            ISlapable slapable = hit.transform.GetComponentInParent<ISlapable>();
            if (slapable != null)
            {
                if(hit.transform.GetComponentInParent<GrandpaParent>() != null)
                {
                    if (hit.transform.GetComponentInParent<GrandpaParent>().spriteCanSlap == true)
                    {
                        SetSprite(true);
                    }
                    else
                    {
                        SetSprite(false);
                    }

                    
                }
                else
                {
                    SetSprite(false);
                }
            }
            else
            {
                SetSprite(false);
            }
        }
        else
        {
            Debug.DrawRay(transform.position + Vector3.up * 1.5f, _viewDirection.forward * 3f, Color.red, 5f);
            SetSprite(false);
        }

    }

    void SetSprite(bool wanted)
    {
        if(spriteSpine == null)
        {
            spriteSpine = ServicesLocator.Get<TimerService>().sprite;
        }
        spriteSpine.SetActive(wanted);
    }

    public void SlapWanted()
    {
    RaycastHit hit;

    if (!_canSlap) return;

    if (Physics.Raycast(transform.position + Vector3.up * 1.5f, _viewDirection.forward, out hit, 3f, _layerMask))
    {
        Debug.DrawRay(transform.position + Vector3.up * 1.5f, _viewDirection.forward * hit.distance, Color.green, 5f);


        ISlapable slapable = hit.transform.GetComponentInParent<ISlapable>();
        if (slapable != null)
        {
            Debug.Log(slapable);
            slapable.Slap();
            GameObject slapVFX = Instantiate(slapFX, hit.point, Quaternion.identity);
            Destroy(slapVFX, 1f);

        }
        else Debug.Log(" je suis null");

        if (slapList != null)
        {
            ServicesLocator.Get<IAudioService>().PlayAudioOneShot(slapList[Random.Range(0, slapList.Count)]);
        }




        }
        else
        {
            Debug.DrawRay(transform.position + Vector3.up * 1.5f, _viewDirection.forward * 3f, Color.red, 5f);
            Debug.Log("Loupé");
            if (slapmissList.Count > 0)
            {
                ServicesLocator.Get<IAudioService>().PlayAudioOneShot(slapmissList[Random.Range(0, slapmissList.Count)]);

            }
        }

        _canSlap = false;
        StartCoroutine(SlapAnimation());
    }

    IEnumerator SlapAnimation()
    {
        // **********************************
        slapSprite.DOLocalMove(initialSlapPosition + new Vector3(1, 1.5f, 0), 0.3f).SetEase(Ease.InCubic);
        yield return new WaitForSeconds(0.3f);
        slapSprite.DOLocalMove(initialSlapPosition + new Vector3(-1.5f, 1f, 0), 0.5f).SetEase(Ease.OutExpo);
        slapSprite.DOLocalRotate(initialSlapRotation + new Vector3(0, -40, 0), 0.5f).SetEase(Ease.OutExpo);
        yield return new WaitForSeconds(0.5f);
        slapSprite.DOLocalMove(initialSlapPosition, 0.2f).SetEase(Ease.InCubic);
        slapSprite.DOLocalRotate(initialSlapRotation, 0.5f).SetEase(Ease.OutExpo);
        yield return new WaitForSeconds(0.2f);
        _canSlap = true;
    } // *********************************
}
