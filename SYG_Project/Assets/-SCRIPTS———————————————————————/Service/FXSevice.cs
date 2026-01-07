using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FXSevice : MonoBehaviour, IFXService
{
    private void Awake()
    {
        ServicesLocator.Register<IFXService>(this);
    }

    public void PlayFx(GameObject prefabFX, Vector3 position)
    {
        Instantiate(prefabFX, position, Quaternion.identity);
    }
}
