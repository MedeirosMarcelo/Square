using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Map : MonoBehaviour {

    public float clockTime = 60f;
    public float runnerRespawnDelay = 3f;
    public float bomberRespawnDelay = 2f;
    public ModifierSpawner modifierSpawner;
    public IList<Vector3> runnerSpawns;
    public IList<Vector3> bomberSpawns;

    void Awake() {
        modifierSpawner = GetComponentInChildren<ModifierSpawner>();
        LoadPlayerSpawns();
    }

    void LoadPlayerSpawns() {
        Transform runnerSpawnObj = transform.Find("Runner Spawns");
        runnerSpawns = new List<Vector3>();
        foreach (Transform tr in runnerSpawnObj) {
            runnerSpawns.Add(tr.position);
        }

        Transform bomberSpawnObj = transform.Find("Bomber Spawns");
        bomberSpawns = new List<Vector3>();
        foreach (Transform tr in bomberSpawnObj) {
            bomberSpawns.Add(tr.position);
        }
    }

    ///<summary>
    ///<para>Returns the position of a given spawn point.</para>
    ///</summary>
    public Vector3 GetSpawnPosition(CharacterType type, int i) {
        if (type == CharacterType.Runner) {
            return runnerSpawns[i];
        }
        else {
            return bomberSpawns[i];
        }
    }

    ///<summary>
    ///<para>Returns the position of a given spawn point.</para>
    ///<para>Ommitting the index param will return a random valid spawn position.</para>
    ///</summary>
    public Vector3 GetSpawnPosition(CharacterType type) {
        if (type == CharacterType.Runner) {
            return runnerSpawns[Random.Range(0, runnerSpawns.Count)];
        }
        else {
            return bomberSpawns[Random.Range(0, bomberSpawns.Count)];
        }
    }
}