using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scr_LaunchGameOver : MonoBehaviour
{

    [SerializeField] GameObject Tablet;
    private void Awake()
    {
        EVENTS.OnGameOver += launchGameOver;
    }

    private void OnDestroy()
    {
        EVENTS.OnGameOver -= launchGameOver;
    }

    public void launchGameOver()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("EndScene");
        //this.enabled = false; 
        //Destroy(gameObject);
    }

    public void StartAgain()
    {
        //AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Scene par default");
        //this.enabled=false;
    }
}
