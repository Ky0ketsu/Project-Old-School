using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scr_GameTimer : MonoBehaviour
{
    public float timer = 300f; //60 min = 300 sec

    public void Update()
    {
        timer -= Time.deltaTime;

        if (timer < 0 )
        {
            SceneManager.LoadScene("EndScene", LoadSceneMode.Single); 
        }
    }
}
