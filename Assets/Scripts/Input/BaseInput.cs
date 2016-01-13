using UnityEngine;
using System.Collections;

public class BaseInput {
    public virtual float horizontal { get; protected set; }
    public virtual float vertical { get; protected set; }
    public virtual bool explode { get; protected set; }

    public BaseInput() {
        horizontal = 0.0f;
        vertical = 0.0f;
        explode = false;
    }

    // must be called each Update before consuming any input.
    public virtual void Update() {
    }

    // must be called each FixedUpdate after consuming all input;
    public virtual void FixedUpdate() {
    }
}
