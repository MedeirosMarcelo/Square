using UnityEngine;
using System.Collections;

public class Runner : BaseCharacter {

    bool previousButton1State;
    public Flag flag;
    bool CanPickFlag { get; set; }

    void Update() {
        if (dead) {
            //UpdateRespawn(gametime);
            CanPickFlag = false;
        }
        else {
            if (canControl && canMove) {
                //UpdateMovement(gametime);
            }
            Move();
            CanPickFlag = true;
            DropFlag();
            if (characterHit != null) {
                characterHit.Die(this);
            }
        }
    }

    void PickUpFlag(Flag flag) {
        flag.PickUp(this);
        BaseColor = new Color(172, 225, 50, 255);
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
    }
}
