using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public enum GameState {
    Load,
    Intro,
    ChooseMod,
    Play,
    End
}

public class GameManager : MonoBehaviour {

    public IList<Bomber> bomberList = new List<Bomber>();
    public Runner runner;
    public byte maxScore = 5;
    public float roundClock;
    public Map currentMap;
    public GameState State { get; private set; }

    [SerializeField]
    GameObject runnerPrefab;
    [SerializeField]
    GameObject bomberPrefab;

    GameCanvas canvas;
    ModifierSpawner modSpawner;
    Timer timer = new Timer();

    void Awake() {
        canvas = GameObject.FindWithTag("Game Canvas").GetComponent<GameCanvas>();
        modSpawner = currentMap.transform.Find("Modifier Spawns").GetComponent<ModifierSpawner>();
        EnterState(GameState.Load);
    }

    void Update() {
        StateMachine();
    }

    void StateMachine() {
        switch (State) {
            default:
            case GameState.Load:
                break;
            case GameState.Intro:
                break;
            case GameState.ChooseMod:
                break;
            case GameState.Play:
                RoundTimer();
                break;
            case GameState.End:
                break;
        }
    }

    void EnterState(GameState newState) {
        State = newState;
        switch (State) {
            default:
            case GameState.Load:
                LoadPlayers();
                break;
            case GameState.Intro:
                break;
            case GameState.ChooseMod:
                ChooseMod();
                break;
            case GameState.Play:
                StartRound();
                break;
            case GameState.End:
                End();
                break;
        }
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
            SpawnCharacter(pl, type, position);
            MatchData.RotateCharacters();
            pl.Character.canControl = false;
        }
        EnterState(GameState.ChooseMod);
    }

    public void Score(ControllerId controller) {
        MatchData.PlayerScore[controller] += 1;
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

    void StartRound() {
        foreach (Player pl in PlayerManager.GetPlayerList()) {
            pl.Character.canControl = true;
        }
    }

    public void StartNextRound() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void EndMatch() {
        EnterState(GameState.End);
    }

    void End() {
        SceneManager.LoadScene("ResultScreen");
    }

    public GameObject SpawnCharacter(Player player, CharacterType type, Vector3 position) {
        GameObject pl = (GameObject)Instantiate(GetCharacterPrefab(type), position, transform.rotation);
        pl.GetComponent<BaseCharacter>().player = player;
        player.Character = pl.GetComponent<BaseCharacter>();
        if (type == CharacterType.Bomber) bomberList.Add(player.Character.GetComponent<Bomber>());
        else if (type == CharacterType.Runner) runner = player.Character.GetComponent<Runner>();
        return pl;
    }

    public void RemoveCharacter(BaseCharacter character){
        if (character.type == CharacterType.Bomber) {
            bomberList.Remove((Bomber)character);
        }
        else if (character.type == CharacterType.Runner) {
            runner = null;
        }
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

    void ChooseMod() {
        if (ModifierManager.AnalyzeScoreForBalance()) {
            canvas.ShowModifierChoice(true);
        }
        else {
            EnterState(GameState.Play);
        }
    }

    public void BuildChosenMods(IList<Modifier> mods) {
        foreach (Modifier mod in mods) {
            modSpawner.Spawn(mod);
        }
        EnterState(GameState.Play);
    }

    public List<BaseCharacter> GetCharacters()
    {
        List<BaseCharacter> list = new List<BaseCharacter>();
        if (runner != null) list.Add(runner);
        foreach (Bomber bomber in bomberList)
        {
            list.Add((BaseCharacter)bomber);
        }
        return list;
    }
}
