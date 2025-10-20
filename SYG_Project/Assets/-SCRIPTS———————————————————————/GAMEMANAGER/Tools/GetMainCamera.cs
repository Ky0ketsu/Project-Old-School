using UnityEngine;

public class GetMainCamera : MonoBehaviour
{
    static int getMainCamInstances = 0;

    void OnEnable()
    {
        getMainCamInstances++;
        if (getMainCamInstances > 1)
        {
            Debug.Log("🎦⚠️ Can't enable two GetMainCamera components in scene!");
            this.enabled = false;
            return;
        }
        CopyTempCameraSettings();
    }

    void OnDisable()
    {
        getMainCamInstances--;
    }

    void CopyTempCameraSettings()
    {
        Camera tempCam = GetComponentInChildren<Camera>();
        if (GAME.MANAGER.gameCam == null) Debug.Log("🎦⚠️ GAME MANAGER HAS NO GAME CAMERA!");
        else if (tempCam != null)
        {
            GAME.MANAGER.gameCam.orthographic = tempCam.orthographic;
            GAME.MANAGER.gameCam.orthographicSize = tempCam.orthographicSize;
            GAME.MANAGER.gameCam.fieldOfView = tempCam.fieldOfView;
            GAME.MANAGER.gameCam.nearClipPlane = tempCam.nearClipPlane;
            GAME.MANAGER.gameCam.farClipPlane = tempCam.farClipPlane;
            Destroy(tempCam.gameObject);
        }
    }



    void LateUpdate()
    {
        GAME.MANAGER.gameCam.transform.position = transform.position;
        GAME.MANAGER.gameCam.transform.rotation = transform.rotation;
    }
} // SCRIPT END
