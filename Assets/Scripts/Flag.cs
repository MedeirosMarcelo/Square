using UnityEngine;
using System.Collections;

public class Flag : MonoBehaviour {

    public Runner Carrier { get; set; }
    public const float resetPositionCountdown = 7.5f;
    public Vector3 defaultPosition;
    Timer timer = new Timer();

    public Flag(){
        //defaultPosition = new Vector2(game.graphics.PreferredBackBufferWidth * 0.9f, game.graphics.PreferredBackBufferHeight * 0.5f);
        //transform.position = defaultPosition;
        //game.sceneControl.GetScene().flagList.Add(this);
    }

    void Start() {
        ResetPosition();
    }

    public void Update() {
        ResetCountdown();
    }

    public void PickUp(Runner runner) {
        Carrier = runner;
        runner.flag = this;
        gameObject.SetActive(false);
    }

    public void Drop() {
        if (Carrier != null) {
            transform.position = Carrier.transform.position;
            gameObject.SetActive(true);
            Carrier.BaseColor = Color.white;
            Carrier.flag = null;
            Carrier = null;
        }
    }

    public void ResetCountdown() {
        if (Carrier == null && transform.position != defaultPosition) {
            bool timerEnded;
            timer.TimerCounter(resetPositionCountdown, out timerEnded);
            if (timerEnded) {
                ResetPosition();
            }
        }
    }

    public void ResetPosition() {
        transform.position = defaultPosition;
       // gameObject.SetActive(true);
    }
}
