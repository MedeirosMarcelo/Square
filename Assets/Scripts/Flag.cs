using UnityEngine;
using System.Collections;

public class Flag : MonoBehaviour {

    public Runner Carrier { get; set; }
    public const float resetPositionCountdown = 7.5f;
    public Vector3 defaultPosition;
    Timer timer = new Timer();

    void Start() {
        defaultPosition = transform.position;
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
    }
}
