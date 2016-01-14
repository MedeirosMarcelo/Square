using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

    public IList<Player> playerList = new List<Player>();
    public static MatchData Match = new MatchData();
    public byte maxScore = 5;
    public float roundClock;

    [SerializeField]
    GameObject runnerPrefab;
    [SerializeField]
    GameObject bomberPrefab;

    Timer timer = new Timer();

    void Awake() {

        Player player1 = new Player(0, "A", runnerPrefab);
        Player player2 = new Player(1, "B", bomberPrefab);
        Player player3 = new Player(2, "C", bomberPrefab);
        Player player4 = new Player(3, "D", bomberPrefab);

        playerList.Add(player1);
        playerList.Add(player2);
        playerList.Add(player3);
        playerList.Add(player4);

        AddPlayer(player1, runnerPrefab, new Vector3(-7.66f, 1f, 0f));
        AddPlayer(player2, bomberPrefab, new Vector3(8.53f, 1f, 3.79f));
        AddPlayer(player3, bomberPrefab, new Vector3(8.53f, 1f, 0f));
        AddPlayer(player4, bomberPrefab, new Vector3(8.53f, 1f, -3.79f));
    }

    void Update() {
        RoundTimer();
    }

    public void Score(int playerNumber) {
        Match.PlayerScore[playerNumber] += 1;
        if (Match.PlayerScore[playerNumber] < maxScore) {
            StartNextRound();
        }
        else {
            EndMatch();
        }
    }

    void StartNextRound() {
        Application.LoadLevel(Application.loadedLevel);
    }

    void EndMatch() {
        Application.LoadLevel("ResultScreen");
    }

    void AddPlayer(Player player, GameObject prefab, Vector3 position) {
        GameObject pl = (GameObject)Instantiate(prefab, position, transform.rotation);
        Match.CharacterList.Insert(player.Number, pl.GetComponent<BaseCharacter>());
    }

    void RoundTimer() {
        bool timerEnded;
        timer.TimerCounter(60f, out timerEnded);
        if (timerEnded) {
            Application.LoadLevel(Application.loadedLevel);
        }
        roundClock = timer.GetTimeDecreasing();
    }

    void RotateCharacters() {

    }
}
