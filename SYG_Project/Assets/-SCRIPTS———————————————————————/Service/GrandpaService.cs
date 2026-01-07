using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrandpaService : MonoBehaviour, IGranpaService
{
    [SerializeField]
    Transform player;

    [SerializeField]
    private Transform _parentBedroom;
    [SerializeField]
    private Transform _grandpaParent;

    private Transform[] _Bedrooms = new Transform[4];
    private Transform[] _grandpas = new Transform[4];

    
    private void Awake()
    {
        ServicesLocator.Register<IGranpaService>(this);
        EVENTS.OnGameStart += SpawnGrandpa;
    }

    private void OnDestroy()
    {
        ServicesLocator.Unregister<IGranpaService>();
        EVENTS.OnGameStart -= SpawnGrandpa;
    }

    private void Start()
    {
        for (int i = 0; i < _Bedrooms.Length; i++)
        {
            _Bedrooms[i] = _parentBedroom.GetChild(i);
        }
        for (int i = 0; i < _grandpas.Length; i++)
        {
            _grandpas[i] = _grandpaParent.GetChild(i);
        }
    }

    public void SpawnGrandpa()
    {
        for (int i = 0; i < _grandpas.Length; i++)
        {
            _grandpas[i] = _grandpaParent.GetChild(i);


            if (_grandpas[i].GetComponent<GrandpaParent>() != null && _parentBedroom.GetChild(i) != null)
            {
                _grandpas[i].GetComponent<GrandpaParent>().player = player;
                _Bedrooms[i] = _parentBedroom.GetChild(i);
                _grandpas[i].GetComponent<GrandpaParent>().bedroom = _Bedrooms[i];
            }
            else Debug.Log($"1 ou 2 elements manquant dans la paire chambre/vieux numero {i}");
        }

        
    }

    public void CheckBedroom()
    {
        foreach(Transform grandpa in _grandpas)
        {
            if(grandpa.GetComponent<GrandpaParent>().inBedroom == false)
            {
                return;
            }
        }

        Debug.Log("win");
    }
}
