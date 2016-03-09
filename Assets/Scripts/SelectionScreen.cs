using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SelectionScreen : MonoBehaviour {

    public float clockLength = 5f;
    IList<CharacterPanel> playerPanel;
    GameObject startCounter;
    [SerializeField] Text clockText;
    float clock;
    Timer timer;
    bool showClock;

    void Start() {
        playerPanel = new List<CharacterPanel>(GetComponentsInChildren<CharacterPanel>());
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

        int active = 0;
        int confirmed = 0;
        foreach (CharacterPanel panel in playerPanel) {
            if (panel.isActive) {
                active++;
            }
            if (panel.isReady) {
                confirmed++;
            }
        }

        // Start timer if at least one is active and all confirmed
        showClock = (active > 0 && active == confirmed);

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


        foreach (var p in PlayerManager.GetPlayerList()) {
            Debug.Log(p.Name + " color: " + p.colorMaterial);
        }

        SceneManager.LoadScene("Prototype v1.0");
    }
}
