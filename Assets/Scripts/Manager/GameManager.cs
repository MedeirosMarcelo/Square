using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

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

        //AddPlayer(player1, runnerPrefab, new Vector3(-7.66f, 1f, 0f));
        //AddPlayer(player2, bomberPrefab, new Vector3(8.53f, 1f, 3.79f));
        //AddPlayer(player3, bomberPrefab, new Vector3(8.53f, 1f, 0f));
        //AddPlayer(player4, bomberPrefab, new Vector3(8.53f, 1f, -3.79f));

        //----- Move this to player selection screen.
        //PlayerManager.AddPlayer(Controller.One, "");
        //PlayerManager.AddPlayer(Controller.Two, "");
        //PlayerManager.AddPlayer(Controller.Three, "");
        //PlayerManager.AddPlayer(Controller.Four, "");
        LoadPlayers();
    }

    void LoadPlayers() {
        int i = 1;
        foreach (Player pl in PlayerManager.GetPlayerList()) {
            GameObject prefab;
            if (Match.GetCharacterInRotation() == CharacterType.Runner) {
                prefab = runnerPrefab;
            }
            else {
                prefab = bomberPrefab;
            }
            pl.Character = prefab;
            SpawnPlayer(pl, new Vector3(-7.66f * i, 1f, 0f));
            i++;
        }
    }

    void Update() {
        RoundTimer();
    }

    public void Score(Controller controller) {
        Debug.Log(Match.PlayerScore[controller]);
        Match.PlayerScore[controller] += 1;
        if (Match.PlayerScore[controller] < maxScore) {
            StartNextRound();
        }
        else {
            EndMatch();
        }
    }

    void StartNextRound() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void EndMatch() {
        SceneManager.LoadScene("ResultScreen");
    }

    void SpawnPlayer(Player player, Vector3 position) {
        GameObject pl = (GameObject)Instantiate(player.Character, position, transform.rotation);
        pl.GetComponent<BaseCharacter>().player = player;
    }

    void RoundTimer() {
        bool timerEnded;
        timer.TimerCounter(60f, out timerEnded);
        if (timerEnded) {
            StartNextRound();
        }
        roundClock = timer.GetTimeDecreasing();
    }
}
