using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MenuSlider : MenuSelectable {

    public Slider slider;

	void Update () {
        if (selected) {
            if (Input.GetKeyDown(KeyCode.RightArrow)) {
                slider.value += 0.05f;
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow)) {
                slider.value -= 0.05f;
            }
        }
	}
}
