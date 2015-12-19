using UnityEngine;
using System.Collections;

public class Flag : MonoBehaviour {

    public Runner Carrier { get; set; }
    public const float resetPositionCountdown = 7.5f;
    Vector2 defaultPosition;
    Timer timer = new Timer();

    public Flag(){
        //defaultPosition = new Vector2(game.graphics.PreferredBackBufferWidth * 0.9f, game.graphics.PreferredBackBufferHeight * 0.5f);
        //transform.position = defaultPosition;
        //game.sceneControl.GetScene().flagList.Add(this);
    }

    public void Update() {
        ResetCountdown();
        DropIfDeadCarrier();
    }

    void DropIfDeadCarrier() {
        if (Carrier != null && Carrier.dead) {
            Drop();
        }
    }

    public void PickUp(Runner runner) {
        Carrier = runner;
        runner.flag = this;
    }

    public void Drop() {
        if (Carrier != null) {
            transform.position = Carrier.transform.position;
            Carrier.BaseColor = Color.white;
            Carrier.flag = null;
            Carrier = null;
        }
    }

    public void ResetCountdown() {
        bool timerEnded;
        timer.TimerCounter(resetPositionCountdown, out timerEnded);
        if (timerEnded) {
            ResetPosition();
        }
    }

    public void ResetPosition() {
        transform.position = defaultPosition;
    }
}
