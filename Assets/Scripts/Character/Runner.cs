using UnityEngine;
using System.Collections;

public class Runner : BaseCharacter {

    bool previousButton1State;
    public Flag flag;
    bool CanPickFlag { get; set; }
    CharacterState state;

    void Update() {
        StateMachine();
    }

    protected void StateMachine() {
        switch (state) {
            default:
            case CharacterState.Alive:
                if (canControl && canMove) {
                    Move();
                }
                CanPickFlag = true;
                break;
            case CharacterState.Dead:
                break;
        }
    }

    protected void EnterState(CharacterState newState) {
        CharacterState previoState = newState;
        state = newState;

        switch (state) {
            default:
            case CharacterState.Alive:
                Move();
                break;
            case CharacterState.Dead:
             //   StartRespawn();
                CanPickFlag = false;
                break;
        }
    }

    void PickUpFlag(Flag flag) {
        flag.PickUp(this);
        BaseColor = Color.green;//new Color(172, 225, 50, 255);
        Debug.Log("pickup");
    }

    void DropFlag() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            if (flag != null) {
                flag.Drop();
            }
        }
    }

    void OnTriggerEnter(Collider col) {
        if (CanPickFlag) {
            if (col.tag == "Flag") {
                Debug.Log(col.gameObject);
                PickUpFlag(col.gameObject.GetComponent<Flag>());
            }
        }

        if (flag != null && col.name == "Finish Line") {
            Score();
        }
    }

    void Score() {
        gameManager.Score += 1;
        Application.LoadLevel(Application.loadedLevel);
        //flag.ResetPosition();
    }

    void OnCollisionEnter(Collision col) {
        if (col.gameObject.name == "Explosion") {
            Die(col.gameObject.tag);
        }
    }
}
