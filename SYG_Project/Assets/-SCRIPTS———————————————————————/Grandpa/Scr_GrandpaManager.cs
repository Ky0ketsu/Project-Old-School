using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_GrandpaManager : MonoBehaviour
{
    [SerializeField]
    Transform player;

    [SerializeField]
    Transform parentBedroom;
    [SerializeField] Transform[] Bedrooms = new Transform[4];
   
    public GameObject[] grandpas = new GameObject[4];

    [SerializeField]
    private  Transform grandpaParent;


    private void Awake()
    {
        EVENTS.OnGameStart += SpawnGrandpa;
    }

    private void OnDestroy()
    {
        EVENTS.OnGameStart -= SpawnGrandpa;
    }

    public void SpawnGrandpa()
    {
        for (int i = 0; i < grandpas.Length; i++)
        {
            grandpas[i] = grandpaParent.GetChild(i).gameObject;


            if (grandpas[i].GetComponent<Scr_GranpaOrigin>() != null && parentBedroom.GetChild(i) != null)
            {
                grandpas[i].GetComponent<Scr_GranpaOrigin>().player = player;
                Bedrooms[i] = parentBedroom.GetChild(i);
                grandpas[i].GetComponent<Scr_GranpaOrigin>().bedroom = Bedrooms[i];
            }
            else Debug.Log($"1 ou 2 elements manquant dans la paire chambre/vieux numero {i}");
        }

        
    }


}
