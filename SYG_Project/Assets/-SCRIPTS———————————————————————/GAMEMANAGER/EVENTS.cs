using System;
using UnityEngine;
using UnityEngine.UI;

public class EVENTS
{
    static void LogEventInConsole(string eventName) // Custom message to display in Unity Console when event is invoked
    {
        Debug.Log("ℹ️ " + eventName);
    }

    public static event Action OnInitialization;
    public static void InvokeInitialization() { LogEventInConsole("Initialization"); OnInitialization?.Invoke(); }

    public static event Action<int> OnSceneLoaded;
    public static void InvokeSceneLoaded(int buildIndex) { LogEventInConsole("🏞SceneLoaded"); OnSceneLoaded?.Invoke(buildIndex); }

    public static event Action OnGameStart;
    public static void InvokeGameStart() { LogEventInConsole("▶️GameStart"); OnGameStart?.Invoke(); }

    public static event Action OnGameOver;
    public static void InvokeGameOver() { LogEventInConsole("💀GameOver"); OnGameOver?.Invoke(); }

    public static event Action OnVictory;
    public static void InvokeVictory() { OnVictory?.Invoke(); }

    public static event Action OnGamePause;
    public static void InvokeGamePause() { LogEventInConsole("⏯️GamePause"); OnGamePause?.Invoke(); }

    public static event Action OnGamePauseExit;
    public static void InvokeGamePauseExit() { LogEventInConsole("❌GamePauseExit"); OnGamePauseExit?.Invoke(); }

    public static event Action OnGamePauseReboot;
    public static void InvokeGamePauseReboot() { LogEventInConsole("♻️GamePauseReboot"); OnGamePauseReboot?.Invoke(); }

    public static event Action OnGameResume;
    public static void InvokeGameResume() { LogEventInConsole("⏯️GameResume"); OnGameResume?.Invoke(); }

    public static event Action OnMenu;
    public static void InvokeMenu() { LogEventInConsole("Menu"); OnMenu?.Invoke(); }

    public static event Action OnMenuExit;
    public static void InvokeMenuExit() { LogEventInConsole("❌MenuExit"); OnMenuExit?.Invoke(); }

    public static event Action OnMenuReboot;
    public static void InvokeMenuReboot() { LogEventInConsole("♻️MenuReboot"); OnMenuReboot?.Invoke(); }

    public static event Action OnGameplay;
    public static void InvokeGameplay() { LogEventInConsole("Gameplay"); OnGameplay?.Invoke(); }

    public static event Action OnGameplayExit;
    public static void InvokeGameplayExit() { LogEventInConsole("❌GameplayExit"); OnGameplayExit?.Invoke(); }

    public static event Action OnGameplayReboot;
    public static void InvokeGameplayReboot() { LogEventInConsole("♻️GameplayReboot"); OnGameplayReboot?.Invoke(); }

    public static event Action OnWaiting;
    public static void InvokeWaiting() { LogEventInConsole("Waiting"); OnWaiting?.Invoke(); }

    public static event Action OnWaitingExit;
    public static void InvokeWaitingExit() { LogEventInConsole("❌WaitingExit"); OnWaitingExit?.Invoke(); }

    public static event Action OnWaitingReboot;
    public static void InvokeWaitingReboot() { LogEventInConsole("♻️WaitingReboot"); OnWaitingReboot?.Invoke(); }

    public static event Action OnMouse;
    public static void InvokeMouse() { LogEventInConsole("🖱️Mouse"); OnMouse?.Invoke(); }

    public static event Action OnController;
    public static void InvokeController() { LogEventInConsole("🎮Controller"); OnController?.Invoke(); }

    public static event Action OnScreenResChange;
    public static void InvokScreenResChange() { /*LogEventInConsole("ScreenResChange 🖥️");*/ OnScreenResChange?.Invoke(); }

    public static event Action<float> OnGameRatioChange;
    public static void InvokGameRatioChange(float newRatio) { /*LogEventInConsole("GameRatioChange " + newRatio);*/ OnGameRatioChange?.Invoke(newRatio); }

    public static event Action<float> OnMouseSensitivityChange;
    public static void InvokeMouseSensitivityChange(float desired) { Debug.Log("🖱️MouseSensitivityChange " + desired); OnMouseSensitivityChange?.Invoke(desired); }

    public static event Action OnMouseCursorLocked;
    public static void InvokeMouseCursorLocked() { Debug.Log("🖱️🔒MouseCursorLocked"); OnMouseCursorLocked?.Invoke(); }

    public static event Action OnMouseCursorUnlocked;
    public static void InvokeMouseCursorUnlocked() { Debug.Log("🖱️🔓MouseCursorUnlocked"); OnMouseCursorUnlocked?.Invoke(); }

    public static event Action<Selectable> OnUIElementSelected;
    public static void InvokeUIElementSelected(Selectable element) { LogEventInConsole("UIElementSelected " + element.name); OnUIElementSelected?.Invoke(element); }

    public static event Action<Selectable> OnUIElementUnselect;
    public static void InvokeUIElementUnselect(Selectable element) { LogEventInConsole("UIElementUnselect " + element.name); OnUIElementUnselect?.Invoke(element); }

    public static event Action<float> OnTimeScaleChange;
    public static void InvokeTimeScaleChange(float newScale) { Debug.Log("⏱️TimeScaleChange " + newScale); OnTimeScaleChange?.Invoke(newScale); }

    public static event Action OnLanguageChange;
    public static void InvokeLanguageChange() { Debug.Log("Language changed for " + LanguageManager.currentLang); OnLanguageChange?.Invoke(); }

    // <-- Add new game events here

} // SCRIPT END
