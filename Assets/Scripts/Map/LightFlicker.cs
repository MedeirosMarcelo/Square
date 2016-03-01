using UnityEngine;
using System.Collections;

public class LightFlicker : MonoBehaviour {

    public Light light;
    public float amount = -0.15f;
    public float speed = 1.88f;
    private Color originalColor;
    private float timePassed;
    private float changeValue;

    void Start() {

        if (light != null) {
            originalColor = light.color;
        }
        else {
            enabled = true;
            return;
        }

        changeValue = 0;
        timePassed = 0;
    }

    void Update() {
        timePassed = Time.time;
        timePassed = timePassed - Mathf.Floor(timePassed);

        light.color = originalColor * CalculateChange();
    }

    float CalculateChange() {
        changeValue = -Mathf.Sin(timePassed * 12 * Mathf.PI) * amount + speed;
        return changeValue;
    }
}
