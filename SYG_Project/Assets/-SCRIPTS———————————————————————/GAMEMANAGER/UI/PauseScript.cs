using UnityEngine;
using Rewired;

public class PauseScript : MonoBehaviour
{
    public static bool Paused{get{return paused;}}
    static bool paused;
    [Range(0,0.1f)][SerializeField] float pauseTimeScale = 0;
    Player player;

    void Awake()
    {
        paused = false;
    }

    void OnEnable()
    {
        EVENTS.OnInitialization += ResetPauseSpeed;
        EVENTS.OnGamePauseExit += ResetPauseSpeed;
    }

    void OnDisable()
    {
        EVENTS.OnInitialization -= ResetPauseSpeed;
        EVENTS.OnGamePauseExit -= ResetPauseSpeed;
    }

    void ResetPauseSpeed()
    {
        TimeScaleManager.PauseSpeed = 1f;
    }


    void Start()
    {
        player = ReInput.players.GetPlayer(0);
    }

    void Update()
    {
        if(player.GetButtonDown("Pause")) TogglePauseMode();
    }

    public void TogglePauseMode()
    {
        switch (GAME.MANAGER.CurrentState)
        {
            case State.gameplay: Pause(); break;
            case State.paused: Resume(); break;
        }
    }

    public void Pause()
    {
        TimeScaleManager.PauseSpeed = pauseTimeScale;
        paused = true;
        GAME.MANAGER.SwitchTo(State.paused);
    }

    public void Resume()
    {
        paused = false;
        TimeScaleManager.PauseSpeed = 1f;
        GAME.MANAGER.SwitchTo(State.gameplay);
        EVENTS.InvokeGameResume();
    }

} // SCRIPT END
