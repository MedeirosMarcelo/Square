using UnityEngine;
using System.Collections;

public class Runner : BaseCharacter {

    public Flag flag;
    public Material colorMaterial;
    bool CanPickFlag { get; set; }

    protected override void Start() {
        base.Start();
        name = "Runner";
        type = CharacterType.Runner;
        transform.Find("Model").Find("Runner").GetComponent<SkinnedMeshRenderer>().material = player.colorMaterial;
        respawnDelay = gameManager.currentMap.runnerRespawnDelay;
    }

    void Update() {
        input.Update();
        StateMachine();
        input.FixedUpdate();
        base.Update();
    }

    protected void StateMachine() {
        switch (State) {
            default:
            case CharacterState.Alive:
                if (canControl && canMove) {
                    Move();
                }
                CanPickFlag = true;
                break;
            case CharacterState.Dead:
                WaitRespawn();
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
                CanPickFlag = false;
                break;
        }
    }

    void PickUpFlag(Flag flag) {
        flag.PickUp(this);
        BaseColor = Color.green;//new Color(172, 225, 50, 255);
    }

    void DropFlag() {
        if (flag != null) {
            flag.Drop();
        }
    }

    void OnTriggerEnter(Collider col) {
        if (CanPickFlag) {
            if (col.tag == "Flag") {
                PickUpFlag(col.gameObject.GetComponent<Flag>());
            }
        }

        if (flag != null && col.name == "Finish Line") {
            Score();
        }
    }

    void Score() {
        gameManager.Score(player.Controller);
    }

    void OnCollisionEnter(Collision col) {
        if (col.gameObject.name == "Explosion") {
            Die(col.gameObject.tag);
        }
    }

    public override void Die(string killerTag) {
        if (State != CharacterState.Dead) {
            DropFlag();
            EnterState(CharacterState.Dead);
            base.Die(killerTag);
        }
    }
}