using UnityEngine;
using System.Collections;

public class Bomber : BaseCharacter {

    public static Color bomberColor = Color.white;
    public static Color explodingColor = Color.red;
    public Explosion explosion;

    bool explosionTriggered;
    const float explosionTime = 1f;
    Timer explosionTimer;
    Timer triggerTimer;

    protected override void Start() {
        base.Start();
        explosion = transform.Find("Explosion").GetComponent<Explosion>();
        name = "Bomber";
        type = CharacterType.Bomber;
        respawnDelay = gameManager.currentMap.bomberRespawnDelay;
    }

    void Update() {
        input.Update();
        StateMachine();
        input.FixedUpdate();
    }

    protected void StateMachine() {
        switch (State) {
            default:
            case CharacterState.Alive:
                if (canControl && canMove) {
                    Move();
                    ControlInput();
                }
                break;
            case CharacterState.Dead:
                StartRespawn();
                break;
        }
    }

    protected void EnterState(CharacterState newState) {
        CharacterState previoState = newState;
        State = newState;

        switch (State) {
            default:
            case CharacterState.Alive:
                break;
            case CharacterState.Dead:
                break;
        }
    }

    public override void Reset() {
        explosionTriggered = false;
        triggerTimer = new Timer();
        explosionTimer = new Timer();
        BaseColor = bomberColor;
        base.Reset();
    }

    void ControlInput() {
        if (input.explode) {
            explosion.TriggerBomb(this);
        }
    }

    public override void Die(string killerTag) {
        if (killerTag == "Explosion" || killerTag == "Bomber") {
            explosion.Explode();
        }
        EnterState(CharacterState.Dead);
        base.Die(killerTag);
    }

    void ReturnFlag(Flag flag) {
        flag.ResetPosition();
    }

    void OnCollisionEnter(Collision col) {
        if (col.gameObject.tag == "Explosion" ||
            col.gameObject.tag == "Runner" ||
            col.gameObject.tag == "Bomber") {
            Die(col.gameObject.tag);
        }
    }
}
