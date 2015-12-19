using UnityEngine;
using System.Collections;

public class Timer {

    float elapsedTime = 0;
    float length;

    public void TimerCounter(float length, out bool ended) {
        this.length = length;
        if (length > 0) {
            if (elapsedTime < length) {
                elapsedTime += Time.deltaTime;
                ended = false;
            }
            else {
                elapsedTime = 0;
                ended = true;
            }
            //       System.Diagnostics.Debug.WriteLine("Timer: " + elapsedTime);
        }
        else {
            ended = false;
        }
    }

    public float GetTimeIncreasing() {
        return elapsedTime;
    }

    public float GetTimeDecreasing() {
        return length - elapsedTime;
    }
}
