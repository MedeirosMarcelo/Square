using UnityEngine;
using System.Collections;

public class Magnetizer : Modifier {

    public float effectDistance = 15f;
    public float pullForce = 5f;
    GameManager gameManager;
    Vector3 velocity;

    void Start() {
        gameManager = GameObject.FindWithTag("Game Manager").GetComponent<GameManager>();
        base.Start();
    }

    void Update() {
        if (Active) {
            foreach (BaseCharacter character in gameManager.characterList) {
                if (character.type == CharacterType.Bomber) {
                    if (InsideAreaOfEffect(character)) {
                        Pull(character);
                    }
                }
            }
        }
    }

    void Pull(BaseCharacter target) {
        Vector3 direction = (Owner.transform.position - target.transform.position).normalized;
        float distance = Vector3.Distance(Owner.transform.position, target.transform.position);
        float speedByDistance;

        //if (direction == Vector3.zero) {
        //    velocity.x = Mathf.Lerp(velocity.x, 0, deceleration);
        //    if (Mathf.Abs(velocity.x - 0) < 0.01f)
        //        velocity.x = 0;

        //    velocity.z = Mathf.Lerp(velocity.z, 0, deceleration);
        //    if (Mathf.Abs(velocity.z - 0) < 0.01f)
        //        velocity.z = 0;
        //}
        //else {
        //    newAcceleration = acceleration * (distance / effectDistance);
        //    velocity += direction * acceleration;
        //}
        speedByDistance = 1 - (distance / effectDistance);
        Debug.Log(speedByDistance);
      //  velocity += direction * speedByDistance;
     //   target.publicVelocity = velocity;

      //  velocity = new Vector3(Mathf.Clamp(velocity.x, -1f, 1f), Mathf.Clamp(velocity.y, -1f, 1f), Mathf.Clamp(velocity.z, -1f, 1f));
       // Vector3 newPosition = transform.position + (velocity * pullForce) * Time.deltaTime;

    //    target.rigidbody.MovePosition(target.transform.position + direction * pullForce * Time.deltaTime);

        velocity += direction * 0.01f;
        target.rigidbody.AddForce(velocity, ForceMode.Impulse);
    }

    bool InsideAreaOfEffect(BaseCharacter obj) {
        if (Vector3.Distance(Owner.transform.position, obj.transform.position) < effectDistance) {
            return true;
        }
        return false;
    }
}
