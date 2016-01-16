using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DebugInput : MonoBehaviour {
    List<BaseInput> inputs = new List<BaseInput>();

	void Start () {
        inputs.Add(new ControllerInput(ControllerId.One));
        inputs.Add(new ControllerInput(ControllerId.Two));
	}
	
	void Update () {
        foreach(var input in inputs) {
            input.Update();
        }
	}

    void FixedUpdate() {
        string msg = "Input debug:";
        foreach(var input in inputs) {
            msg += "\n " + input.name + ":"
                + "  H=" + input.horizontal
                + "  V=" + input.vertical
                + "  E=" + input.explode;
            input.FixedUpdate();
        }
        Debug.Log(msg);
    }
}
