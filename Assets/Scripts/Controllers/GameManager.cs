using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    public float Score { get; set;}

    void Awake() {
        DontDestroyOnLoad(this.gameObject);
    }
}
