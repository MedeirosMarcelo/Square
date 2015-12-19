using UnityEngine;
using System.Collections;

public enum CharacterState {
    Idle,
    Moving,
    Dead,
}

public class BaseCharacter : MonoBehaviour {
 
        public float maxSpeed_Walk = 15;
        public float acceleration_Walk = 0.1f;
        public float deceleration_Walk = 0.05f;
        public float maxSpeed_Run = 25;
        public float acceleration_Run = 0.1f;
        public float deceleration_Run = 0.05f;

        public bool canControl = true;
        public bool canMove = true;

        public BaseCharacter characterHit;
        public bool collided;
        public bool dead = false;

        public float respawnTime = 1.0f;

        public static Random random = new Random();
        public Vector2 defaultSpawnPosition = new Vector2(0.0f, 0.0f);

        Color baseColor;    
        Vector3 velocity;
        Vector3 size;
        Animator animator = new Animator();
        SceneController sceneControl;

        void Start() {
            Reset();
        }

        void Update() {
            Move();
        }

        public virtual void Reset() {
            characterHit = null;
            collided = false;
            dead = false;
            size = new Vector3(transform.position.x - transform.localScale.x * 0.5f, 1, transform.position.z - transform.localScale.z * 0.5f);
        }

        public void Respawn() {
            //position = SpawnPosition;
            Reset();
        }

        protected void Move() {
            Vector3 direction = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
            
            if (direction == Vector3.zero) {
                velocity.x = Mathf.Lerp(velocity.x, 0, deceleration_Walk);
                if (Mathf.Abs(velocity.x - 0) < 0.01f)
                    velocity.x = 0;

                velocity.z = Mathf.Lerp(velocity.z, 0, deceleration_Walk);
                if (Mathf.Abs(velocity.z - 0) < 0.01f)
                    velocity.z = 0;
            }
            else {
                velocity += direction * acceleration_Walk;
            }

            velocity = new Vector3(Mathf.Clamp(velocity.x, -1f, 1f), Mathf.Clamp(velocity.y, -1f, 1f), Mathf.Clamp(velocity.z, -1f, 1f));
            Vector3 newPosition = transform.position + (velocity * maxSpeed_Walk) * Time.deltaTime;
            
            //Clamp position to scene borders
            //transform.position = new Vector2(Mathf.Clamp(newPosition.x, -200 + size.x,
            //                                           200 - size.z),
            //                          Mathf.Clamp(newPosition.z, -200 + size.z,
            //                                           200 - size.z));
            transform.position = newPosition;
        }

        virtual public void Die(BaseCharacter killer) {
            dead = true;
        }

        public Color BaseColor {
            get {
                return baseColor;
            }
            set {
                baseColor = value;
                GetComponent<MeshRenderer>().material.color = baseColor;
            }
        }
    }

