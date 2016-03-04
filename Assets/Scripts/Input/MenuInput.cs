using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class MenuInput {

    // Timeout for axis to generate a new imput
    static readonly float axisTimeOut = 0.5f;

    // Generic read axis for Vertical and Horizontal
    static int ReadAxis(string name, ref float timeout) {
        var raw = Input.GetAxis(name);

        if (Mathf.Abs(raw) < 0.5f) {
            // If axis was realeased clear the timeout
            timeout = 0f;
            return 0;
        }

        // Check if we are waiting for a timeout
        if (timeout > 0f) {
            timeout -= Time.deltaTime;
            if (timeout > 0f) {
                return 0;
            }
        }

        // Timeout expired we will return value and reload timeout
        timeout = axisTimeOut;
        var value = (int)Mathf.Sign(raw);
        Debug.Log(name + " = " + value);
        return value;
    }

    // Horizontal input with timeout
    static float hTimeOut = axisTimeOut;
    public static int horizontal {
        get {
            return ReadAxis("Horizontal", ref hTimeOut);
        }
    }

    // Vertical input with timeout
    static float vTimeOut = axisTimeOut;
    public static int vertical {
        get {
            return ReadAxis("Vertical", ref vTimeOut);
        }
    }

    public static bool submit {
        get {
            var res = Input.GetKeyDown(KeyCode.JoystickButton0) || Input.GetKeyDown(KeyCode.Return);
            if (res) {
                Debug.Log("Submit Pressed");
            }
            return res;
        }
    }

    public static bool cancel {
        get {
            var res = Input.GetKeyDown(KeyCode.JoystickButton1) || Input.GetKeyDown(KeyCode.Backspace); ;
            if (res) {
                Debug.Log("Cancel Pressed");
            }
            return res;
        }
    }
}