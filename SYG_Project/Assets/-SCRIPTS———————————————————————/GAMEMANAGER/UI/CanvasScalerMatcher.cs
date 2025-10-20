using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class CanvasScalerMatcher : MonoBehaviour
{
    CanvasScaler scaler => GetComponent<CanvasScaler>();

    void OnEnable()
    {
        SetRatio(AspectRatioGlobal.GlobalRatio);
        EVENTS.OnGameRatioChange += SetRatio;
    }

    void OnDisable()
    {
        EVENTS.OnGameRatioChange -= SetRatio;
    }

    void SetRatio(float desired)
    {
        if (scaler != null) scaler.matchWidthOrHeight = AspectRatioGlobal.GlobalRatio > 1.7777f ? 1f : 0;
    }

} // SCRIPT END
