using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
[RequireComponent(typeof(AspectRatioFitter))]
public class AspectRatioGlobal : MonoBehaviour
{
    public static float GlobalRatio{get{return globalRatio;}set{if (globalRatio!=value) {globalRatio=value;EVENTS.InvokGameRatioChange(value);}}}
    static float globalRatio = 1.77777f;
    AspectRatioFitter fitter => GetComponent<AspectRatioFitter>();

    void Awake()
    {
        SetRatio(GlobalRatio);
    }

    void OnEnable()
    {
        SetRatio(GlobalRatio);
        EVENTS.OnGameRatioChange += SetRatio;
    }

    void OnDisable()
    {
        EVENTS.OnGameRatioChange -= SetRatio;
    }

    void SetRatio(float desired)
    {
        //fitter.aspectMode = AspectRatioFitter.AspectMode.FitInParent;
        fitter.aspectRatio = desired;
    }
} // SCRIPT END
