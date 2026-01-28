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
    public Transform[] grandpas = new Transform[4];

    
    private void Awake()
    {
        ServicesLocator.Register<GrandpaService>(this);
        EVENTS.OnGameStart += SpawnGrandpa;
    }

    private void OnDestroy()
    {
        ServicesLocator.Unregister<GrandpaService>();
        EVENTS.OnGameStart -= SpawnGrandpa;
    }

    private void Start()
    {
        if (_parentBedroom == null) return;
        for (int i = 0; i < _Bedrooms.Length; i++)
        {
            _Bedrooms[i] = _parentBedroom.GetChild(i);
        }
        if (_grandpaParent == null) return;
        for (int i = 0; i < grandpas.Length; i++)
        {
            grandpas[i] = _grandpaParent.GetChild(i);
        }
    }

    public void SpawnGrandpa()
    {
        if (grandpas == null) return;
        for (int i = 0; i < grandpas.Length; i++)
        {
            grandpas[i] = _grandpaParent.GetChild(i);


            if (grandpas[i].GetComponent<GrandpaParent>() != null && _parentBedroom.GetChild(i) != null)
            {
                grandpas[i].GetComponent<GrandpaParent>().player = player;
                _Bedrooms[i] = _parentBedroom.GetChild(i);
                grandpas[i].GetComponent<GrandpaParent>().bedroom = _Bedrooms[i];
            }
            else Debug.Log($"1 ou 2 elements manquant dans la paire chambre/vieux numero {i}");
        }

        
    }

    public void CheckBedroom()
    {
        foreach(Transform grandpa in grandpas)
        {
            if(grandpa.GetComponent<GrandpaParent>().inBedroom == false)
            {
                return;
            }
        }

        EVENTS.InvokeVictory();
    }

    
}
