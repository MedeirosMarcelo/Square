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
    EigthWays,
    EigthWaysNormalized,
    FourWays,
}


public class ControllerInput : BaseInput {
    public ControllerId id { get; private set; }
    public static ControllerMode mode;

    public ControllerInput(ControllerId id) {
        this.id = id;
        mode = ControllerMode.Smooth;
        name = "Controller " + id;
    }

    public override void Update() {
        horizontal = Input.GetAxis("HorizontalC" + (int)id);
        vertical = Input.GetAxis("VerticalC" + (int)id);
        explode |= Input.GetButtonDown("ExplodeC" + (int)id);
        ProcessInput();
    }

    public override void FixedUpdate() {
        explode = false;
    }

    private float axisThreshold = 0.45f;
    private void ProcessInput() {
        switch (mode) {
            default:
            case ControllerMode.Smooth:
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
