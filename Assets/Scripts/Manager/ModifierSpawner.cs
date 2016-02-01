using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ModifierSpawner : MonoBehaviour {

    public bool autoSpawn;
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
        if (autoSpawn)
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

    public void Spawn() {
        int randomIndex = Random.Range(0, spawnList.Count - 1);
        Vector3 selectedPos = spawnList[randomIndex].position;
        randomIndex = Random.Range(0, modifier.Length - 1);
        GameObject selectedMod = modifier[randomIndex];

        Instantiate(selectedMod, selectedPos, selectedMod.transform.rotation);
        modifierCount++;
    }

    public void Spawn(Modifier selectedMod) {
        int randomIndex = Random.Range(0, spawnList.Count - 1);
        Vector3 selectedPos = spawnList[randomIndex].position;
        randomIndex = Random.Range(0, modifier.Length - 1);

        Instantiate(selectedMod.pickUpObject, selectedPos, selectedMod.transform.rotation);
        modifierCount++;
    }
}