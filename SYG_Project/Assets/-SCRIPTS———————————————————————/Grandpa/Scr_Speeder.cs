using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Scr_Speeder : Scr_Character
{
    [SerializeField, Range(0f, 50f)] float speed;
    private float currentSpeed;

    private Vector3[] dir = new Vector3[8] { Vector3.forward, -Vector3.forward, Vector3.right, -Vector3.right, (Vector3.forward+Vector3.left).normalized, (Vector3.forward+ Vector3.right).normalized,(Vector3.back + Vector3.left).normalized,( Vector3.back+ Vector3.right ).normalized};
    [SerializeField] float minimumDistante = 3f;

    public List<AudioClip> stunList;
    public List<AudioClip> crashList;
    public List<AudioClip> runList;
    public List<AudioClip> glissList;
    [SerializeField] AudioSource audioAction;
    [SerializeField] AudioSource audioCrash;
    [SerializeField] AudioSource audioMove;
    [SerializeField] AudioSource audioGliss;



    bool isStun;

    public override void SetRandomDestination(Vector3 center, float randomMaxDistance)
    {

        bool mouvIsSet = false;
        int currentDirIndex = Random.Range(0, dir.Length);
        

        while(mouvIsSet == false)
        {
            NavMeshHit hit;

            Vector3 lastPickPosition = center;
            lastPickPosition.y = -1f;
            Vector3 currentPickPosition = lastPickPosition;

            bool currentIsOnNavmesh = true;
            int currentIndex = 0;

            while (currentIsOnNavmesh == true)
            {
                lastPickPosition = currentPickPosition;
                currentPickPosition += dir[currentDirIndex]*2f;
                Debug.Log(dir[currentDirIndex]);
                Debug.Log(currentPickPosition);

                Debug.DrawLine(lastPickPosition, currentPickPosition, Color.yellow, 1f);

                if (NavMesh.SamplePosition(currentPickPosition, out hit, 1f, NavMesh.AllAreas))
                {
                    currentIndex++;
                    Debug.DrawLine(transform.position, hit.position,Color.green,1f);
                }
                else
                { 
                    currentIsOnNavmesh = false;
                }
                
            } // fin du petit while

            if (currentIndex < 3)
            {
                break;
            }
            targetPosition = lastPickPosition;
            mouvIsSet = true;

        } // fin du while


        if(mouvIsSet == true )
        {
            agent.SetDestination(targetPosition);
        }

    } // fin de SetRandomDestination

    public override void Update()
    {
        if (GAME.MANAGER.CurrentState != State.gameplay) return;
        if (inBedroom == true)
        {
            UpdateTimer();
        }
        else if (ArrivedToDestination())
        {
            if (controlledMove)
            {
                timer = 10f;
                inBedroom = true;
                ActivateAgent(false);
            }
            else if (!isStun)
            {
                currentSpeed = 0;
                Stun(2f);

                int r = Random.Range(0, stunList.Count);
                audioAction.clip = stunList[r];
                audioAction.Play();

                int rr = Random.Range(0, crashList.Count);
                audioCrash.clip = crashList[rr];
                audioCrash.Play();
            }
        }

        if (!isStun && currentSpeed < speed) currentSpeed += 3f * Time.deltaTime * (1 + currentSpeed / 10);

        if (agent.isActiveAndEnabled) agent.isStopped = !canMove;
        agent.speed = currentSpeed;
    }

    void Stun(float stunTime)
    {
        isStun = true;

        StartCoroutine(StunRoutine(stunTime));
        
    }

    IEnumerator StunRoutine(float StunTime)
    {
        yield return new WaitForSeconds(StunTime);
        SetRandomDestination(transform.position, 10f);
        isStun = false;
    }
}
