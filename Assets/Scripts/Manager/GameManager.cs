using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public enum GameState {
    Loading,
    Playing,
    End
}

public class GameManager : MonoBehaviour {

    public IList<BaseCharacter> characterList = new List<BaseCharacter>();
    public byte maxScore = 5;
    public float roundClock;
    public Map currentMap;
    public GameState State { get; private set; }

    [SerializeField]
    GameObject runnerPrefab;
    [SerializeField]
    GameObject bomberPrefab;

    Timer timer = new Timer();

    void Awake() {
        State = GameState.Loading;
        LoadPlayers();
        State = GameState.Playing;
    }

    void Update() {
        RoundTimer();
    }

    void LoadPlayers() {
        int r = 0;
        int b = 0;
        int i = 0;
        Vector3 position;
        CharacterType type;
        MatchData.RotateCharacters();
        foreach (Player pl in PlayerManager.GetPlayerList()) {
            type = MatchData.CharacterRotation.Peek();
            if (type == CharacterType.Runner) { i = r; r++; }
            if (type == CharacterType.Bomber) { i = b; b++; }
            position = currentMap.GetSpawnPosition(type, i);
            SpawnPlayer(pl, type, position);
            MatchData.RotateCharacters();
        }
    }

    public void Score(ControllerId controller) {
        MatchData.PlayerScore[controller] += 1;
        Debug.Log(MatchData.PlayerScore[controller]);
        Debug.Log(controller);
        if (MatchData.PlayerScore[controller] < maxScore) {
            StartNextRound();
        }
        else {
            EndMatch();
        }
    }

    void RoundTimer() {
        if (timer.Run(currentMap.clockTime)) {
            StartNextRound();
        }
        roundClock = timer.GetTimeDecreasing();
    }

    public void StartNextRound() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void EndMatch() {
        State = GameState.End;
        SceneManager.LoadScene("ResultScreen");
    }

    public void SpawnPlayer(Player player, CharacterType type, Vector3 position) {
        GameObject pl = (GameObject)Instantiate(GetCharacterPrefab(type), position, transform.rotation);
        pl.GetComponent<BaseCharacter>().player = player;
        player.Character = pl.GetComponent<BaseCharacter>();
        characterList.Add(player.Character);
    }

    GameObject GetCharacterPrefab(CharacterType type) {
        switch (type) {
            default:
            case CharacterType.Runner:
                return runnerPrefab;
            case CharacterType.Bomber:
                return bomberPrefab;
        }
    }
}
