using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Scr_Mimolle : Scr_Character
{
    [SerializeField] bool shearchPlayer;
    [SerializeField] LayerMask layerMask;

    [SerializeField]
    private float timerNotShowPlayer;

    private float timerAfterkickPlayer;

    public List<AudioClip> attackList;
    [SerializeField] AudioSource audioAttack;


    private void Update()
    {   
        CheckCanViewPlayer();

        if (timerCanAttack > 0) timerCanAttack -= Time.deltaTime;
        if (timerCanAttack <= 0) canAttack = true;


        if(timerAfterkickPlayer > 0) timerAfterkickPlayer -= Time.deltaTime;
        if (timerAfterkickPlayer < 0) timerAfterkickPlayer = 0;
    }

    void CheckCanViewPlayer()
    {


        RaycastHit hit;
        if (Physics.Raycast(transform.position + Vector3.up, player.position - transform.position, out hit, Vector3.Distance(transform.position, player.position) * 1.05f, layerMask))
        {
            if (hit.transform.GetComponent<Scr_Player_Slap>() != null && timerAfterkickPlayer == 0)
            {
                shearchPlayer = false;
                timerNotShowPlayer = 3f;
                Debug.DrawRay(transform.position + Vector3.up, (player.position - transform.position).normalized * Vector3.Distance(transform.position, player.position), Color.green);
            }
            else
            {
                if (timerNotShowPlayer > 0) timerNotShowPlayer -= Time.deltaTime;
                if (timerNotShowPlayer <= 0) timerNotShowPlayer = 0f; shearchPlayer = true;
                Debug.DrawRay(transform.position + Vector3.up, (player.position - transform.position).normalized * Vector3.Distance(transform.position, player.position), Color.red);
            }
        }
    }

    //recherche une position al�atoire autour du joueur
    public override void SetRandomDestination(Vector3 center, float maxRandomDistance)
    {
        if (Vector3.Distance(transform.position, player.position) <= 2f && timerAfterkickPlayer == 0)
        {
            AttackPlayer();
            agent.SetDestination(transform.position);
            return;
        }

        if (shearchPlayer)
        {

            if (agent.isOnNavMesh && Vector3.Distance(new Vector3(targetPosition.x, 0, targetPosition.z), new Vector3(transform.position.x, 0, transform.position.z)) < 2f)
            {
                Vector3 randomPoint = player.position + new Vector3(Random.Range(-5, 5), 0, Random.Range(-5, 5));
                NavMeshHit hit;
                if (NavMesh.SamplePosition(randomPoint, out hit, 10f, NavMesh.AllAreas))
                {
                    targetPosition = hit.position;
                }
            }
        }
        else targetPosition = player.position;
    }

    bool canAttack;
    float timerCanAttack;

    void AttackPlayer()
    {
        if (canAttack)
        {
            canAttack = false;
            StartCoroutine(AttackAnimation());
            Invoke("CheckTouchPlayer", 0.6f);

            int r = Random.Range(0, attackList.Count);
            audioAttack.clip = attackList[r];
            audioAttack.Play();

            timerCanAttack = 2f;
        }
    }

    void CheckTouchPlayer()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position + Vector3.up, player.position - transform.position, out hit, 2.3f, layerMask))
        {

            if (hit.transform.GetComponentInParent<Scr_PlayerStun>() != null)
            {
                hit.transform.GetComponentInParent<Scr_PlayerStun>().Stun();
                timerAfterkickPlayer = 15f;
            }
            else Debug.Log("mimolle na pas toucher");
        }
    }

    IEnumerator AttackAnimation()
    {
        transform.GetChild(0).DOScaleY(0.7f, 0.45f).SetEase(Ease.InCubic);
        yield return new WaitForSeconds(0.5f);
        transform.GetChild(0).DOScaleY(1f, 0.2f).SetEase(Ease.OutCubic);
       
    }
}
