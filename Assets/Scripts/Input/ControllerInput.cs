using UnityEngine;
using System.Collections;

public enum ControllerId : int {
    One = 1,
    Two = 2,
    Three = 3,
    Four = 4,
}

public class ControllerInput : BaseInput {
    public ControllerId id;
    public ControllerInput(ControllerId id) {
        this.id = id;
        name = "Controller " + id;
    }

    public override void Update() {
        horizontal = Input.GetAxis("HorizontalC" + (int)id);
        vertical = Input.GetAxis("VerticalC" + (int)id);
        explode |= Input.GetButtonDown("ExplodeC" + (int)id);
    }

    public override void FixedUpdate() {
        explode = false;
    }
}
