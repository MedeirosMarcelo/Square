using UnityEngine;
using System.Collections;

public class Shrinker : Modifier {
  
    public float scale = 0.5f;
    public float speedBonus = 10f;
    float oldSpeed;
    bool activated = false;
    Timer time = new Timer();

    void Start() {
        oldSpeed = Owner.maxSpeed;
    }

	void Update () {
        if (Active) {
            Shrink();
        }
	}

    void Shrink() {
        if (activated == false) {
            Owner.transform.localScale = new Vector3(scale, scale, scale);
            Owner.maxSpeed += speedBonus;
            activated = true;
        }
        else {
            EndEffect();
        }
    }

    void EndEffect() {
        if (time.Run(duration)) {
            Owner.transform.localScale = new Vector3(1f, 1f, 1f);
            Owner.maxSpeed = oldSpeed;
            Destroy(this.gameObject);
        }
    }
}
