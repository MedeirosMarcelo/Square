using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    public static MatchData Match = new MatchData();
    public byte maxScore = 5;
    public float roundClock;

    [SerializeField] GameObject runnerPrefab;
    [SerializeField] GameObject bomberPrefab;

    Timer timer = new Timer();

    void Awake() {
        AddPlayer(0, runnerPrefab, new Vector3(-7.66f, 1f, 0f));
        AddPlayer(1, bomberPrefab, new Vector3(8.53f, 1f, 3.79f));
        AddPlayer(2, bomberPrefab, new Vector3(8.53f, 1f, 0f));
        AddPlayer(3, bomberPrefab, new Vector3(8.53f, 1f, -3.79f));
    }

    void Update() {
        RoundTimer();
    }

    public void Score(byte playerNumber) {
        Match.PlayerScore[playerNumber] += 1;
        if (Match.PlayerScore[playerNumber] < maxScore) {
            StartNextRound();
        }
        else {
            EndMatch();
        }
    }

    void StartNextRound(){
        Application.LoadLevel(Application.loadedLevel);
    }

    void EndMatch() {
        Application.LoadLevel("ResultScreen");
    }

    void AddPlayer(byte playerNumber, GameObject prefab, Vector3 position) {
        if (playerNumber < 4) {
            GameObject player = (GameObject)Instantiate(prefab, position, transform.rotation);
            player.GetComponent<BaseCharacter>().playerNumber = playerNumber;
            Match.PlayerList.Insert(playerNumber, player.GetComponent<BaseCharacter>());
        }
        else {
            Debug.LogError("playerNumber can only be from 0 to 3 - AddPlayer");
        }
    }

    void RoundTimer() {
        bool timerEnded;
        timer.TimerCounter(60f, out timerEnded);
        if (timerEnded) {
            Application.LoadLevel(Application.loadedLevel);
        }
        roundClock = timer.GetTimeDecreasing();
    }

    void SpawnModifiers (){


    }
}
