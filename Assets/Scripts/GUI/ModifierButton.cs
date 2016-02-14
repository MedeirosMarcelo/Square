    using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ModifierButton : MenuSelectable {

    public ModifierChoice modifierChoice;
    Modifier modifier;

    void Start() {
        modifierChoice = transform.parent.GetComponent<ModifierChoice>();
    }

	void Update () {
        if (selected) {
            if (Input.GetKeyDown(KeyCode.JoystickButton0) || Input.GetKeyDown(KeyCode.Space)){
                modifierChoice.SelectModifier(modifier);
            }
        }
	}

    public Modifier Modifier {
        get {
            return modifier;
        }
        set {
            modifier = value;
            GetComponent<Image>().sprite = modifier.icon;
        }
    }
}
