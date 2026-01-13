using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_CrazyCat : GrandpaParent
{
    [SerializeField]
    LayerMask layerMask;


    private bool _shearchingPlayer;
    private float _timerNotShowPlayer;
    

    [SerializeField]
    GameObject catPrefab;
    
    private bool _catIsSpawned = false;
    

     private GameObject _currentCat;


    [SerializeField]
    private Transform _catSpawnPointParent;

    private Transform[] _catSpawnPoint;

    private void Start()
    {
        _catSpawnPoint = new Transform[_catSpawnPointParent.childCount];
        for(int i = 0; i < _catSpawnPointParent.childCount; i++)
        {
            _catSpawnPoint[i] = _catSpawnPointParent.GetChild(i);
        }
    }


    void CheckCanViewPlayer()
    {
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



    public override void Slap()
    {
        Debug.Log("Trouve le chat");
        if (_catIsSpawned == false)
        {
            _currentCat = Instantiate(catPrefab, _catSpawnPoint[Random.Range(0, _catSpawnPoint.Length)].position, Quaternion.identity);
            _currentCat.GetComponent<Scr_Cat>().crazyCat = this;
            _catIsSpawned = true;
        }
    }

    protected override void Update()
    {
        if (GAME.MANAGER.CurrentState != State.gameplay) return;

        CheckCanViewPlayer();


        if (inBedroom == true)
        {
            UpdateTimer();
        }
        else if (ArrivedToDestination())
        {
            if (controlledMove)
            {
                exitBedroomTimer = 10f;
                inBedroom = true;
                ActivateAgent(false);
            }
            else if (_shearchingPlayer) SetRandomDestination(transform.position, 10f);
            else SetDestinationToPlayer();
        }

        if (agent.isActiveAndEnabled) agent.isStopped = !canMove;
    }

    void SetDestinationToPlayer()
    {
        targetPosition = player.position;
    }
}
