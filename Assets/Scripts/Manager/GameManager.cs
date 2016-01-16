﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public enum GameState {
    Loading,
    Playing,
    End
}

public class GameManager : MonoBehaviour {

    public IList<Player> playerList = new List<Player>();
    public static MatchData Match = new MatchData();
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
        Vector3 position;
        CharacterType type;
        int r = 0;
        int b = 0;
        int i = 0;
        foreach (Player pl in PlayerManager.GetPlayerList()) {
            type = Match.GetCharacterInRotation();
            if (type == CharacterType.Runner) { i = r; r++; }
            if (type == CharacterType.Bomber) { i = b; b++; }
            position = currentMap.GetSpawnPosition(type, i);
            SpawnPlayer(pl, type, position);
        }
    }

    public void Score(ControllerId controller) {
        Debug.Log(Match.PlayerScore[controller]);
        Match.PlayerScore[controller] += 1;
        if (Match.PlayerScore[controller] < maxScore) {
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

    void StartNextRound() {
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
