using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_RandomizeSpawnObject : MonoBehaviour
{
    [SerializeField]
    private Transform parentSpawnPoint;

    [SerializeField]
    private Transform[] spawnPointList;

    [SerializeField]
    private Transform[] objectList = new Transform[4];

    private void Start()
    {
        spawnPointList = new Transform[parentSpawnPoint.childCount];

        for(int i = 0; i < spawnPointList.Length; i++)
        {
            spawnPointList[i] = parentSpawnPoint.GetChild(i).transform;
        }

        for(int i = 0;i < objectList.Length; i++)
        {
            objectList[i] = transform.GetChild(i).transform;
        }

        AttributeBedroom();
    }

    void AttributeBedroom()
    {
        for (int i = 0; i < objectList.Length; i++)
        {
            int index = Random.Range(0, spawnPointList.Length);
            while (spawnPointList[index] == null)
            {
                index = Random.Range(0, spawnPointList.Length);
            }
            if (spawnPointList[index] != null)
            {
                objectList[i].position = spawnPointList[index].position;
                objectList[i].eulerAngles = spawnPointList[index].eulerAngles;
                spawnPointList[index] = null;
            }
        }
    }
}
