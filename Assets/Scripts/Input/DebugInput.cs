using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DebugInput : MonoBehaviour {
    public bool logValues = false;
    public List<BaseInput> inputs = new List<BaseInput>();

    void Start() {
        inputs.Add(new ControllerInput(ControllerId.One));
        inputs.Add(new ControllerInput(ControllerId.Two));
        inputs.Add(new ControllerInput(ControllerId.Three));
        inputs.Add(new ControllerInput(ControllerId.Four));
    }

    void Update() {
        UpdatePrint();
        UpdateMode();
        if (logValues) {
            foreach (var input in inputs) {
                input.Update();
                Debug.Log(input.DebugMsg());
            }
        }
    }

    void FixedUpdate() {
        if (logValues) {
            foreach (var input in inputs) {
                input.FixedUpdate();
            }
        }
    }

    public void UpdatePrint() {
        if (Input.GetKeyDown(KeyCode.F12)) {
            logValues = !logValues;
        }
    }

    public void UpdateMode() {
        if (Input.GetKeyDown(KeyCode.F11)) {
            switch (ControllerInput.mode) {
                default:
                case ControllerMode.Smooth:
                    ControllerInput.mode = ControllerMode.EigthWays;
                    break;
                case ControllerMode.EigthWays:
                    ControllerInput.mode = ControllerMode.EigthWaysNormalized;
                    break;
                case ControllerMode.EigthWaysNormalized:
                    ControllerInput.mode = ControllerMode.FourWays;
                    break;
                case ControllerMode.FourWays:
                    ControllerInput.mode = ControllerMode.Smooth;
                    break;
            }
            Debug.Log("ControllerInput mode=" + ControllerInput.mode);
        }
    }



}
