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


            if (grandpas[i].GetComponent<Scr_GrandpaOrigin>() != null && parentBedroom.GetChild(i) != null)
            {
                grandpas[i].GetComponent<Scr_GrandpaOrigin>().player = player;
                Bedrooms[i] = parentBedroom.GetChild(i);
                grandpas[i].GetComponent<Scr_GrandpaOrigin>().bedroom = Bedrooms[i];
            }
            else Debug.Log($"1 ou 2 elements manquant dans la paire chambre/vieux numero {i}");
        }

        
    }

    public void CountGrandpa()
    {
        if (grandpas[0].GetComponent<Scr_Mimolle>().inBedroom == true && grandpas[1].GetComponent<Scr_CrazyCat>().inBedroom == true &&
            grandpas[2].GetComponent<Scr_Rex>().inBedroom == true && grandpas[3].GetComponent<Scr_Speeder>().inBedroom == true)
        {
            Debug.Log("win");
        }
    }
}
