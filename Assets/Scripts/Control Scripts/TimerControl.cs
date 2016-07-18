using UnityEngine;
using System.Collections;

public class TimerControl : MonoBehaviour {

    public static TimerControl timerControl;
    public float startTime;
    public float totalElapsedTime;
    public int totalElapsedMinutes;
    public int totalElapsedSeconds;
    public int totalElapsedTimeRounded;

    void Awake() {
        if (timerControl == null) {
            timerControl = this;
        } else if (timerControl != this) {
            Destroy(gameObject);
        }

        startTime = Time.time;  //get total time since start of scene.  Might need to be re-worked for loading games.  Maybe store the value and fetch it here for when game is loaded?
    }

    void Update() {
        totalElapsedTime = Time.time - startTime;  //time elapsed since start of game
        totalElapsedMinutes = (int)totalElapsedTime / 60;  //minutes elapsed (for UI display purposes)
        totalElapsedSeconds = (int)totalElapsedTime % 60;  //seconds elapsed (for UI display purposes)
        totalElapsedTimeRounded = (int)totalElapsedTime; //same as totalElapsedTime but the fractional seconds will be rounded down/off (ie: 23.897 becomes 23)
    }
}