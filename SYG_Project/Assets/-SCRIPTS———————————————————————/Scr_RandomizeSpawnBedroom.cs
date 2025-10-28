using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_RandomizeSpawnBedroom : MonoBehaviour
{
    [SerializeField]
    private Transform parentSpawnPoint;

    [SerializeField]
    private Transform[] spawnPoints;

    [SerializeField]
    private Transform[] bedrooms = new Transform[4];

    private void Start()
    {
        spawnPoints = new Transform[parentSpawnPoint.childCount];

        for(int i = 0; i < spawnPoints.Length; i++)
        {
            spawnPoints[i] = parentSpawnPoint.GetChild(i).transform;
        }

        for(int i = 0;i < bedrooms.Length; i++)
        {
            bedrooms[i] = transform.GetChild(i).transform;
        }

        AttributeBedroom();
    }

    void AttributeBedroom()
    {
        for (int i = 0; i < bedrooms.Length; i++)
        {
            int index = Random.Range(0, spawnPoints.Length);
            while (spawnPoints[index] == null)
            {
                index = Random.Range(0, spawnPoints.Length);
            }
            if (spawnPoints[index] != null)
            {
                bedrooms[i].position = spawnPoints[index].position;
                bedrooms[i].eulerAngles = spawnPoints[index].eulerAngles;
                spawnPoints[index] = null;
            }
        }
    }
}
