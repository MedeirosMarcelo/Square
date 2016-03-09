using UnityEngine;
using System.Collections;

public enum ControllerId : int {
    One = 1,
    Two = 2,
    Three = 3,
    Four = 4
}

public enum ControllerMode {
    Smooth,
    SmoothThreshold,
    EigthWays,
    EigthWaysNormalized,
    FourWays
}



public class ControllerInput : BaseInput {
    public ControllerId id { get; private set; }
    public static ControllerMode mode = ControllerMode.SmoothThreshold;

    MenuAxis menuHorizontalAxis= new MenuAxis();
    MenuAxis menuVerticalAxis = new MenuAxis();

    public ControllerInput(ControllerId id) {
        this.id = id;
        name = "Controller " + id;
    }

    public override void Update() {
        horizontal = Input.GetAxis("HorizontalC" + (int)id);
        vertical = Input.GetAxis("VerticalC" + (int)id);
        explode |= Input.GetButtonDown("ExplodeC" + (int)id);

        menuHorizontal = menuHorizontalAxis.Update(horizontal);
        menuVertical = menuVerticalAxis.Update(vertical);
        menuSubmit = Input.GetKeyDown("joystick " + (int)id + " button 0");
        menuCancel = Input.GetKeyDown("joystick " + (int)id + " button 1");

        ProcessInput();
    }

    public override void FixedUpdate() {
        explode = false;
    }

    private float axisThreshold = 0.55f;
    private void ProcessInput() {
        switch (mode) {
            default:
            case ControllerMode.Smooth:
                break;
            case ControllerMode.SmoothThreshold:
                horizontal = (Mathf.Abs(horizontal) < axisThreshold) ? 0f : horizontal;
                vertical = (Mathf.Abs(vertical) < axisThreshold) ? 0f : vertical;
                break;

            case ControllerMode.EigthWays:
                horizontal = (Mathf.Abs(horizontal) < axisThreshold) ? 0f : Mathf.Sign(horizontal);
                vertical = (Mathf.Abs(vertical) < axisThreshold) ? 0f : Mathf.Sign(vertical);
                break;

            case ControllerMode.EigthWaysNormalized:
                horizontal = (Mathf.Abs(horizontal) < axisThreshold) ? 0f : Mathf.Sign(horizontal);
                vertical = (Mathf.Abs(vertical) < axisThreshold) ? 0f : Mathf.Sign(vertical);
                if ((horizontal != 0) && (vertical != 0)) {
                    horizontal *= Mathf.Sqrt(2);
                    vertical *= Mathf.Sqrt(2);
                }
                break;

            case ControllerMode.FourWays:
                if ((Mathf.Abs(horizontal) < axisThreshold) && (Mathf.Abs(vertical) < axisThreshold)) {
                    horizontal = 0f;
                    vertical = 0f;
                }
                else { // at least one axis has valid input
                    if (Mathf.Abs(horizontal) > Mathf.Abs(vertical)) {
                        horizontal = Mathf.Sign(horizontal);
                        vertical = 0f;
                    }
                    else {
                        vertical = Mathf.Sign(vertical);
                        horizontal = 0f;
                    }
                }
                break;
        }
    }
}

public class MenuAxis {
    // Timeout for axis to generate a new imput
    static readonly float axisTimeOut = 0.5f;

    private float menuThreshold = 0.55f;
    private float timeout = axisTimeOut;

    public int Update(float raw) {

        if (Mathf.Abs(raw) < menuThreshold) {
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
        return value;
    }
}


