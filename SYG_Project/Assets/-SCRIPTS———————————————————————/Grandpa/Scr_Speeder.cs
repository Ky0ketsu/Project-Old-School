using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Scr_Speeder : GrandpaParent
{
    [SerializeField, Range(0f, 50f)] float speed;
    private float currentSpeed;

    private Vector3[] dir = new Vector3[8]
    {
      Vector3.forward,
      -Vector3.forward,
      Vector3.right,
      -Vector3.right,
      (Vector3.forward + Vector3.left).normalized,
      (Vector3.forward + Vector3.right).normalized,
      (Vector3.back + Vector3.left).normalized,
      (Vector3.back + Vector3.right).normalized
    };
    
    [SerializeField] float minimumDistante = 3f;

    public List<AudioClip> stunList;
    public List<AudioClip> crashList;
    public List<AudioClip> runList;
    [SerializeField] AudioSource audioAction;
    [SerializeField] AudioSource audioCrash;
    [SerializeField] AudioSource audioRun;

    public GameObject stunFX;

    [SerializeField]
    private LayerMask doorLayer;

    private bool _isStun;

    public override void SetRandomDestination(Vector3 center, float randomMaxDistance)
    {
        bool mouvIsDefine = false;
        int currentDirIndex = Random.Range(0, dir.Length);

        while(mouvIsDefine == false)
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

                RaycastHit hitDoor;
                if (Physics.Raycast(lastPickPosition, dir[currentDirIndex], out hitDoor, 2f, doorLayer))
                {
                    if (hitDoor.transform.GetComponent<FireDoor>())
                    {
                        currentIsOnNavmesh = false;
                    }
                }
                else
                {
                    if (NavMesh.SamplePosition(currentPickPosition, out hit, 1f, NavMesh.AllAreas))
                    {
                        currentIndex++;
                    }
                    else
                    {
                        currentIsOnNavmesh = false;
                    }
                }
            } // fin du petit while

            if (currentIndex < minimumDistante)
            {
                break;
            }

            Debug.DrawLine(transform.position, lastPickPosition, Color.white, 1f);
            targetPosition = lastPickPosition;
            mouvIsDefine = true;
        } // fin du while


        if(!controlledMove)
        {
            agent.SetDestination(targetPosition);

            if (GAME.MANAGER.CurrentState != State.gameplay) { return;}

            int d = Random.Range(0, runList.Count);
            audioRun.clip = runList[d];
            audioRun.Play();
            Debug.Log("Je fonce");
        }

    } // fin de SetRandomDestination

    public override void GoBedroom()
    {
        agent.SetDestination(bedroom.position);
        controlledMove = true;
        Debug.DrawLine(transform.position, targetPosition, Color.magenta, 10f);
    }


    protected override void Update()
    {
        if (GAME.MANAGER.CurrentState != State.gameplay) return;
        if (inBedroom == true)
        {
            UpdateTimer();
            audioAction.Stop();
            audioRun.Stop();
            audioCrash.Stop();
        }
        else if (ArrivedToDestination())
        {
            RaycastHit hitDoor;
            if (Physics.Raycast(transform.position, transform.forward, out hitDoor, 2f, doorLayer))
            {
                if (hitDoor.transform.GetComponent<FireDoor>().isClosed)
                {
                    hitDoor.transform.GetComponent<FireDoor>().ChangeDoorState();
                }
            }

            if (controlledMove)
            {
                exitBedroomTimer = 100f;
                inBedroom = true;
                ActivateAgent(false);
            }
            else if (!_isStun)
            {
                currentSpeed = 0;
                Stun(2f);
                audioRun.Stop();

                int r = Random.Range(0, stunList.Count);
                audioAction.clip = stunList[r];
                audioAction.Play();

                GameObject stunVFX  = Instantiate(stunFX, transform.position+(transform.up*1.5f),transform.rotation);
                Destroy(stunVFX, 2f);
                
                


                int rr = Random.Range(0, crashList.Count);
                audioCrash.clip = crashList[rr];
                audioCrash.Play();
            }
        }

        if (!_isStun && currentSpeed < speed) currentSpeed += 3f * Time.deltaTime * (1 + currentSpeed / 10);

        if (agent.isActiveAndEnabled) agent.isStopped = !canMove;
        agent.speed = currentSpeed;

    }

    public override void Slap()
    {
        if (!_isStun) return;
        Debug.Log($"{transform.name} a pris une claque");
        GoBedroom();
    }


    void Stun(float stunTime)
    {
        _isStun = true;

        StartCoroutine(StunRoutine(stunTime));
        
    }

    IEnumerator StunRoutine(float StunTime)
    {
        yield return new WaitForSeconds(StunTime);
        SetRandomDestination(transform.position, 10f);
        _isStun = false;
        
    }
}
