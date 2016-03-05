using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SelectionScreen : MonoBehaviour {

    public float clockLength = 5f;
    IList<CharacterSelection> playerPanel;
    GameObject startCounter;
    [SerializeField] Text clockText;
    float clock;
    Timer timer;
    bool showClock;

    void Start() {
        playerPanel = new List<CharacterSelection>(GetComponentsInChildren<CharacterSelection>());
        startCounter = transform.Find("Start Counter").gameObject;
        timer = new Timer();
    }

    void Update() {
        CheckConfirmedSelection();
        if (showClock) {
            ShowClock();
            RunSelectionTimer();
            OverrideToStart();
        }
    }

    void CheckConfirmedSelection() {
        showClock = false;
        foreach (CharacterSelection panel in playerPanel) {
            if (panel.confirmed) {
                showClock = true;
            }
        }
        if (!showClock) {
            HideClock();
        }
    }

    void ShowClock() {
        startCounter.SetActive(true);
        clockText.text = clock.ToString();
    }

    void HideClock() {
        startCounter.SetActive(false);
        timer = new Timer();
    }

    void RunSelectionTimer() {
        if (timer.Run(clockLength)) {
            StartGame();
        }
        clock = (int)timer.GetTimeDecreasing() + 1;
    }

    void OverrideToStart() {
        if (Input.GetKeyDown(KeyCode.Return)) {
            StartGame();
        }
    }

    void StartGame() {
        SceneManager.LoadScene("Prototype v1.0");
    }
}
