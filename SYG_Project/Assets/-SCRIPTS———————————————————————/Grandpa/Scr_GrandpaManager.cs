using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_GrandpaManager : MonoBehaviour
{
    [SerializeField]
    Transform player , parentGranpa;

    [SerializeField]
    Transform parentBedroom;
    [SerializeField] Transform[] Bedrooms = new Transform[4];
    
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
            grandpas[i] = Instantiate(grandpaPrefab[i] , parentGranpa.position + Vector3.right * 2 * i , Quaternion.identity, parentGranpa);


            if (grandpas[i].GetComponent<Scr_Character>() != null)
            {
                grandpas[i].GetComponent<Scr_Character>().player = player;
            }

            if (parentBedroom.GetChild(i) != null)
            {
                Bedrooms[i] = parentBedroom.GetChild(i);
            }

            if (grandpas[i].GetComponent<Scr_Character>() != null)
            {
                if (Bedrooms[i] != null)
                {
                    grandpas[i].GetComponent<Scr_Character>().bedroom = Bedrooms[i];
                }
                else Debug.Log($"Pas de chambre {i}");
            }
            else Debug.Log($"Pas de vieux {i}");
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
