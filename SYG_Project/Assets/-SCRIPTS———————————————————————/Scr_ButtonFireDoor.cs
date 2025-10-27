using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Scr_ButtonFireDoor : MonoBehaviour, ISlapable
{
    public float timer = 15f;
    public bool isClosed = false;
    public GameObject doorToSpawn;
    public GameObject loadedDoor; 

   public void Slap()
   {
        timer = 15f;
        if (isClosed == true)
        {
            Debug.Log("RESET TIMER DOOR");
            return;
        }
        CloseDoor();
   }

   void CloseDoor()
    {
        isClosed = true;

        loadedDoor = Instantiate(doorToSpawn,new Vector3(2, 1, 6.5f), Quaternion.identity);
    }

    public void Update()
    {
        if (isClosed == true) timer = timer - Time.deltaTime;

        if (timer < 0)
        {
            timer = 15f;
            Debug.Log("FIRE DOOR OPEN");
            isClosed = false;
            Destroy(loadedDoor);
        }
           
    }
}
