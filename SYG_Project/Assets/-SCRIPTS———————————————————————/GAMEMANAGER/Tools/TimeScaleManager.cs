using UnityEngine;

public class TimeScaleManager
{
    public static float GameSpeed {get{return gameSpeed;} set{gameSpeed = value; SetTimeScale();}}
    public static float FreezeFrameSpeed {get{return freezeFrameSpeed;} set{freezeFrameSpeed = value; SetTimeScale();}}
    public static float PauseSpeed {get{return pauseSpeed;} set{pauseSpeed = value; SetTimeScale();}}
    
    // USE THESE ↑ TO CHANGE TIMESCALE MULTIPLIERS "GAMEPLAY SPEED", "PAUSE SPEED" AND "FREEZE SPEED"

    public static float GameTimeScale{get{return gameSpeed*pauseSpeed;}} // Read-only



    // INTERNAL - DO NOT USE FROM OUTSIDE
    static float gameSpeed = 1f;
    static float freezeFrameSpeed=1f;
    static float pauseSpeed = 1f;

    static void SetTimeScale() 
    {
        Time.timeScale = GameTimeScale*freezeFrameSpeed;
        EVENTS.InvokeTimeScaleChange(GameTimeScale);
    }
} // SCRIPT END
