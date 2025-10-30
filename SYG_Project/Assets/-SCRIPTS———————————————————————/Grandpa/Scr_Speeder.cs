using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Scr_Speeder : Scr_Character
{
    [Range(0f, 50f)] float speed;

    [SerializeField] private Vector3[] dir = new Vector3[8];
    [SerializeField] float minimumDistante = 3f;

    public override void SetRandomDestination(Vector3 center, float randomMaxDistance)
    {
        for (int i = 0; i < dir.Length; i++)
        {
            dir[i] = (Vector3.up * 360/ dir.Length) * (i - 1);
        }

        bool mouvIsSet = false;
        Vector3[] tempDir = dir;
        int currentDirIndex = Random.Range(0, dir.Length);
        

        while(mouvIsSet == false)
        {
            NavMeshHit hit;

            Vector3 lastPickPosition = transform.position;
            Vector3 currentPickPosition = transform.position;
            bool currentIsOnNavmesh = true;
            int currentIndex = 0;

            while (currentIsOnNavmesh == true)
            {
                lastPickPosition = currentPickPosition;
                currentPickPosition = lastPickPosition += dir[currentDirIndex].normalized * 1f;

                if (NavMesh.SamplePosition(currentPickPosition, out hit, 0.5f, NavMesh.AllAreas))
                {
                    currentIndex++;
                }
                else currentIsOnNavmesh = false;

            }
            if (currentIsOnNavmesh == false)
            {
                if (NavMesh.SamplePosition(lastPickPosition, out hit, 1f, NavMesh.AllAreas))
                {
                    if (currentIndex >= minimumDistante)
                    {
                        
                        targetPosition = hit.position;
                        mouvIsSet = true;
                    }
                }
                else Debug.Log("Echec au point final");
            }
        }
        if(mouvIsSet == true)
        {
            agent.SetDestination(targetPosition);
        }

    }

    void ShortStun()
    {
        
    }

    void LongStun()
    {

    }


}
