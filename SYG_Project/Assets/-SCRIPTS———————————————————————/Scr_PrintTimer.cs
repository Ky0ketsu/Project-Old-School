
using UnityEngine;
using UnityEngine.UI;

public class Scr_PrintTimer : MonoBehaviour
{
    [SerializeField]
    Scr_GameTimer gameTimer;

    void Update()
    {
        Text textTime = GetComponentInChildren<Text>();

        if (textTime != null)
        {
            if (gameTimer != null)
            {
                textTime.text = gameTimer.currentTimer.ToString();
            }
            else Debug.Log("Pas de Script");
        }
        else Debug.Log("Pas de Component");
        
    }
}
