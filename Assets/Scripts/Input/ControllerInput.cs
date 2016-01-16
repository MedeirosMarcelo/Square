using UnityEngine;
using System.Collections;

public enum Controller : int {
    One = 1,
    Two = 2,
    Three = 3,
    Four = 4,
    Computer = 0
}

public class ControllerInput : BaseInput {
    // private input data
    private float _horizontal = 0f;
    private float _vertical = 0f;
    private bool _explode = false;

    // public input proprieties
    public override float horizontal {
        get {
            CheckUpdated();
            return _horizontal;
        }
        protected set {
            _horizontal = value;
        }
    }
    public override float vertical {
        get {
            CheckUpdated();
            return _vertical;
        }
        protected set { _vertical = value; }
    }
    public override bool explode {
        get {
            CheckUpdated();
            return _explode;
        }
        protected set { _explode = value; }
    }

    private bool updated = false;
    private void CheckUpdated() {
        if (!updated) {
            Debug.Log("Input read before updated");
        }
    }

    // config controller to be read
    public Controller index = Controller.One;

    public override void Update() {
        horizontal = Input.GetAxis("HorizontalC" + (int)index);
        vertical = Input.GetAxis("VerticalC" + (int)index);
        explode |= Input.GetButtonDown("ExplodeC" + (int)index);
        updated = true;
    }

    public override void FixedUpdate() {
        updated = false;
    }
}
