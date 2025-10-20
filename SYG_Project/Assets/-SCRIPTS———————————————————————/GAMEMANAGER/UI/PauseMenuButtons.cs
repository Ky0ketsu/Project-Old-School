using UnityEngine;

public class PauseMenuButtons : MonoBehaviour
{
    public void Resume()
    {
        GAME.MANAGER.Resume();
    }

    public void BackToMainMenu()
    {
        MENU.SCRIPT.BackToMainMenu();
    }

    public void Settings()
    {
        MENU.SCRIPT.ShowSettingsFromPause();
    }
   
} // SCRIPT END
