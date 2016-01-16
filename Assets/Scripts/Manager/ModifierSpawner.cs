using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ModifierSpawner : MonoBehaviour{

    public GameObject[] modifier;
    public int modifierCount;
    public float interval = 5f;
    IList<Transform> spawnList;
    Timer timer;

    void Start() {
        timer = new Timer();

        spawnList = new List<Transform>();
        foreach (Transform tr in this.transform) {
            spawnList.Add(tr);
        }
    }

    void Update() {
        SpawnAI();
    }

    void SpawnAI() {
        if (modifierCount == 0) {
            Spawn();
        }
        else {
            if (modifierCount < spawnList.Count) {
                if (timer.Run(10f)) {
                    Spawn();
                }
            }
        }
    }

    void Spawn() {
        int randomIndex = Random.Range(0, spawnList.Count - 1);
        Vector3 selectedPos = spawnList[randomIndex].position;
        randomIndex = Random.Range(0, modifier.Length - 1);
        GameObject selectedMod = modifier[randomIndex];

        Instantiate(selectedMod, selectedPos, selectedMod.transform.rotation);
        modifierCount++;
    }
}
