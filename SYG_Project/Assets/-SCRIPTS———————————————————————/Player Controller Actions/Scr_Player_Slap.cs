using Rewired;
using System.Collections;
using UnityEngine;
using DG.Tweening;

public class Scr_Player_Slap : MonoBehaviour
{
    [SerializeField] int playerID = 0;
    [SerializeField] private bool canSlap;
    Player player;
    [SerializeField] AudioSource audioSlap;
    [SerializeField] Transform slapSprite;
    [HideInInspector] Vector3 initialSlapPosition, initialSlapRotation;

    [SerializeField] private Transform viewDirection;
    public LayerMask layerMask;

    [SerializeField] ParticleSystem slapParticule;

    void Awake()
    {
        initialSlapPosition = slapSprite.position;
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
    }

    void EnableSlap()
    {
        canSlap = true;
    }

    void DisableSlap()
    {
        canSlap = false;
    }


    void Update()
    {
        RaycastHit hit;
        if(Physics.Raycast(transform.position + Vector3.up * 1.5f , viewDirection.forward, out hit , 3f, layerMask))
        {
            Debug.DrawRay (transform.position + Vector3.up * 1.5f , viewDirection.forward * hit.distance, Color.green);
        }
        else
        {
            Debug.DrawRay(transform.position + Vector3.up * 1.5f , viewDirection.forward * 3f, Color.red);
        }


        if (canSlap)
        {
            if (player.GetButton("Slap"))
            {
                canSlap = false;
                if (Physics.Raycast(transform.position + Vector3.up * 1.5f, viewDirection.forward, out hit, 3f, layerMask))
                {
                    ISlapable slapable = hit.transform.GetComponent<ISlapable>();

                    
                    if (slapable != null)
                    {
                        Debug.Log(slapable);
                        slapable.Slap();
                    }
                    else Debug.Log(" je suis null");
                }
                Slap();
            }
        }
    }

    void Slap()
    {
        if(audioSlap != null) audioSlap.Play();
        StartCoroutine(SlapAnimation());
    }

    IEnumerator SlapAnimation()
    {
        slapSprite.DOLocalMove(initialSlapPosition + new Vector3(1, 1.5f, 0), 0.3f).SetEase(Ease.InCubic);
        yield return new WaitForSeconds(0.3f);
        slapSprite.DOLocalMove(initialSlapPosition + new Vector3(-1.5f, 1f, 0), 0.5f).SetEase(Ease.OutExpo);
        slapSprite.DOLocalRotate(initialSlapRotation + new Vector3(0, -40, 0), 0.5f).SetEase(Ease.OutExpo);
        yield return new WaitForSeconds(0.5f);
        slapSprite.DOLocalMove(initialSlapPosition, 0.2f).SetEase(Ease.InCubic);
        slapSprite.DOLocalRotate(initialSlapRotation, 0.5f).SetEase(Ease.OutExpo);
        yield return new WaitForSeconds(0.2f);
        canSlap = true;
    }
}
