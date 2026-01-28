using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_CrazyCat : GrandpaParent
{
    [SerializeField] GameObject healCrazyCatVFX;

    [SerializeField]
    LayerMask layerMask;


    private bool _shearchingPlayer;
    private float _timerNotShowPlayer;
    

    [SerializeField]
    GameObject catPrefab;
    
    public bool _catIsSpawned = false;
    

     private GameObject _currentCat;


    [SerializeField]
    private Transform _catSpawnPointParent;

    private Transform[] _catSpawnPoint;

    void Start()
    {
        _catSpawnPoint = new Transform[_catSpawnPointParent.childCount];
        for(int i = 0; i < _catSpawnPointParent.childCount; i++)
        {
            _catSpawnPoint[i] = _catSpawnPointParent.GetChild(i);
        }
    }

    void CheckCanViewPlayer()
    {
        if (!CanSeePlayer()) return;

        RaycastHit hit;
        if (Physics.Raycast(transform.position + Vector3.up, player.position - transform.position, out hit, Vector3.Distance(transform.position, player.position) * 1.05f, layerMask))
        {
            if (hit.transform.GetComponent<SlapAction>() != null)
            {
                _shearchingPlayer = false;
                _timerNotShowPlayer = 3f;

                Debug.DrawRay(transform.position + Vector3.up, (player.position - transform.position).normalized * Vector3.Distance(transform.position, player.position), Color.green);
            }
            else
            {
                if (_timerNotShowPlayer > 0) _timerNotShowPlayer -= Time.deltaTime;
                if (_timerNotShowPlayer <= 0) _timerNotShowPlayer = 0f; _shearchingPlayer = true;

                Debug.DrawRay(transform.position + Vector3.up, (player.position - transform.position).normalized * Vector3.Distance(transform.position, player.position), Color.red);
            }
        }
    }

    public override void GoBedroom()
    {
        SetRandomDestination(bedroom.position,0);
        controlledMove = true;
        Debug.DrawLine(transform.position, targetPosition, Color.magenta, 10f);

        Instantiate(healCrazyCatVFX, transform.position, Quaternion.identity);
    }

    public override void Slap()
    {
        Debug.Log("Trouve le chat");
        if (_catIsSpawned == false && !controlledMove)
        {
            _currentCat = Instantiate(catPrefab, _catSpawnPoint[Random.Range(0, _catSpawnPoint.Length)].position, Quaternion.identity);
            _currentCat.GetComponent<Scr_Cat>().crazyCat = this;
            _catIsSpawned = true;
        }
    }

    protected override void Update()
    {
        if (GAME.MANAGER.CurrentState != State.gameplay) return;

        if (!inBedroom && !ArrivedToDestination())
        {
            int d = Random.Range(0, footStep.Count);
            audioFootStep.clip = footStep[d];
            audioFootStep.Play();
        }

        CheckCanViewPlayer();

        if (_shearchingPlayer == true) agent.speed = 2;
        else if (!controlledMove) SetDestinationToPlayer();

        if (inBedroom == true)
        {
            UpdateTimer();
        }
        else if (ArrivedToDestination())
        {
            if (controlledMove)
            {
                exitBedroomTimer = 100f;
                inBedroom = true;
                ActivateAgent(false);
            }
            else if (!_shearchingPlayer) SetRandomDestination(transform.position, 10f);
            else SetDestinationToPlayer();

        }

        if (agent.isActiveAndEnabled) agent.isStopped = !canMove;
    }

    void SetDestinationToPlayer()
    {
        agent.speed = 4;
        targetPosition = player.position;
        if(ArrivedToDestination()) return;
        SetRandomDestination(player.position, 0f);
    }
}
