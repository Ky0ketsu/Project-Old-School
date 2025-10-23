using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_GrandpaManager : MonoBehaviour
{
    [SerializeField]
    Transform player , parentGranpa;
    
    public GameObject[] grandpaPrefab = new GameObject[4];
    public GameObject[] grandpas = new GameObject[4];


    private void Awake()
    {
        EVENTS.OnGameStart += SpawnGrandpa;
        EVENTS.OnGameOver += DestroyGrandpa;
    }

    private void OnDestroy()
    {
        EVENTS.OnGameStart -= SpawnGrandpa;
        EVENTS.OnGameOver -= DestroyGrandpa;
    }

    public void SpawnGrandpa()
    {
        for (int i = 0; i < grandpas.Length; i++)
        {
            grandpas[i] = Instantiate(grandpaPrefab[i] , parentGranpa.position + Vector3.right * i , Quaternion.identity, parentGranpa);
            grandpas[i].GetComponent<Scr_Character>().player = player;
        }
    }

    public void DestroyGrandpa()
    {
        foreach (GameObject grandpa in grandpas)
        {
            Destroy(grandpa);
        }
    }
}
