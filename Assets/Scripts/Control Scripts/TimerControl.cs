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

        startTime = Time.time;
    }

    void Update() {
        totalElapsedTime = Time.time - startTime;
        totalElapsedMinutes = (int)totalElapsedTime / 60;
        totalElapsedSeconds = (int)totalElapsedTime % 60;
        totalElapsedTimeRounded = (int)totalElapsedTime;
    }
}