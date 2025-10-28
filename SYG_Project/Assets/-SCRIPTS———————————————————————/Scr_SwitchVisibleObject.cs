using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class Scr_SwitchVisibleObject : MonoBehaviour
{
    [SerializeField]
    bool active;

    [SerializeField]
    GameObject[] _gameObjects;


    void Update()
    {
        foreach (GameObject gameObject in _gameObjects)
        {
            if (gameObject != null)
            {
                gameObject.SetActive(active);
            }
        }
    }
}
