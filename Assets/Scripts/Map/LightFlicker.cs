using UnityEngine;
using System.Collections;

//[RequireComponent(typeof(Light))]
public class LightFlicker : MonoBehaviour {

    public float minIntensity = 0.25f;
    public float maxIntensity = 0.1f;
    public Light[] light;

    float random;
    float randomTime;
    float intensity;
    bool flicker;
    Timer timer = new Timer();

    void Start() {
        flicker = true;
    }

    void Update() {
        if (flicker) {
            random = Random.Range(2.8f, 3.81f);
            for (int i = 0; i < light.Length; i++) {
                light[i].intensity = random;
            }
            flicker = false;
            randomTime = Random.Range(0.1f, 0.5f);
        }
        else {
            DelayFlick(randomTime);
        }
    }

    void DelayFlick(float delay) {
        if (timer.Run(delay)) {
            flicker = true;
        }
    }
}
