using UnityEngine;
using System.Collections;

public class BomberAI : MonoBehaviour {

    public BaseCharacter target;

    BaseCharacter baseCharacter;
    Vector3 velocity;
    Rigidbody rigidbody;

	void Start () {
        baseCharacter = GetComponent<BaseCharacter>();
        rigidbody = GetComponent<Rigidbody>();
        baseCharacter.canControl = false;
	}
	
	void Update () {
        Seek();
	}

    void Seek() {
        Vector3 direction = (target.transform.position - transform.position).normalized;

        if (direction == Vector3.zero) {
            velocity.x = Mathf.Lerp(velocity.x, 0, baseCharacter.deceleration);
            if (Mathf.Abs(velocity.x - 0) < 0.01f)
                velocity.x = 0;

            velocity.z = Mathf.Lerp(velocity.z, 0, baseCharacter.deceleration);
            if (Mathf.Abs(velocity.z - 0) < 0.01f)
                velocity.z = 0;
        }
        else {
            velocity += direction * baseCharacter.acceleration;
        }

        velocity = new Vector3(Mathf.Clamp(velocity.x, -1f, 1f), Mathf.Clamp(velocity.y, -1f, 1f), Mathf.Clamp(velocity.z, -1f, 1f));
        Vector3 newPosition = transform.position + (velocity * baseCharacter.maxSpeed) * Time.deltaTime;

        rigidbody.MovePosition(newPosition);
    }
}
