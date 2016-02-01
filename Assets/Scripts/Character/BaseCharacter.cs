using UnityEngine;
using System.Collections;

public enum CharacterState {
    Alive,
    Dead
}

public enum CharacterType {
    Runner,
    Bomber
}

public class BaseCharacter : MonoBehaviour {

    public float maxSpeed = 15;
    public float acceleration = 0.1f;
    public float deceleration = 0.05f;

    public CharacterType type;
    public Player player;
    public BaseInput input;
    public bool canControl = true;
    public bool canMove = true;
    public Rigidbody rigidbody;
    public Vector3 publicVelocity;
    public CharacterState State { get; protected set; }
    public ControllerId id;

    protected float respawnDelay;
    protected GameManager gameManager;

    [SerializeField]
    Color baseColor = Color.white;

    Timer respawnTimer = new Timer();
    Animator animator = new Animator();
    GameObject model;
    Vector3 velocity;

    protected virtual void Start() {
        gameManager = GameObject.FindWithTag("Game Manager").GetComponent<GameManager>();
        rigidbody = GetComponent<Rigidbody>();
        model = transform.Find("Model").gameObject;
        Reset();
        if (player != null) {
            input = new ControllerInput(player.Controller);
        }
        else {
            input = new ControllerInput(ControllerId.One);
        }
        id = player.Controller;
    }

    public virtual void Reset() {
        model.SetActive(true);
        GetComponent<BoxCollider>().enabled = true;
        State = CharacterState.Alive;
    }

   protected void Move() {
        Vector3 direction = new Vector3(input.horizontal, 0, input.vertical);


        if (direction == Vector3.zero) {
            velocity.x = Mathf.Lerp(velocity.x, 0, deceleration);
            if (Mathf.Abs(velocity.x - 0) < 0.01f)
                velocity.x = 0;

            velocity.z = Mathf.Lerp(velocity.z, 0, deceleration);
            if (Mathf.Abs(velocity.z - 0) < 0.01f)
                velocity.z = 0;
        }
        else {
            velocity += direction * acceleration;
        }

        velocity = new Vector3(Mathf.Clamp(velocity.x, -1f, 1f), Mathf.Clamp(velocity.y, -1f, 1f), Mathf.Clamp(velocity.z, -1f, 1f));
        Vector3 newPosition = transform.position + (velocity * maxSpeed) * Time.deltaTime;

        rigidbody.MovePosition(newPosition);
        Forward();
    }

    protected void Forward() {
        var direction = new Vector2(input.horizontal, input.vertical);
        if (direction.magnitude < 0.75f) {
            return;
        }
        if (Mathf.Abs(input.horizontal) >=  Mathf.Abs(input.vertical)) {
            transform.forward = new Vector3(input.horizontal, 0f, 0f);
        } else {
            transform.forward = new Vector3(0f, 0f, input.vertical);
        }
    }
 
    protected void StartRespawn() {
        if (respawnTimer.Run(respawnDelay)) {
            Respawn();
        }
    }

    public void Respawn() {
        if (gameManager.State == GameState.Play) {
           // Reset();
           // transform.position = gameManager.currentMap.GetSpawnPosition(type);
            gameManager.SpawnPlayer(player, type, gameManager.currentMap.GetSpawnPosition(type));
            Destroy(this.gameObject);
        }
    }

    virtual public void Die(string killerTag) {
        model.SetActive(false);
        GetComponent<BoxCollider>().enabled = false;
        rigidbody.velocity = Vector3.zero;
        State = CharacterState.Dead;
    }

    public Color BaseColor {
        get {
            return baseColor;
        }
        set {
            baseColor = value;
            //model.GetComponent<SkinnedMeshRenderer>().material.color = baseColor;
        }
    }
}
