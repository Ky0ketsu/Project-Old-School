using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Scr_Speeder : Scr_Character
{
    [Range(0f, 50f)] float speed;

    private Vector3[] dir = new Vector3[8] { Vector3.forward, -Vector3.forward, Vector3.right, -Vector3.right, (Vector3.forward+Vector3.left).normalized, (Vector3.forward+ Vector3.right).normalized,(Vector3.back + Vector3.left).normalized,( Vector3.back+ Vector3.right ).normalized};
    [SerializeField] float minimumDistante = 3f;

    public override void SetRandomDestination(Vector3 center, float randomMaxDistance)
    {

        bool mouvIsSet = false;
        int currentDirIndex = Random.Range(0, dir.Length);


        int echecMax1 =0;
       
        

        while(mouvIsSet == false)
        {
            NavMeshHit hit;

            Vector3 lastPickPosition = transform.position;
            lastPickPosition.y = -1f;
            Vector3 currentPickPosition = lastPickPosition;

            bool currentIsOnNavmesh = true;
            int currentIndex = 0;

            int echecMax2 = 0;
            echecMax1++;
            if (echecMax1 > 5)
            {
                Debug.Log("Pas de Destination trouver");
                break;
            }

            while (currentIsOnNavmesh == true && echecMax2 < 5)
            {
                lastPickPosition = currentPickPosition;
                currentPickPosition += dir[currentDirIndex]*2f;
                Debug.Log(dir[currentDirIndex]);
                Debug.Log(currentPickPosition);

                Debug.DrawLine(lastPickPosition, currentPickPosition, Color.yellow, 1f);

                if (NavMesh.SamplePosition(currentPickPosition, out hit, 1f, NavMesh.AllAreas))
                {
                    currentIndex++;
                    Debug.DrawLine(transform.position, hit.position,Color.green,1f);
                    echecMax2++;
                }
                else
                { 
                    currentIsOnNavmesh = false;
                }
                
            } // fin du petit while
   
            targetPosition = lastPickPosition;
            mouvIsSet = true;

        } // fin du while


        if(mouvIsSet == true || echecMax1 > 2)
        {
            agent.SetDestination(targetPosition);
        }

    } // fin de SetRandomDestination

    void ShortStun()
    {
        
    }

    void LongStun()
    {

    }


}
