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
        int rnd = Random.Range(0, bomberSpawns.Count);
        if (type == CharacterType.Runner) {
            return runnerSpawns[rnd];
        }
        else {
            return SearchFarthestSpawn(bomberSpawns);
        }
    }

    Vector3 SearchFarthestSpawn(IList<Vector3> spawnList) {
        Vector3 spawnPoint;
        Vector3 character;
        float distance = Mathf.NegativeInfinity;
        float farthestSpawnDistance = Mathf.NegativeInfinity;
        float spawnClosestChar;
        Vector3 farthestSpawn = new Vector3(0f, 1f, 0f);

        for (int i = 0; i < spawnList.Count; i++) {
            spawnClosestChar = Mathf.Infinity;
            for (int j = 0; j < PlayerManager.GetPlayerList().Count; j++) {

                if (PlayerManager.GetPlayerList()[j].Character.State == CharacterState.Alive) {
                    spawnPoint = spawnList[i];
                    character = PlayerManager.GetPlayerList()[j].Character.transform.position;
                    distance = Vector3.Distance(spawnPoint, character);

                    if (distance < spawnClosestChar) {
                        spawnClosestChar = distance;
                    }
                }
            }
            if (spawnClosestChar >= farthestSpawnDistance) {
                farthestSpawnDistance = spawnClosestChar;
                farthestSpawn = spawnList[i];
            }
        }
        return farthestSpawn;
    }
}